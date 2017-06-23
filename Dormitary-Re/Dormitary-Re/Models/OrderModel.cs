using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace Dormitary_Re.Models
{

    public interface ISetAllOrdering
    {
        bool SetAllOrdering(int Status);
    }
    public class OrderModel : ISetAllOrdering
    {
        public virtual List<Order> GetOrderList()
        {
            using (var cn = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                return cn.Query<Order>("select * from orders where ordering=1").ToList();
            }
        }
        public virtual bool Submit(string ordername, string Product, int price, int ordering)
        {
            try
            {
                string sqlCommand = "Insert Into orders(price,orderaccount,product,ordertime,ordering) VALUES(@price,@orderaccount,@product,@ordertime,@ordering)";
                var ordered = new Order()
                {
                    price = price,
                    orderaccount = ordername,
                    product = Product,
                    ordertime = System.DateTime.Now,
                    ordering = ordering
                };
                ExecuteSQLForOrders<Order>(sqlCommand, ordered);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool SetAllOrdering(int Status)
        {
            string sqlCommand = "Update orders SET ordering = 1 where ordering != 1";
            if (Status == 0)
            {
                sqlCommand = "Update orders SET ordering = 0 where ordering != 0";
            }
            ExecuteSQLForOrders<Order>(sqlCommand, null);
            return false;
        }
        public void ExecuteSQLForOrders<T>(string sql, object param)
        {
            using (var cn = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                cn.Query<T>(sql, param);
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