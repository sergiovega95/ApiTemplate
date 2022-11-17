using FluentValidation;
using Iris.Api.Controllers;
using Iris.Api.ViewModels.Responses;
using Iris.Domain.DomainServices.Tasks;
using Iris.Domain.DTOs.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Iris.Test.Api.Controllers
{
    public  class TaskControllerTest
    {
     
        private readonly Mock<ILogger<TaskController>> _mockLogger;
        private readonly Mock<IValidator<TaskRequest>> _mockValidatorTaskRequest;
        private readonly Mock<IValidator<TaskDto>> _mockValidatorTaskDto;
        private readonly Mock<ITaskService> _mockTaskService;
        private TaskController _taskController;


        public TaskControllerTest() { 
        
            _mockLogger= new Mock<ILogger<TaskController>>();
            _mockValidatorTaskRequest= new Mock<IValidator<TaskRequest>>();
            _mockValidatorTaskDto = new Mock<IValidator<TaskDto>>();
            _mockTaskService= new Mock<ITaskService>();
            _taskController = new TaskController(_mockLogger.Object,_mockValidatorTaskRequest.Object, _mockValidatorTaskDto.Object,_mockTaskService.Object);


        }

        //Todo Complete Finish Unit testing
        
        [Fact]

        public async Task GetAllTasks_Successfull()
        {
            //assing
            List<TaskDto> tasks = new List<TaskDto>()
            {
                new TaskDto()
                {
                     TaskId= 1,
                     TaskDescription="Task 1"
                },
                new TaskDto()
                {
                    TaskId= 2,
                    TaskDescription="Task 2"
                }
            };            
            _mockTaskService.Setup(s => s.GetAllTask()).ReturnsAsync(tasks);

            //act
            var response = await _taskController.GetAllTasks();

            var data = response as OkObjectResult;

            var body = data.Value as GenericResponse;

            //assert
            Assert.Equal(200,data.StatusCode);
            Assert.Equal(tasks, body.Data);
        }
    }
}
