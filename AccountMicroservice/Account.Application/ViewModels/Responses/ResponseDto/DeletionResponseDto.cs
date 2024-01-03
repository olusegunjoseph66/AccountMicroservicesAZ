using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.Application.ViewModels.Responses.ResponseDto
{
    public class DeletionResponseDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Reason { get; set; }
        public DateTime RequestDate { get; set; }
    }
}
