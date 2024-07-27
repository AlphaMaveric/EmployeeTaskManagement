using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeTaskManagement.Application.DTOs
{
    public class ManagerTaskReportDto
    {
        public int ManagerId { get; set; }
        public string ManagerName { get; set; }
        public List<TaskDto> TeamTasks { get; set; }
        public int TotalTasks { get; set; }
        public int CompletedTasks { get; set; }
        public int PendingTasks { get; set; }
    }
}
