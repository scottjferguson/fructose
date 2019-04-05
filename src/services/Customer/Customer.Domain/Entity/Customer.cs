using System;
using System.Collections.Generic;

namespace Customer.Domain.Entity
{
    public partial class Customer
    {
        public Customer()
        {
            CustomerAttribute = new HashSet<CustomerAttribute>();
            CustomerHistory = new HashSet<CustomerHistory>();
            CustomerNote = new HashSet<CustomerNote>();
            CustomerSearch = new HashSet<CustomerSearch>();
        }

        public long Id { get; set; }
        public long OrganizationId { get; set; }
        public long PersonId { get; set; }
        public int CustomerStatusId { get; set; }
        public int CustomerTypeId { get; set; }
        public string CustomerNumber { get; set; }
        public DateTime JoinDate { get; set; }
        public bool? IsActive { get; set; }
        public string ReferenceIdentifier { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public byte[] RowVersion { get; set; }

        public virtual CustomerStatus CustomerStatus { get; set; }
        public virtual CustomerType CustomerType { get; set; }
        public virtual Person Person { get; set; }
        public virtual ICollection<CustomerAttribute> CustomerAttribute { get; set; }
        public virtual ICollection<CustomerHistory> CustomerHistory { get; set; }
        public virtual ICollection<CustomerNote> CustomerNote { get; set; }
        public virtual ICollection<CustomerSearch> CustomerSearch { get; set; }
    }
}
