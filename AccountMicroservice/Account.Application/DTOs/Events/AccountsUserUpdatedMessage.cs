using Shared.ExternalServices.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.Application.DTOs.Events
{
    public class AccountsUserUpdatedMessage : IntegrationBaseMessage
    {
        public int UserId { get; set; }
        public string OldFirstName { get; set; }
        public string OldLastName { get; set; }
        public string OldEmailAddress { get; set; }
        public string OldPhoneNumber { get; set; }
        public string OldDeviceId { get; set; }
        public string OldUserName { get; set; }
        public NameAndCode OldAccountStatus { get; set; }
        public DateTime DateModified { get; set; }
        public int ModifiedByUserId { get; set; }

        public string NewFirstName { get; set; }
        public string NewLastName { get; set; }
        public string NewEmailAddress { get; set; }
        public string NewPhoneNumber { get; set; }
        public string NewDeviceId { get; set; }
        public string NewUserName { get; set; }
        public NameAndCode NewAccountStatus { get; set; }
    }
}
