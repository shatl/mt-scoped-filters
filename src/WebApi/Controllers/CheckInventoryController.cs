namespace WebApi.Controllers
{
    using System.Threading.Tasks;
    using Contracts;
    using MassTransit;
    using Microsoft.AspNetCore.Mvc;


    [ApiController]
    [Route("[controller]")]
    public class CheckInventoryController :
        ControllerBase
    {
        private readonly IBus _bus;

        public CheckInventoryController(IBus bus)
        {
            _bus = bus;
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddInventory model)
        {
            await _bus.Publish(model);
            return Accepted();
        }
    }
}
