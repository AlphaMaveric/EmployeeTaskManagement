using EmployeeTaskManagement.Application.DTOs;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
