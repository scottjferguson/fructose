using System;
using System.Collections.Generic;

namespace Customer.Domain.Entity
{
    public partial class PersonHistory
    {
        public long Id { get; set; }
        public long PersonId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }

        public virtual Person Person { get; set; }
    }
}
