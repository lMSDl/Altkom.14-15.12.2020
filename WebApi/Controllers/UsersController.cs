using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Models;
using Service.Interfaces;

namespace WebApi.Controllers
{
    [Authorize(Roles = "admin")]
    public class UsersController : Controller<User>
    {
        public UsersController(ILogger<Controller<User>> logger, IEntityService<User> service) : base(logger, service)
        {
        }

        [HttpGet("{id}", Name = "GetUser")]
        [AllowAnonymous]
        public override Task<IActionResult> Get(int id)
        {
            return base.Get(id);
        }
    }
}