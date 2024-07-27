using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeTaskManagement.Core.Entities
{
    public class EmployeeDocument
    {
        public int DocumentId { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public int TaskId { get; set; }
        public DateTime UploadedDate { get; set; }
        public long FileSize { get; set; }
 
        public EmployeeTask Task { get; set; }
    }
}
