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
        readonly OrderModel _orderModel = new OrderModel();
        readonly LoginModel _loginMdel = new LoginModel();
        [FilterPermission]
        public ActionResult Index()
        {
            return View();
        }

        [FilterPermission]
        public ActionResult Order()
        {
            List<Order> OrderList = _orderModel.GetOrderList((int)OrderingStatus.Ordering).ToList();
            return View(OrderList);
        }

        [HttpPost]
        [FilterPermission]
        public ActionResult Submit(string Product, int HowMuch)
        {
            if (_orderModel.Submit(Session["Account"].ToString(), Product, HowMuch, (int)OrderingStatus.Ordering))
            {
                return RedirectToAction("Order");
            }
            return Content("InsertFalse");
        }

        public ActionResult LoginIndex()
        {
            return View();
        }
        
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("LoginIndex");
        }

        enum OrderingStatus { Ordering = 1, NotOrdering = 0 }
        public ActionResult SetAllProductOrderingStatus()
        {
            int FinalPrice = _orderModel.GetFinalPrice(_orderModel.GetOrderList((int)OrderingStatus.Ordering).ToList(), (int)OrderingStatus.Ordering);
            _orderModel.SetAllOrdering((int)OrderingStatus.NotOrdering);
            _orderModel.InsertFinshOrder(Session["Account"].ToString(), FinalPrice);
            _orderModel.UpdateFinishedOrders(_orderModel.GetLatestIDFromFinishOrder());
            return RedirectToAction("Order");
        }

        [FilterPermission]
        public ActionResult HistoryPage()
        {
            OrderAndHistory oah = new OrderAndHistory
            {
                order = _orderModel.GetOrderList((int)OrderingStatus.NotOrdering).ToList(),
                finishorder = _orderModel.GetFinishedOrdersList().ToList()
            };
            oah.finishorder = oah.finishorder.OrderByDescending(m => m.finishtime);
            return View(oah);
        }


        [HttpPost]
        public ActionResult Login(string account, string pwd)
        {
            Session.Clear();
            if (_loginMdel.Login(account, pwd))
            {
                Session["account"] = account;
                return RedirectToAction("Index");
            }
            return RedirectToAction("LoginIndex");
        }
    }
}