using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeTaskManagement.Core.Entities
{
    public class EmployeeTask
    {
        public int TaskId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public bool IsCompleted { get; set; }
        public string Notes { get; set; }
        public ICollection<EmployeeDocument> Documents { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
