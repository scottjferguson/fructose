using System;
using System.Collections.Generic;

namespace Customer.Domain.Entity
{
    public partial class Address
    {
        public Address()
        {
            AddressHistory = new HashSet<AddressHistory>();
            PersonAddress = new HashSet<PersonAddress>();
        }

        public long Id { get; set; }
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
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public byte[] RowVersion { get; set; }

        public virtual AddressType AddressType { get; set; }
        public virtual ICollection<AddressHistory> AddressHistory { get; set; }
        public virtual ICollection<PersonAddress> PersonAddress { get; set; }
    }
}
