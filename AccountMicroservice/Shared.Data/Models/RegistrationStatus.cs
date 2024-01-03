using System;
using System.Collections.Generic;

namespace Shared.Data.Models
{
    public partial class RegistrationStatus
    {
        public RegistrationStatus()
        {
            Registrations = new HashSet<Registration>();
        }

        public byte Id { get; set; }
        public string Code { get; set; } = null!;
        public string Name { get; set; } = null!;

        public virtual ICollection<Registration> Registrations { get; set; }
    }
}
