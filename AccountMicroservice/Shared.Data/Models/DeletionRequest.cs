using System;
using System.Collections.Generic;

namespace Shared.Data.Models
{
    public partial class DeletionRequest
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Reason { get; set; } = null!;
        public DateTime DateCreated { get; set; }

        public virtual User User { get; set; } = null!;
    }
}
