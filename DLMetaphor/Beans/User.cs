using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLMetaphor.Beans
{
    public class User
    {
        public long Rowpos { get; set; }
        public string UserName { get; set; }
        public string OTP { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Bio { get; set; }
        public string BankAccount { get; set; }
        public string IFSCCode { get; set; }
    }
}
