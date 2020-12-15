using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Models;
using Service.Interfaces;

namespace WebApi.Controllers
{
    public class ProductsController : Controller<Product>
    {
        public ProductsController(ILogger<Controller<Product>> logger, IEntityService<Product> service) : base(logger, service)
        {
        }

        [HttpGet("{id}", Name = "GetProduct")]
        public override Task<IActionResult> Get(int id)
        {
            return base.Get(id);
        }
    }
}