using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Dormitary_Re.Models;
using Dormitary_Re.Filter;

namespace Dormitary_Re.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        OrderModel orderModel = new OrderModel();
        LoginModel loginMdel = new LoginModel();
        [FilterPermission]
        public ActionResult Index()
        {
            return View();
        }

        [FilterPermission]
        public ActionResult Order()
        {
            List<Order> OrderList = orderModel.GetOrderList().ToList();
            foreach (var item in OrderList)
            {
                ViewData["Ordered"] = item.price;
            }
            return View(OrderList);
        }
        enum Ordering { wantorder = 1, dontwantorder = 0 };
        [HttpPost]
        [FilterPermission]
        public ActionResult Submit(string Product, int HowMuch)
        {
            if (orderModel.Submit(Session["Account"].ToString(), Product, HowMuch, (int)Ordering.wantorder))
            {
                return RedirectToAction("Order");
            }
            return Content("InsertFalse");
        }

        public ActionResult LoginIndex()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string account, string pwd)
        {
            Session.Clear();
            if (loginMdel.Login(account, pwd))
            {
                Session["Account"] = account;
                return RedirectToAction("Index");
            }
            return RedirectToAction("LoginIndex");
        }
    }
}