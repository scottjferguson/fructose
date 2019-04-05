using System;
using System.Collections.Generic;

namespace Customer.Domain.Entity
{
    public partial class Person
    {
        public Person()
        {
            PersonAddress = new HashSet<PersonAddress>();
            PersonEmail = new HashSet<PersonEmail>();
            PersonHistory = new HashSet<PersonHistory>();
            PersonPhone = new HashSet<PersonPhone>();
        }

        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool? IsActive { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public byte[] RowVersion { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual ICollection<PersonAddress> PersonAddress { get; set; }
        public virtual ICollection<PersonEmail> PersonEmail { get; set; }
        public virtual ICollection<PersonHistory> PersonHistory { get; set; }
        public virtual ICollection<PersonPhone> PersonPhone { get; set; }
    }
}
