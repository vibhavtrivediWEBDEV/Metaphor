using DLMetaphor.Beans;
using DLMetaphor.Classes;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLMetaphor
{
    public class DLLogin
    {
        private SQLConnect connection;
        private SqlCommand cmd;
        public DLLogin()
        {
            connection = new SQLConnect();
            cmd = new SqlCommand();
        }

        public User Login(User? user)
        {
            try
            {
                if(user != null)
                {
                    cmd = new SqlCommand();
                    cmd.CommandText = $"SELECT TOP 1 ROWPOS, USERNAME, MOBILE, EMAIL, OTP FROM USERLOGIN WHERE USERNAME = '{user.UserName}'";

                    var userData = connection.ExecuteSelectorQuery(cmd);
                    if(userData.Rows.Count > 0)
                    {
                        return user;
                    }
                }
            }
            catch(Exception err)
            {
                Console.WriteLine("Error occurred : " + err.Message);
            }

            return null;
        }
    }
}
