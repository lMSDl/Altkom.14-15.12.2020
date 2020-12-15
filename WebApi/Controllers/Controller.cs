using System;
using System.IO;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Logging;
using Microsoft.Net.Http.Headers;
using Models;
using Service.Interfaces;
using WebApi.Filters;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize(Roles = "Create, Update, Read")]
    //[Produces("application/xml")]  
    public abstract class Controller<T> : ControllerBase where T : Entity
    {
        
        private readonly ILogger<Controller<T>> _logger;
        protected readonly IEntityService<T> _service;

        public Controller(ILogger<Controller<T>> logger, IEntityService<T> service)
        {
            _logger = logger;
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _service.ReadAsync());
        }

        public virtual async Task<IActionResult> Get(int id)
        {
            var entity = await _service.ReadAsync(id);
            if(entity == null)
                return NotFound();
            return Ok(entity);
        }

        
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]T entity) {
            // if(!ModelState.IsValid)
            //     return BadRequest(ModelState);
            entity = await _service.CreateAsync(entity);
            return CreatedAtRoute("Get"+typeof(T).Name, new {id = entity.Id}, entity);
        }

        
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]T entity) {
            if(await _service.ReadAsync(id) == null)
                return NotFound();
            await _service.UpdateAsync(id, entity);
            return NoContent();
        }
        
        
        [HttpDelete("{id}")]
         [ProducesResponseType(StatusCodes.Status204NoContent)]
         [ProducesResponseType(StatusCodes.Status404NotFound)]
         
        [Authorize(Roles = nameof(UserRoles.Delete))]
        public async Task<IActionResult> Delete(int id) {
            if(await _service.ReadAsync(id) == null)
                return NotFound();
            await _service.DeleteAsync(id);
            return NoContent();
        }
    }
}