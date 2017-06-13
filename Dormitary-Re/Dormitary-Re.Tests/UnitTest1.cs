using Microsoft.VisualStudio.TestTools.UnitTesting;
using Dormitary_Re.Models;
using System.Collections.Generic;
using System;

namespace Dormitary_Re.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestGetOrderList()
        {
            //arrange
            DateTime dt = new DateTime(2017, 01, 01, 5, 5, 5);
            List<Order> ExceptedData = new List<Order>() {
                new Order {orderaccount="Ice",ordertime = dt,price = 50,product="Drink"},
                new Order {orderaccount="Ice",ordertime = dt,price = 80,product="Food"},
            };
            OrderModelFake omF = new OrderModelFake();
            //act
            List<Order> ActualData = new List<Order>();
            ActualData = omF.GetOrderList();
            //assert
            Equals(ExceptedData, ActualData);
        }
        [TestMethod]
        public void TestSubmitIsTrue()
        {
            OrderModelFake omF = new OrderModelFake();
            var expected = true;
            var actual = omF.Submit("測試", "測試吃的", 88);
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void TestSubmitIsFalse()
        {
            OrderModelFake omF = new OrderModelFake();
            var expected = false;
            var actual = omF.Submit(null, null, 0);
            Assert.AreEqual(expected, actual);
        }
    }
    class OrderModelFake : OrderModel
    {
        public override List<Order> GetOrderList()
        {
            DateTime dt = new DateTime(2017, 01, 01, 5, 5, 5);
            List<Order> OrderActualData = new List<Order>() {
                new Order {orderaccount="Ice",ordertime = dt,price = 50,product="Drink"},
                new Order {orderaccount="Ice",ordertime = dt,price = 80,product="Food"},
            };
            return OrderActualData;
        }
        public override bool Submit(string ordername, string Product, int price)
        {
            if (ordername != null && Product != null && price.ToString() != null)
            {
                return true;
            }
            return false;
        }
    }
}
