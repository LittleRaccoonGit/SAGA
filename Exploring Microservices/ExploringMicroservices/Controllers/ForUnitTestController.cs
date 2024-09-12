using ExploringMicroservices.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace ExploringMicroservices.Controllers
{
    public class ForUnitTestController : Controller
    {
        public IOrderExampleRepository _OrderExample { get; set; }
        public ForUnitTestController(IOrderExampleRepository OrderExample) 
        {
            _OrderExample = OrderExample;
        }

        [HttpGet("{id}", Name = "GetOrderById")]
        public IActionResult GetById(Guid id)
        {
            var item = _OrderExample.GetAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            return Ok(item);
            //return new ObjectResult(item);
        }

    }
}
