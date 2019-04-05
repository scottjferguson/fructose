using System;
using System.Collections.Generic;

namespace Customer.Domain.Entity
{
    public partial class EmailHistory
    {
        public long Id { get; set; }
        public long EmailId { get; set; }
        public int EmailTypeId { get; set; }
        public string EmailAddress { get; set; }
        public bool? IsValidated { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }

        public virtual Email Email { get; set; }
    }
}
