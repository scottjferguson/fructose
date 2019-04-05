using System;
using System.Collections.Generic;

namespace Customer.Domain.Entity
{
    public partial class PersonPhone
    {
        public long Id { get; set; }
        public long PersonId { get; set; }
        public long PhoneId { get; set; }
        public bool? IsActive { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public byte[] RowVersion { get; set; }

        public virtual Person Person { get; set; }
        public virtual Phone Phone { get; set; }
    }
}
