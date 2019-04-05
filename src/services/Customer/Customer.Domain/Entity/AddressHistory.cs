using System;
using System.Collections.Generic;

namespace Customer.Domain.Entity
{
    public partial class AddressHistory
    {
        public long Id { get; set; }
        public long AddressId { get; set; }
        public int AddressTypeId { get; set; }
        public long? GeographyId { get; set; }
        public string Line1 { get; set; }
        public string Line2 { get; set; }
        public string Line3 { get; set; }
        public string City { get; set; }
        public string StateProv { get; set; }
        public string PostalCode { get; set; }
        public string CountryCode { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }

        public virtual Address Address { get; set; }
    }
}
