namespace EmployeeTaskManagement.Core.Entities
{
    public class EmployeeDocument
    {
        public int DocumentId { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public int TaskId { get; set; }
        public EmployeeTask Task { get; set; }
    }
}
