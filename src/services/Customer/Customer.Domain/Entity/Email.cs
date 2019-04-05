using System;
using System.Collections.Generic;

namespace Customer.Domain.Entity
{
    public partial class Email
    {
        public Email()
        {
            EmailHistory = new HashSet<EmailHistory>();
            PersonEmail = new HashSet<PersonEmail>();
        }

        public long Id { get; set; }
        public int EmailTypeId { get; set; }
        public string EmailAddress { get; set; }
        public bool? IsValidated { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public byte[] RowVersion { get; set; }

        public virtual EmailType EmailType { get; set; }
        public virtual ICollection<EmailHistory> EmailHistory { get; set; }
        public virtual ICollection<PersonEmail> PersonEmail { get; set; }
    }
}
