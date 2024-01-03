using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.Application.ViewModels.Responses.ResponseDto
{
    public class UserDetailResponseDto
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
        public string UserName { get; set; }
        public int NumOfSapAccounts { get; set; }
        public DateTime DateCreated { get; set; }
        public string ProfilePhotoPublicUrl { get; set; }
        public DateTime? LastLoginDate { get; set; }
    }
}
