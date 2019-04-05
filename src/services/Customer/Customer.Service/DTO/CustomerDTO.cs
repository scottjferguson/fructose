using System;
using System.Collections.Generic;

namespace Customer.Microservice.DTO
{
    public class CustomerDTO : PersonDTO
    {
        public long OrganizationId { get; set; }
        public string Status { get; set; }
        public string Type { get; set; }
        public string CustomerNumber { get; set; }
        public DateTime JoinDate { get; set; }
        public string ReferenceIdentifier { get; set; }
        
        public List<AttributeDTO> Attributes { get; set; }
        public List<NoteDTO> Notes { get; set; }
    }
}
