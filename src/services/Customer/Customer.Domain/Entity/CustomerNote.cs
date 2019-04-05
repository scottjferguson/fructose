using System;
using System.Collections.Generic;

namespace Customer.Domain.Entity
{
    public partial class CustomerNote
    {
        public long Id { get; set; }
        public long CustomerId { get; set; }
        public int CustomerNoteTypeId { get; set; }
        public string Note { get; set; }
        public bool IsSuppressed { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public byte[] RowVersion { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual CustomerNoteType CustomerNoteType { get; set; }
    }
}
