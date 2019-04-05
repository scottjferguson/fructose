using System;
using System.Collections.Generic;

namespace Customer.Domain.Entity
{
    public partial class Phone
    {
        public Phone()
        {
            PersonPhone = new HashSet<PersonPhone>();
            PhoneHistory = new HashSet<PhoneHistory>();
        }

        public long Id { get; set; }
        public int PhoneTypeId { get; set; }
        public string PhoneNumber { get; set; }
        public string PhoneNumberNumeric { get; set; }
        public bool? IsValidated { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public byte[] RowVersion { get; set; }

        public virtual PhoneType PhoneType { get; set; }
        public virtual ICollection<PersonPhone> PersonPhone { get; set; }
        public virtual ICollection<PhoneHistory> PhoneHistory { get; set; }
    }
}
