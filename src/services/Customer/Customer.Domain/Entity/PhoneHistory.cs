using System;
using System.Collections.Generic;

namespace Customer.Domain.Entity
{
    public partial class PhoneHistory
    {
        public long Id { get; set; }
        public long PhoneId { get; set; }
        public int PhoneTypeId { get; set; }
        public string PhoneNumber { get; set; }
        public bool? IsValidated { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }

        public virtual Phone Phone { get; set; }
    }
}
