using Microsoft.VisualStudio.TestTools.UnitTesting;
using Dormitary_Re.Models;
using System.Collections.Generic;
using System;
using System.Linq;

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
        public void TestGetOrderListAndOrderingIsNotNull()
        {
            //arrange
            DateTime dt = new DateTime(2017, 01, 01, 5, 5, 5);
            List<Order> ExceptedData = new List<Order>() {
                new Order {orderaccount="Ice",ordertime = dt,price = 50,product="Drink"}
            };
            OrderModelFake omF = new OrderModelFake();
            //act
            List<Order> ActualData = GetOrderList();
            //assert
            Equals(ExceptedData, ActualData);
            Assert.IsFalse(ExceptedData.SequenceEqual(ActualData, new OrderListEquality()));
        }
        [TestMethod]
        public void TestSubmitIsTrue()
        {
            OrderModelFake omF = new OrderModelFake();
            var expected = true;
            var actual = omF.Submit("測試", "測試吃的", 88, 1);
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void TestSubmitIsFalse()
        {
            OrderModelFake omF = new OrderModelFake();
            var expected = false;
            var actual = omF.Submit(null, null, 0, 1);
            Assert.AreEqual(expected, actual);
        }

        private List<Order> GetOrderList()
        {
            DateTime dt = new DateTime(2017, 01, 01, 5, 5, 5);
            return new List<Order>() {
                new Order {orderaccount="Ice",ordertime = dt,price = 50,product="Drink",ordering=1},
                new Order {orderaccount="Ice",ordertime = dt,price = 80,product="Food",ordering=0}
            };
        }
        public class OrderListEquality : EqualityComparer<Order>
        {
            public override bool Equals(Order x, Order y)
            {
                return x.ordering == y.ordering
                    && x.ordertime == y.ordertime;
            }
            public override int GetHashCode(Order obj)
            {
                return 0;
            }
        }
    }
    class OrderModelFake : OrderModel
    {
        public override List<Order> GetOrderList()
        {
            DateTime dt = new DateTime(2017, 01, 01, 5, 5, 5);
            return new List<Order>() {
                new Order {orderaccount="Ice",ordertime = dt,price = 50,product="Drink",ordering=1},
                new Order {orderaccount="Ice",ordertime = dt,price = 80,product="Food",ordering=0}
            };
        }
        public override bool Submit(string ordername, string Product, int price, int ordering)
        {
            if (ordername != null && Product != null && price.ToString() != null && ordering.ToString() != null)
            {
                return true;
            }
            return false;
        }
    }
}
