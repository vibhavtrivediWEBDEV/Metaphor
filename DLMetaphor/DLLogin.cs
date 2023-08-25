using DLMetaphor.Beans;
using DLMetaphor.Classes;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Data;
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

        public bool IsUserExists(User? user)
        {
            bool isExists = false;
            try
            {
                if(user != null)
                {
					cmd = new SqlCommand();
					cmd.CommandText = $"SELECT TOP 1 ROWPOS FROM USERLOGIN WHERE USERNAME ='{user.UserName}'";

                    isExists = connection.ExecuteSelectorQuery(cmd).AsEnumerable().Any();
				}
                
            }
            catch(Exception err)
            {

            }

            return isExists;
        }

		public bool DeleteUserName(User? user)
		{
			try
			{
				if (user != null)
				{
					cmd = new SqlCommand();
					cmd.CommandText = $"DELETE FROM USERLOGIN WHERE USERNAME ='{user.UserName}'";

					int i = connection.ExecuteDMLQuery(cmd);
                    if (i > 0)
                    {
                        return true;
                    }
				}

			}
			catch (Exception err)
			{

			}

            return false;
		}

        public int GenerateOTP()
        {
			Random random = new Random();
			int randomNumber = random.Next(100000, 999999);

            return randomNumber;
		}

		public User CreateNewUser(User? user)
        {
            try
            {
                if(user != null)
                {
                    int otp = GenerateOTP();
                    cmd = new SqlCommand();
                    cmd.CommandText = $"INSERT INTO USERLOGIN(USERNAME, MOBILE, OTP) VALUES('{user.UserName}', {user.Mobile}, {otp})";

                    int i = connection.ExecuteDMLQuery(cmd);
                    if (i > 0)
                    {
                        user.OTP = otp.ToString();
                        return user;
                    }
                }
            }
            catch(Exception err)
            {

            }

            return null;
        }

        public User Login(User? user)
        {
            try
            {
                if(user != null)
                {
                    cmd = new SqlCommand();
                    cmd.CommandText = $"SELECT TOP 1 ROWPOS, USERNAME, MOBILE, OTP FROM USERLOGIN WHERE MOBILE = '{user.Mobile}'";

                    var userData = connection.ExecuteSelectorQuery(cmd);
                    if(userData.Rows.Count > 0)
                    {
                        var message = $"Your OTP for registration is : {userData.Rows[0]["OTP"]}";
                        user.WillPersist = SendOTP(message, user.Mobile);
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

		public bool SendOTP(string message, string phoneNumber)
		{
            try
            {
				// Create a RestClient
				var client = new RestClient($"https://www.fast2sms.com/dev/bulkV2?authorization={GlobalVariables.API_KEY}&route=otp&variables_values={message}&flash=1&numbers={phoneNumber}");

				// Create a RestRequest with the POST method
				var request = new RestRequest(Method.Get.ToString());

				// Execute the request
				RestResponse response = client.Execute(request);

				// Check if the request was successful
				if (response.IsSuccessful)
				{
					Console.WriteLine($"Message sent successfully. Response: {response.Content}");
					return true;
				}
				else
				{
					Console.WriteLine($"Error sending message. Status code: {response.StatusCode}, Response: {response.Content}");
				}
			}
            catch(Exception err)
            {

            }

            return false;
		}
    }
}
