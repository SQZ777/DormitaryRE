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
        OrderModel OM = new OrderModel();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Order()
        {
            List<Order> OrderList = OM.GetOrderList().ToList();
            foreach (var item in OrderList)
            {
                ViewData["Ordered"] = item.price;
            }
            return View(OrderList);
        }

        [HttpPost]
        public ActionResult Submit(string Product,int HowMuch)
        {
            if (OM.Submit("Test", Product, HowMuch))
            {
                return RedirectToAction("Order");
            }
            return Content("InsertFalse");
        }

        public ActionResult LoginIndex()
        {
            return View();
        }


    }
}