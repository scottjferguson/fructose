using System;
using System.Collections.Generic;

namespace Customer.Domain.Entity
{
    public partial class CustomerSearch
    {
        public long Id { get; set; }
        public long CustomerId { get; set; }
        public int SearchTermTypeId { get; set; }
        public string SearchTerm { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual CustomerSearchTermType SearchTermType { get; set; }
    }
}
