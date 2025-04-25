
using Group13iFinanceFix.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Group13iFinanceFix.Controllers
{
    public class NonAdminController : Controller
    {
        public ActionResult NonAdminDashboard() //Display the non admin dashboard
        {
            var userId = Session["UserID"]?.ToString(); // check if the user is logged in from session data
            if (userId == null) return RedirectToAction("Login", "Account"); //if not logged in, redirect to login page

            using (var db = new Group13_iFINANCEDBEntities1()) //If logged in access the DB
            {
                var user = db.NonAdminUser.FirstOrDefault(u => u.ID == userId); //Get users info from their ID
                var profile = db.iFINANCEUser.FirstOrDefault(u => u.ID == userId); //Get the users name from iFinanceUser table

                if (user != null && profile != null) //If the records are found continue
                {
                    ViewBag.UsersName = profile.UsersName; // Stores the users name
                    return View(user);
                }
            }

            return RedirectToAction("Login", "Account"); //redirect to login if failed (small security measure)
        }

        [HttpGet]
        public ActionResult ChangePassword()
        {
            var userId = Session["UserID"]?.ToString(); // check session
            if (userId == null)
                return RedirectToAction("Login", "Account");

            return View(); // load change pass page
        }

        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordViewModel model)
        {
            var userId = Session["UserID"]?.ToString(); //grab current user
            if (userId == null)
                return RedirectToAction("Login", "Account");

            if (!ModelState.IsValid)
                return View(model); 

            if (model.NewPassword != model.ConfirmPassword)
            {
                ViewBag.Message = "New passwords do not match."; //mismatch
                return View(model);
            }

            using (var db = new Group13_iFINANCEDBEntities1())
            {
                var user = db.UserPassword.FirstOrDefault(u => u.ID == userId);

                if (user == null || user.encryptedPassword != model.CurrentPassword)
                {
                    ViewBag.Message = "Current password is incorrect."; //wrong current
                    return View(model);
                }

                user.encryptedPassword = model.NewPassword;
                db.SaveChanges(); //save new pass

                TempData["Success"] = "Password changed successfully!";
                return RedirectToAction("NonAdminDashboard", "NonAdmin"); 
            }
        }

        public ActionResult BalanceSheet()
        {
            var userId = Session["UserID"]?.ToString(); //Grab user
            var isAdmin = Session["IsAdmin"] as bool?; //Check admin

            if (userId == null || isAdmin == true)
                return RedirectToAction("Login", "Account"); // block if not logged in or is admin

            using (var db = new Group13_iFINANCEDBEntities1())
            {   //Grab all accounts with type from category
                var data = (
                    from ma in db.MasterAccount
                    join grp in db.GroupTable on ma.accountGroup equals grp.ID
                    join cat in db.AccountCategory on grp.element equals cat.ID
                    select new ReportEntry
                    {
                        AccountName = ma.name,
                        Amount = ma.closingAmount ?? 0, //handles nulls
                        AccountType = cat.accountType
                    }
                ).ToList();

                //Filter between assest liability and equity
                ViewBag.Assets = data.Where(d => d.AccountType == "Asset").ToList(); 
                ViewBag.Liabilities = data.Where(d => d.AccountType == "Liability").ToList();
                ViewBag.Equity = data.Where(d => d.AccountType == "Equity").ToList();

                return View();
            }
        }

        public ActionResult ProfitAndLoss()
        {
            var userId = Session["UserID"]?.ToString(); // get current user
            var isAdmin = Session["IsAdmin"] as bool?;  // check if admin
            if (userId == null || isAdmin == true)
                return RedirectToAction("Login", "Account"); // block admins or not logged in

            using (var db = new Group13_iFINANCEDBEntities1())
            {
                // grab accounts and join with category for types
                var data = (
                    from ma in db.MasterAccount
                    join grp in db.GroupTable on ma.accountGroup equals grp.ID
                    join cat in db.AccountCategory on grp.element equals cat.ID
                    select new ReportEntry
                    {
                        AccountName = ma.name,
                        Amount = ma.closingAmount ?? 0, // fallback if null
                        AccountType = cat.accountType
                    }
                ).ToList();

                // filter types
                ViewBag.Income = data.Where(d => d.AccountType == "Income").ToList();
                ViewBag.Expenses = data.Where(d => d.AccountType == "Expense").ToList();

                // cast back so we can sum it (lambda issue workaround)
                var incomeList = ViewBag.Income as List<ReportEntry>;
                var expenseList = ViewBag.Expenses as List<ReportEntry>;

                ViewBag.TotalIncome = incomeList?.Sum(i => i.Amount) ?? 0; // total income
                ViewBag.TotalExpenses = expenseList?.Sum(e => e.Amount) ?? 0; // total expenses

                return View(); // push to view
            }
        }


    }


}
