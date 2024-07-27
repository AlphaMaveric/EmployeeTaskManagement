using EmployeeTaskManagement.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeTaskManagement.Application.Interfaces
{
    public interface IEmployeeReportService
    {
        Task<ReportDto> GetWeeklyReportAsync();
        Task<ReportDto> GetMonthlyReportAsync();
        Task<ReportDto> GetTeamReportAsync(int teamId);
        Task<IEnumerable<ManagerTaskReportDto>> GetTeamReportsAsync();
    }
}
