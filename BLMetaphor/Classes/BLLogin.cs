using DLMetaphor;
using DLMetaphor.Beans;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLMetaphor.Classes
{
    public class BLLogin
    {
        DLLogin login = new DLLogin();

		public Result<User> SignUp(User user)
		{
			Result<User> result = new Result<User>();
			try
			{
                bool isExists = login.IsUserExists(user);
                if (!isExists)
                {
					user = login.CreateNewUser(user);
					if (user != null)
					{
						result.IsSuccess = true;
						result.Data = user;
					}
				}
                else
                {
                    result.IsSuccess = false;
                    result.Message = "This username is already taken, try again with another!";
                }
				
			}
			catch (Exception err)
			{
				result.IsSuccess = false;
				result.Message = "Error occurred : " + err.Message;
			}

			return result;
		}

		public Result<User> Login(User user)
        {
            Result<User> result = new Result<User>();
            try
            {
                user = login.Login(user);
                if(user != null)
                {
                    result.IsSuccess = true;
                    result.Data = user;
                }
            }
            catch(Exception err)
            {
                result.IsSuccess = false;
                result.Message = "Error occurred : " + err.Message;
            }

            return result;
        } 

        public bool DeleteUserName(User user)
        {
            DLLogin login = new DLLogin();
            bool result = login.DeleteUserName(user);

            return result;
        }

	}
}
