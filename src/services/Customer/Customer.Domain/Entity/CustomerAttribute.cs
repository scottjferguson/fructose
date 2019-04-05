using System;
using System.Collections.Generic;

namespace Customer.Domain.Entity
{
    public partial class CustomerAttribute
    {
        public long Id { get; set; }
        public long CustomerId { get; set; }
        public int CustomerAttributeTypeId { get; set; }
        public string AttributeValue { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public byte[] RowVersion { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual CustomerAttributeType CustomerAttributeType { get; set; }
    }
}
