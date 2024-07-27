using EmployeeTaskManagement.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeTaskManagement.Application.Interfaces
{
    public interface IEmployeeTaskService
    {
        Task<IEnumerable<TaskDto>> GetAllTasksAsync();
        Task<IEnumerable<TaskDto>> GetTasksByUserAsync(int userId);
        Task<TaskDto> GetTaskByIdAsync(int id);
        Task CreateTaskAsync(TaskDto taskDto);
        Task UpdateTaskAsync(int id, TaskDto taskDto);
        Task DeleteTaskAsync(int id);
        Task CompleteTaskAsync(int id);
        Task AddNotesToTaskAsync(int id, string notes);
    }
}
