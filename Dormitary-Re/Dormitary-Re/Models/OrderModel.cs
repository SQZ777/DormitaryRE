using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace Dormitary_Re.Models
{
    public class OrderModel
    {
        public virtual List<Order> GetOrderList()
        {
            using (var cn = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                return cn.Query<Order>("select * from orders where ordering=1").ToList();
            }
        }
        public virtual bool Submit(string ordername,string Product,int price)
        {
            try
            {
                using (var cn = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
                {
                    string sqlCommand = "Insert Into orders(price,orderaccount,product,ordertime) VALUES(@price,@orderaccount,@product,@ordertime)";
                    var ordered = new Order()
                    {
                        price = price,
                        orderaccount = ordername,
                        product = Product,
                        ordertime = System.DateTime.Now
                    };
                    cn.Execute(sqlCommand, ordered);
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
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