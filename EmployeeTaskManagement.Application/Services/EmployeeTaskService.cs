using AutoMapper;
using EmployeeTaskManagement.Application.DTOs;
using EmployeeTaskManagement.Application.Interfaces;
using EmployeeTaskManagement.Core.Entities;
using EmployeeTaskManagement.Core.Interfaces;

namespace EmployeeTaskManagement.Application.Services
{
    public class EmployeeTaskService : IEmployeeTaskService
    {
        private readonly IRepository<EmployeeTask> _taskRepository;
        private readonly IMapper _mapper;

        public EmployeeTaskService(IRepository<EmployeeTask> taskRepository, IMapper mapper)
        {
            _taskRepository = taskRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TaskDto>> GetTasksByUserAsync(int userId)
        {
            var tasks = await _taskRepository.GetAllAsync(
            includes: t => t.Documents,
            filter: null);
            return _mapper.Map<IEnumerable<TaskDto>>(tasks.Where(t => t.UserId == userId));
        }

        public async Task<IEnumerable<TaskDto>> GetAllTasksAsync()
        {
            var tasks = await _taskRepository.GetAllAsync(
            includes: t => t.Documents,
            filter: null);
            return _mapper.Map<IEnumerable<TaskDto>>(tasks);
        }

        public async Task<TaskDto> GetTaskByIdAsync(int id)
        {
            var task = await _taskRepository.GetAllAsync(
            filter: t => t.TaskId == id,
            includes: t => t.Documents
        );
            return _mapper.Map<TaskDto>(task);
        }

        public async Task CreateTaskAsync(TaskDto taskDto)
        {
            var task = _mapper.Map<EmployeeTask>(taskDto);
            await _taskRepository.AddAsync(task);
        }

        public async Task UpdateTaskAsync(int id, TaskDto taskDto)
        {
            var task = _mapper.Map<EmployeeTask>(taskDto);
            task.TaskId = id;
            await _taskRepository.UpdateAsync(task);
        }

        public async Task DeleteTaskAsync(int id)
        {
            var task = await _taskRepository.GetByIdAsync(id);
            if (task != null)
                await _taskRepository.DeleteAsync(task);
        }

        public async Task CompleteTaskAsync(int id)
        {
            var task = await _taskRepository.GetByIdAsync(id);
            if (task != null)
            {
                task.IsCompleted = true;
                await _taskRepository.UpdateAsync(task);
            }
        }

        public async Task AddNotesToTaskAsync(int id, string notes)
        {
            var task = await _taskRepository.GetByIdAsync(id);
            if (task != null)
            {
                task.Notes = notes;
                await _taskRepository.UpdateAsync(task);
            }
        }

    }
}
