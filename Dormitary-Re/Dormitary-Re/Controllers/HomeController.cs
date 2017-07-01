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
            List<Order> OrderList = orderModel.GetOrderList((int)OrderingStatus.Ordering).ToList();
            return View(OrderList);
        }

        [HttpPost]
        [FilterPermission]
        public ActionResult Submit(string Product, int HowMuch)
        {
            if (orderModel.Submit(Session["Account"].ToString(), Product, HowMuch, (int)OrderingStatus.Ordering))
            {
                return RedirectToAction("Order");
            }
            return Content("InsertFalse");
        }

        public ActionResult LoginIndex()
        {
            return View();
        }

        enum OrderingStatus { Ordering = 1, NotOrdering = 0 }
        public ActionResult SetAllProductOrderingStatus()
        {
            int FinalPrice = orderModel.GetFinalPrice(orderModel.GetOrderList((int)OrderingStatus.Ordering).ToList(), (int)OrderingStatus.Ordering);
            orderModel.SetAllOrdering((int)OrderingStatus.NotOrdering);
            orderModel.InsertFinshOrder(Session["Account"].ToString(), FinalPrice);
            orderModel.UpdateFinishedOrders(orderModel.GetLatestIDFromFinishOrder());
            return RedirectToAction("Order");
        }
        
        public ActionResult HistoryPage()
        {
            OrderAndHistory oah = new OrderAndHistory {
                order= orderModel.GetOrderList((int)OrderingStatus.NotOrdering).ToList(),
                finishorder = orderModel.GetFinishedOrdersList().ToList()
            };
            return View(oah);
        }
        

        [HttpPost]
        public ActionResult Login(string account, string pwd)
        {
            Session.Clear();
            if (loginMdel.Login(account, pwd))
            {
                Session["account"] = account;
                return RedirectToAction("Index");
            }
            return RedirectToAction("LoginIndex");
        }
    }
}