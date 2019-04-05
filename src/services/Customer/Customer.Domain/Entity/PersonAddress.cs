using System;
using System.Collections.Generic;

namespace Customer.Domain.Entity
{
    public partial class PersonAddress
    {
        public long Id { get; set; }
        public long PersonId { get; set; }
        public long AddressId { get; set; }
        public bool? IsPhysical { get; set; }
        public bool? IsShipping { get; set; }
        public bool? IsBilling { get; set; }
        public bool? IsActive { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public byte[] RowVersion { get; set; }

        public virtual Address Address { get; set; }
        public virtual Person Person { get; set; }
    }
}
