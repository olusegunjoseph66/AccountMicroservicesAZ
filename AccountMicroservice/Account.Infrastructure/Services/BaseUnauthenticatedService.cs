using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.Infrastructure.Services
{
    public class BaseUnauthenticatedService
    {
        public BaseUnauthenticatedService()
        {
                
        }

        public bool PasswordContainsUserName(string userName, string password)
        {
            if (string.IsNullOrWhiteSpace(userName) || string.IsNullOrWhiteSpace(password)) 
                return false;

            if(password.ToLower().Contains(userName.ToLower()))
                return true;

            return false;

        }
    }
}
