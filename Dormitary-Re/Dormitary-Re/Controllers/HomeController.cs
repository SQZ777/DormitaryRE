using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Dormitary_Re.Models;
namespace Dormitary_Re.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        OrderModel orderModel = new OrderModel();
        LoginModel loginMdel = new LoginModel();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Order()
        {
            List<Order> OrderList = orderModel.GetOrderList().ToList();
            foreach (var item in OrderList)
            {
                ViewData["Ordered"] = item.price;
            }
            return View(OrderList);
        }

        [HttpPost]
        public ActionResult Submit(string Product,int HowMuch)
        {
            if (orderModel.Submit("Test", Product, HowMuch))
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
        public ActionResult Login(string account,string pwd)
        {
            if (loginMdel.Login(account, pwd))
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("LoginIndex");
        }
    }
}