using ApiTemplate.Domain.Exceptions;
using ApiTemplate.Domain.Interfaces;
using Iris.Domain.DTOs.Tasks;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace Iris.Domain.DomainServices.Tasks
{
    public class TaskService : ITaskService {

        public readonly IGenericRepository<Entities.Task> _repository;

        public TaskService(IGenericRepository<Entities.Task> repository) {

            _repository = repository;
        }

        public async Task<int> CreateTask(TaskRequest task) {

            Entities.Task newTask = new Entities.Task()
            {
                TaskDescription = task.TaskDescription,
                DateCreated = DateTime.Now.AddHours(-5)
            };

            await _repository.AddAsync(newTask);

            return newTask.TaskId;
        }

        public async Task DeleteTask(int taskId) {

            Entities.Task task = await _repository.GetByIdAsync(taskId);

            if (task is null) {

                throw new BusinessException($"TaskId {taskId} not found");
            }

            await _repository.RemoveAsync(task);
        }

        public async Task<IEnumerable<TaskDto>> GetAllTask() {

            var data =  await _repository.GetAllAsync();

            return data.Select(x => new TaskDto
            {
                TaskDescription = x.TaskDescription,
                TaskId = x.TaskId
            }).ToList();
        }

        public async Task UpdateTask(TaskDto taskUpdate) {

            Entities.Task task = await _repository.GetByIdAsync(taskUpdate.TaskId);

            if (task is null)
            {
                throw new BusinessException($"TaskId {taskUpdate.TaskId} not found");
            }

            task.TaskDescription = taskUpdate.TaskDescription;

            await _repository.UpdateAsync(task);
        }
    }
}
