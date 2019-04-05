using Core.Framework.Services;
using Customer.Microservice.DTO;

namespace Customer.Microservice
{
    public interface ICustomerService : ICrudService<CustomerDTO> { }
}
