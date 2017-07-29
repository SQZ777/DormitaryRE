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
            object success;
            using (var cn = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                string sqlCommand = "Select * from account where account = @account and password = @password";
                var login = new Login()
                {
                    Account = account,
                    Password = password
                };
                success = cn.QueryFirstOrDefault(sqlCommand, login);
            }
            if (success != null)
                return true;
            return false;
        }
    }

    public class Login
    {
        public string Account { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string NickName { get; set; }
        public int Money { get; set; }
    }
}