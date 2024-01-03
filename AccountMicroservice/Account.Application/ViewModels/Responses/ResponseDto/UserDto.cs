using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.Application.ViewModels.Responses.ResponseDto
{
    public class UserDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string UserName { get; set; }
        public string CompanyCode { get; set; }
        public DateTime? LastLoginDate { get; set; }
        public string Role { get; set; }
    }
}
