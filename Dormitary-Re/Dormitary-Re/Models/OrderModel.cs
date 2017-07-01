using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Configuration;

namespace Dormitary_Re.Models
{

    public interface ISetAllOrdering
    {
        void SetAllOrdering(int Status);
        void InsertFinshOrder(string account, int finalprice);
        void UpdateFinishedOrders(int FinishOrder);
        int GetLatestIDFromFinishOrder();
    }
    public class OrderModel : ISetAllOrdering
    {
        public virtual List<Order> GetOrderList(int ordering)
        {
            return ExecuteSQL<Order>("select * from orders where ordering=@ordering", new { ordering = ordering }).ToList();
        }

        public virtual List<FinishedOrders> GetFinishedOrdersList()
        {
            return ExecuteSQL<FinishedOrders>("select * from FinishedOrders", null).ToList();
        }

        public virtual bool Submit(string ordername, string Product, int price, int ordering)
        {
            string sqlCommand = "Insert Into orders(price,orderaccount,product,ordertime,ordering) VALUES(@price,@orderaccount,@product,@ordertime,@ordering)";
            var ordered = new Order()
            {
                price = price,
                orderaccount = ordername,
                product = Product,
                ordertime = DateTime.Now,
                ordering = ordering
            };
            ExecuteSQL<Order>(sqlCommand, ordered);

            return true;
        }

        public int GetFinalPrice(List<Order> OrderList, int Ordering)
        {
            int FinalPrice = 0;
            foreach (var item in OrderList)
            {
                if (item.ordering == Ordering)
                {
                    FinalPrice += item.price;
                }
            }
            return FinalPrice;
        }

        public void SetAllOrdering(int Status)
        {
            string sqlCommand = "Update orders SET ordering = @Status where ordering != @Status";
            ExecuteSQL<Order>(sqlCommand, new { Status = Status });
            
        }

        public void UpdateFinishedOrders(int finishedorders)
        {
            string sqlCommand = "Update orders SET finishedorders = @finishedorders where finishedorders is NULL";
            ExecuteSQL<Order>(sqlCommand, new { finishedorders = finishedorders });
        }

        public void InsertFinshOrder(string account, int finalprice)
        {
            string sqlCommand = "Insert into FinishedOrders(finalPrice,whoFinished,finishtime) VALUES(@finalPrice,@whoFinished,@finishtime)";
            FinishedOrders fo = new FinishedOrders
            {
                finalPrice = finalprice,
                whoFinished = account,
                finishtime = DateTime.Now
            };
            ExecuteSQL<FinishedOrders>(sqlCommand, fo);
        }

        public int GetLatestIDFromFinishOrder()
        {
            return ExecuteSQL<FinishedOrders>("select * from FinishedOrders",null).LastOrDefault().Id;
        }


        public IEnumerable<T> ExecuteSQL<T>(string sql, object param)
        {
            using (var cn = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                return cn.Query<T>(sql, param);
            }
        }
    }
    public partial class FinishedOrders
    {
        public int Id { get; set; }
        public int finalPrice { get; set; }
        public string whoFinished { get; set; }
        public DateTime finishtime { get; set; }
    }
    public partial class Order
    {
        public string product { get; set; }
        public DateTime ordertime { get; set; }
        public int price { get; set; }
        public string orderaccount { get; set; }
        public int ordering { get; set; }
        public int finishedorders { get; set; }
    }
    public partial class OrderAndHistory
    {
        public IEnumerable<Order> order { get; set; }
        public IEnumerable<FinishedOrders> finishorder { get; set; }
    }
}