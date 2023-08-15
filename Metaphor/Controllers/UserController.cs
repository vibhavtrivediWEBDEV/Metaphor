using BLMetaphor.Classes;
using DLMetaphor.Beans;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography.X509Certificates;

namespace Metaphor.Controllers
{
    public class UserController
    {
        [HttpGet]
        public Result<User> Login([FromBody] User user)
        {
            Result<User> result = new Result<User>();
            BLLogin login = new BLLogin();
            try
            {
                result = login.Login(user);
            }
            catch (Exception err)
            {
                result.IsSuccess = false;
                result.Message = "Error occurred : " + err.Message;
            }

            return result;
        }
    }
}
