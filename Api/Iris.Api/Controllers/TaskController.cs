using ApiTemplate.Api.ViewModels;
using FluentValidation;
using Iris.Api.ViewModels.Responses;
using Iris.Domain.DomainServices.Tasks;
using Iris.Domain.DTOs.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Iris.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TaskController : ControllerBase
    {
        private readonly ILogger<TaskController> _logger;
        private readonly IValidator<TaskRequest> _validatorTaskRequest;
        private readonly IValidator<TaskDto> _validatorTaskDto;
        private readonly ITaskService _taskService;

        public TaskController(ILogger<TaskController> logger, IValidator<TaskRequest> validatorTaskRequest, 
                               IValidator<TaskDto> validatorTaskDto,
                                ITaskService taskService)
        {

            _validatorTaskRequest = validatorTaskRequest;
            _logger= logger;
            _taskService= taskService;
            _validatorTaskDto= validatorTaskDto;

        }

        // GET: api/<TaskController>
        [HttpGet]
        [ProducesResponseType(typeof(GenericResponse), StatusCodes.Status200OK, contentType: "application/json")]        
        [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status500InternalServerError, contentType: "application/json")]
        public async Task <IActionResult> GetAllTasks()
        {
            _logger.LogTrace("Gettings all tasks");

            return Ok(new GenericResponse() { StatusCode = 200, Data = await _taskService.GetAllTask() });
        }       

        // POST api/<TaskController>
        [HttpPost]
        [ProducesResponseType(typeof(GenericResponse), StatusCodes.Status200OK, contentType: "application/json")]
        [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status400BadRequest, contentType: "application/json")]
        [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status500InternalServerError, contentType: "application/json")]
        public async Task <IActionResult> Post ([FromBody] TaskRequest task)
        {
            _logger.LogTrace("Creating a task");

            await _validatorTaskRequest.ValidateAndThrowAsync(task);

            return Ok(new GenericResponse() { StatusCode = 200, Data = new { taskId = await _taskService.CreateTask(task)}});                    

        }

        // PUT api/<TaskController>
        [HttpPut]
        [ProducesResponseType(typeof(GenericResponse), StatusCodes.Status200OK, contentType: "application/json")]
        [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status400BadRequest, contentType: "application/json")]
        [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status500InternalServerError, contentType: "application/json")]
        public async Task<IActionResult> Put([FromBody] TaskDto taskUpdated)
        {
            _logger.LogTrace("Updating a task");

            await _validatorTaskDto.ValidateAndThrowAsync(taskUpdated);

            await _taskService.UpdateTask(taskUpdated);

            return Ok(new GenericResponse() { StatusCode = 200 , Data = new { taskIdUpdated = taskUpdated.TaskId } });

        }

        // DELETE api/<TaskController>/{id}
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(GenericResponse), StatusCodes.Status200OK, contentType: "application/json")]
        [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status400BadRequest, contentType: "application/json")]
        [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status500InternalServerError, contentType: "application/json")]
        public async Task<IActionResult> Delete(int id)
        {
            _logger.LogTrace("Deleting a task");

            await _taskService.DeleteTask(id);

            return Ok(new GenericResponse() { StatusCode = 200 , Data = new { taskIdDeleted = id }});
        }
    }
}
