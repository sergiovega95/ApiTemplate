using ApiTemplate.Api.Controllers;
using ApiTemplate.Api.ViewModels;
using ApiTemplate.Domain.DTOs.Authentication;
using FluentValidation;
using Iris.Domain.DomainServices.Tasks;
using Iris.Domain.DTOs.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Iris.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TaskController : ControllerBase
    {
        private readonly ILogger<TaskController> _logger;
        private readonly IValidator<TaskDto> _validator;
        private readonly ITaskService _taskService;

        public TaskController(ILogger<TaskController> logger, IValidator<TaskDto> validator, ITaskService taskService)
        {
            _validator= validator;
            _logger= logger;
            _taskService= taskService;

        }

        // GET: api/<TaskController>
        [HttpGet]
        public async Task <IActionResult> GetAllTasks()
        {
            _logger.LogTrace("Gettings all tasks");

            return Ok(await _taskService.GetAllTask());
        }       

        // POST api/<TaskController>
        [HttpPost]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK, contentType: "application/json")]
        [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status400BadRequest, contentType: "application/json")]
        [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status500InternalServerError, contentType: "application/json")]
        public async Task <IActionResult> Post ([FromBody] TaskRequest task)
        {
            _logger.LogTrace("Creating a task");

            return Ok(await _taskService.CreateTask());

        }

        // PUT api/<TaskController>
        [HttpPut]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK, contentType: "application/json")]
        [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status400BadRequest, contentType: "application/json")]
        [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status500InternalServerError, contentType: "application/json")]
        public async Task Put([FromBody] TaskDto taskUpdated)
        {
            _logger.LogTrace("Updating a task");

        }

        // DELETE api/<TaskController>/5
        [HttpDelete("{id}")]        
        [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status400BadRequest, contentType: "application/json")]
        [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status500InternalServerError, contentType: "application/json")]
        public async Task Delete(int taskId)
        {
            _logger.LogTrace("Deleting a task");
        }
    }
}
