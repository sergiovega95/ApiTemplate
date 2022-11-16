using Iris.Domain.DTOs.Tasks;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Iris.Domain.DomainServices.Tasks
{
    public interface ITaskService
    {
        Task<IEnumerable<TaskDto>> GetAllTask();
        Task<int> CreateTask(TaskRequest task);
        Task UpdateTask(TaskDto taskUpdate);
        Task DeleteTask(int taskId);

    }
}
