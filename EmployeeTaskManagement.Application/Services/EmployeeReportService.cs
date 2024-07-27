using AutoMapper;
using EmployeeTaskManagement.Application.DTOs;
using EmployeeTaskManagement.Application.Interfaces;
using EmployeeTaskManagement.Core.Entities;
using EmployeeTaskManagement.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeTaskManagement.Application.Services
{
    public class EmployeeReportService : IEmployeeReportService
    {
        private readonly IRepository<EmployeeTask> _taskRepository;
        private readonly IRepository<User> _userRepository;
        private readonly IMapper _mapper;

        public EmployeeReportService(IRepository<EmployeeTask> taskRepository, IRepository<User> userRepository, IMapper mapper)
        {
            _taskRepository = taskRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<ReportDto> GetWeeklyReportAsync()
        {
            var week = DateTime.Now.AddDays(-7);
            var tasks = await _taskRepository.FindByConditionAsync(t => t.DueDate >= week && t.DueDate <= DateTime.Now);
            return GenerateReport(tasks);
        }
        public async Task<ReportDto> GetMonthlyReportAsync()
        {
            var month = DateTime.Now.AddMonths(-1);
            var tasks = await _taskRepository.FindByConditionAsync(t => t.DueDate >= month && t.DueDate <= DateTime.Now);
            return GenerateReport(tasks);
        }

        public async Task<ReportDto> GetTeamReportAsync(int teamId)
        {
            var teamMembers = await _userRepository.FindByConditionAsync(u => u.ManagerId == teamId);
            var teamMemberIds = teamMembers.Select(tm => tm.UserId).ToList();
            var tasks = await _taskRepository.FindByConditionAsync(t => teamMemberIds.Contains(t.UserId));
            return GenerateReport(tasks);
        }

        private ReportDto GenerateReport(IEnumerable<EmployeeTask> tasks)
        {
            return new ReportDto
            {
                TotalTasks = tasks.Count(),
                CompletedTasks = tasks.Count(t => t.IsCompleted),
                PendingTasks = tasks.Count(t => !t.IsCompleted)
            };
        }

        public async Task<IEnumerable<ManagerTaskReportDto>> GetTeamReportsAsync()
        {
            var users = await _userRepository.GetAllAsync(includes: u => u.Manager);
            var tasks = await _taskRepository.GetAllAsync(includes: t => t.User);

            var reports = users
                .Where(u => u.ManagerId.HasValue)
                .GroupBy(u => u.ManagerId.Value)
                .Select(g => new ManagerTaskReportDto
                {
                    ManagerId = g.Key,
                    ManagerName = users.FirstOrDefault(u => u.UserId == g.Key)?.UserName,
                    TeamTasks = tasks
                        .Where(t => g.Select(u => u.UserId).Contains(t.UserId))
                        .Select(task => _mapper.Map<TaskDto>(task))
                        .ToList(),
                    TotalTasks = tasks.Count(),
                    CompletedTasks = tasks.Count(t => t.IsCompleted),
                    PendingTasks = tasks.Count(t => !t.IsCompleted)
                })
                .ToList();

            return reports;
        }
    }
}
