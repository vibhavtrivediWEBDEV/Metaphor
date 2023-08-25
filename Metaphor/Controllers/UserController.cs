using BLMetaphor.Classes;
using DLMetaphor.Beans;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography.X509Certificates;

namespace Metaphor.Controllers
{
    public class UserController : Controller
    {
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
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

        [HttpPost]
        public Result<User> SignUp([FromBody] User user)
        {
            Result<User> result = new Result<User>();
            BLLogin login = new BLLogin();
            try
            {
                result = login.SignUp(user);
            }
            catch (Exception err)
            {
                result.IsSuccess = false;
                result.Message = "Error found : " + err.Message;
            }

            return result;
        }
    }
}
