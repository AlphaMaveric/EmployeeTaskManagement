using EmployeeTaskManagement.Application.DTOs;
using EmployeeTaskManagement.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeTaskManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly IEmployeeTaskService _taskService;
        private readonly IDocumentService _documentService;

        public TaskController(IEmployeeTaskService taskService, IDocumentService documentService)
        {
            _taskService = taskService;
            _documentService = documentService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateTask([FromBody] TaskDto taskDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _taskService.CreateTaskAsync(taskDto);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTask(int id, [FromBody] TaskDto taskDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _taskService.UpdateTaskAsync(id, taskDto);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            await _taskService.DeleteTaskAsync(id);
            return Ok();
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetTasksByUser(int userId)
        {
            var tasks = await _taskService.GetTasksByUserAsync(userId);
            return Ok(tasks);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskDto>>> GetAllTasks()
        {
            var tasks = await _taskService.GetAllTasksAsync();
            return Ok(tasks);
        }

        [HttpPut("{id}/complete")]
        public async Task<ActionResult> CompleteTask(int id)
        {
            await _taskService.CompleteTaskAsync(id);
            return NoContent();
        }

        [HttpPut("{id}/notes")]
        public async Task<ActionResult> AddNotesToTask(int id, [FromBody] string notes)
        {
            await _taskService.AddNotesToTaskAsync(id, notes);
            return NoContent();
        }

        [HttpPost("{taskId}/documents")]
        public async Task<ActionResult> AddDocumentToTask(int taskId, IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("No file uploaded.");
            }

            await _documentService.AddDocumentAsync(file, taskId);
            return Ok();
        }

        [HttpDelete("documents/{documentId}")]
        public async Task<ActionResult> DeleteDocumentFromTask(int documentId)
        {
            await _documentService.DeleteDocumentAsync(documentId);
            return NoContent();
        }

        [HttpGet("{taskId}/documents")]
        public async Task<ActionResult<IEnumerable<DocumentDto>>> GetDocumentsByTask(int taskId)
        {
            var documents = await _documentService.GetDocumentsByTaskAsync(taskId);
            return Ok(documents);
        }

        [HttpGet("documents/{documentId}/download")]
        public async Task<IActionResult> DownloadDocument(int documentId)
        {
            var fileData = await _documentService.GetDocumentFileAsync(documentId);
            if (fileData == null)
            {
                return NotFound();
            }

            var document = await _documentService.GetDocumentByIdAsync(documentId);
            return File(fileData, "application/octet-stream", document.FileName);
        }

    }
}
