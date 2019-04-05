using Core.Framework.Descriptor;
using System;

namespace Customer.Microservice.DTO
{
    public class PhoneDTO : IHaveAnID, IAuditable
    {
        public long ID { get; set; }
        public string PhoneType { get; set; }
        public string Value { get; set; }
        public string ValueNumeric { get; set; }
        public bool IsValidated { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
