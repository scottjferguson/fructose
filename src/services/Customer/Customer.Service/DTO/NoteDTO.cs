using Core.Framework.Descriptor;
using System;

namespace Customer.Microservice.DTO
{
    public class NoteDTO : IHaveAnID, IAuditable
    {
        public long ID { get; set; }
        public string NoteType { get; set; }
        public string Value { get; set; }
        public bool IsSuppressed { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
