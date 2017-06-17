using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Dormitary_Re.Models
{
    interface ILoginCertify
    {
        bool Login(string account, string password);
    }

    public class LoginModel : ILoginCertify
    {
        public bool Login(string account, string password)
        {
            object Success;
            using (var cn = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                string sqlCommand = "Select * from account where account = @account and password = @password";
                var Login = new Login()
                {
                    account = account,
                    password = password
                };
                Success = cn.QueryFirstOrDefault(sqlCommand, Login);
            }
            if (Success != null)
            {
                return true;
            }
            return false;
        }
    }

    public class Login
    {
        public string account { get; set; }
        public string password { get; set; }
        public string email { get; set; }
        public string NickName { get; set; }
        public int money { get; set; }
    }
}