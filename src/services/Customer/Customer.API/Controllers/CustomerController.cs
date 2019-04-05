using Customer.Microservice;
using Customer.Microservice.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Customer.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        // GET api/v1/customer/5
        [HttpGet("{id:long}")]
        [ProducesResponseType(typeof(CustomerDTO), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<CustomerDTO>> Get(long id)
        {
            try
            {
                CustomerDTO customerDTO = await _customerService.GetByIDAsync(id);

                return Ok(customerDTO);
            }
            catch
            {
                return NotFound();
            }
        }

        // POST api/v1/customer
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/v1/customer/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/v1/customer/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        //[Route("create")]
        //[HttpPut]
        //[ProducesResponseType((int)HttpStatusCode.OK)]
        //[ProducesResponseType((int)HttpStatusCode.BadRequest)]
        //public IActionResult Create()
        //{
        //    return Ok();
        //}

        //// GET api/v1/customer
        //[HttpGet]
        //public ActionResult<IEnumerable<string>> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}
    }
}