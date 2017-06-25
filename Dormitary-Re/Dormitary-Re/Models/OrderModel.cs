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
        bool SetAllOrdering(int Status);
    }
    public class OrderModel : ISetAllOrdering
    {
        public virtual List<Order> GetOrderList(int ordering)
        {

            return GetOrderLsitForOrdering<Order>("select * from orders where ordering=@ordering", new { ordering = ordering}).ToList();
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
            ExecuteSQLForOrders<Order>(sqlCommand, ordered);
            return true;
        }

        public bool SetAllOrdering(int Status)
        {
            string sqlCommand = "Update orders SET ordering = 1 where ordering != @Status";
            ExecuteSQLForOrders<Order>(sqlCommand, new { Status = Status });
            return false;
        }


        public void ExecuteSQLForOrders<T>(string sql, object param)
        {
            using (var cn = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                cn.Query<T>(sql, param);
            }
        }
        public IEnumerable<T> GetOrderLsitForOrdering<T>(string sql, object param)
        {
            using (var cn = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                return cn.Query<T>(sql, param);
            }
        }

    }
    public partial class Order
    {
        public string product { get; set; }
        public DateTime ordertime { get; set; }
        public int price { get; set; }
        public string orderaccount { get; set; }
        public int ordering { get; set; }
    }
}