using Banking.Dao;
using Banking_Project.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Banking_Project.Controllers
{
    public class AdminLoginController : Controller
    {
        private readonly SqlDataAccess _context = new SqlDataAccess();
        // GET: AdminLogin
        public ActionResult Index()
        {
            return View();
        }
    
        [HttpPost]
        public ActionResult Index(Admin adminlog)
        {
            SqlCommand sqlcommand = new SqlCommand("[dbo].[AdminLogin]", _context.Connect());
            sqlcommand.CommandType = CommandType.StoredProcedure;
            sqlcommand.Parameters.AddWithValue("@UserName", adminlog.UserName);
            sqlcommand.Parameters.AddWithValue("@Email", adminlog.Email);
            sqlcommand.Parameters.AddWithValue("@Password", adminlog.Password);
            SqlDataReader sdr = sqlcommand.ExecuteReader();

            if (sdr.Read())
            {
               
                Session["UserName"] = adminlog.UserName.ToString();
                if (Session["UserName"] != null)
                {
                    return RedirectToAction("Dashboard","Customer");
                }
                else
                {
                    return RedirectToAction("index");
                }
            }
            else
            {
                ViewData["message"] = "Login Failed";
            }
            _context.Connect().Close();
            return View();
        }
        public ActionResult AdminDashboard()
        {
            return View();
        }
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("index");
        }

    }
}