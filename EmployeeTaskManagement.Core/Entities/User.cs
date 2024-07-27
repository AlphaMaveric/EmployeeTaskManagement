namespace EmployeeTaskManagement.Core.Entities
{
    public class User
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public int? ManagerId { get; set; }
        public User Manager { get; set; }
        public ICollection<EmployeeTask> Tasks { get; set; }
    }
}
