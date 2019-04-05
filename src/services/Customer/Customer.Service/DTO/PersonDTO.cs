using Core.Framework.Descriptor;
using System;
using System.Collections.Generic;

namespace Customer.Microservice.DTO
{
    public class PersonDTO : IHaveAnID, IAuditable
    {
        public long ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public List<AddressDTO> Addresses { get; set; }
        public List<EmailDTO> Emails { get; set; }
        public List<PhoneDTO> Phones { get; set; }
    }
}
