using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Models;
using Service.Interfaces;

namespace WebApi.Controllers
{
    public class OrdersController : Controller<Order>
    {
        public OrdersController(ILogger<Controller<Order>> logger, IEntityService<Order> service) : base(logger, service)
        {
        }

        [HttpGet("{id}", Name = "GetOrder")]
        public override Task<IActionResult> Get(int id)
        {
            return base.Get(id);
        }

        [HttpGet("~/Users/{userId}/[controller]")]
        public async Task<IActionResult> GetForUser(int userId)
        {
            var orders = await _service.ReadAsync();
            orders = orders.Where(x => x.User.Id == userId);
            return Ok(orders);
        }
    }
}