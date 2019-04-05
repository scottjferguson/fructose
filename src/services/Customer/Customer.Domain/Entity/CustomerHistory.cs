using System;
using System.Collections.Generic;

namespace Customer.Domain.Entity
{
    public partial class CustomerHistory
    {
        public long Id { get; set; }
        public long CustomerId { get; set; }
        public long OrganizationId { get; set; }
        public long PersonId { get; set; }
        public int CustomerStatusId { get; set; }
        public int CustomerTypeId { get; set; }
        public string CustomerNumber { get; set; }
        public DateTime JoinDate { get; set; }
        public bool IsActive { get; set; }
        public string ReferenceIdentifier { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }

        public virtual Customer Customer { get; set; }
    }
}
