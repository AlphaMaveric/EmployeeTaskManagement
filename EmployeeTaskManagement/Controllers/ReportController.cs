using EmployeeTaskManagement.Application.DTOs;
using EmployeeTaskManagement.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeTaskManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly IEmployeeReportService _reportService;

        public ReportController(IEmployeeReportService reportService)
        {
            _reportService = reportService;
        }

        [HttpGet("weekly")]
        public async Task<ActionResult<ReportDto>> GetWeeklyReport()
        {
            var report = await _reportService.GetWeeklyReportAsync();
            return Ok(report);
        }

        [HttpGet("monthly")]
        public async Task<ActionResult<ReportDto>> GetMonthlyReport()
        {
            var report = await _reportService.GetMonthlyReportAsync();
            return Ok(report);
        }

        [HttpGet("team/{teamId}")]
        public async Task<ActionResult<ReportDto>> GetTeamReport(int teamId)
        {
            var report = await _reportService.GetTeamReportAsync(teamId);
            return Ok(report);
        }

        [HttpGet("team-reports")]
        public async Task<IActionResult> GetTeamReports()
        {
            var reports = await _reportService.GetTeamReportsAsync();
            return Ok(reports);
        }

    }
}
