using EmployeeTaskManagement.Application.DTOs;
using Microsoft.AspNetCore.Http;

namespace EmployeeTaskManagement.Application.Interfaces
{
    public interface IDocumentService
    {
        Task<IEnumerable<DocumentDto>> GetAllDocumentsAsync();
        Task<DocumentDto> GetDocumentByIdAsync(int id);
        Task<IEnumerable<DocumentDto>> GetDocumentsByTaskAsync(int taskId);
        Task AddDocumentAsync(IFormFile file, int taskId);
        Task DeleteDocumentAsync(int id);
        Task<byte[]> GetDocumentFileAsync(int id);
    }
}
