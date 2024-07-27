namespace EmployeeTaskManagement.Application.DTOs
{
    public class DocumentDto
    {
        public int DocumentId { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public int TaskId { get; set; }
        public DateTime UploadedDate { get; set; }
        public long FileSize { get; set; }
    }
}
