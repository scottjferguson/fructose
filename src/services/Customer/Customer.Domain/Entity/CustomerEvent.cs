using System;
using System.Collections.Generic;

namespace Customer.Domain.Entity
{
    public partial class CustomerEvent
    {
        public CustomerEvent()
        {
            InverseCustomer = new HashSet<CustomerEvent>();
        }

        public long Id { get; set; }
        public long CustomerId { get; set; }
        public int CustomerEventTypeId { get; set; }
        public DateTime EventDate { get; set; }
        public string Notes { get; set; }
        public bool IsSuppressed { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public byte[] RowVersion { get; set; }

        public virtual CustomerEvent Customer { get; set; }
        public virtual CustomerEventType CustomerEventType { get; set; }
        public virtual ICollection<CustomerEvent> InverseCustomer { get; set; }
    }
}
