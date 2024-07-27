using EmployeeTaskManagement.Application.DTOs;

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
