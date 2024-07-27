using AutoMapper;
using EmployeeTaskManagement.Application.DTOs;
using EmployeeTaskManagement.Application.Interfaces;
using EmployeeTaskManagement.Core.Entities;
using EmployeeTaskManagement.Core.Interfaces;
using Microsoft.AspNetCore.Http;


namespace EmployeeTaskManagement.Application.Services
{
    public class DocumentService : IDocumentService
    {
        private readonly IRepository<EmployeeDocument> _documentRepository;
        private readonly IMapper _mapper;
        private readonly string _storagePath;

        public DocumentService(IRepository<EmployeeDocument> documentRepository, IMapper mapper)
        {
            _documentRepository = documentRepository;
            _mapper = mapper;
            _storagePath = Path.Combine(AppContext.BaseDirectory, "UploadedFiles");
            if (!Directory.Exists(_storagePath))
            {
                Directory.CreateDirectory(_storagePath);
            }
        }

        public async Task<IEnumerable<DocumentDto>> GetAllDocumentsAsync()
        {
            var documents = await _documentRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<DocumentDto>>(documents);
        }

        public async Task<DocumentDto> GetDocumentByIdAsync(int id)
        {
            var document = await _documentRepository.GetByIdAsync(id);
            return _mapper.Map<DocumentDto>(document);
        }

        public async Task<IEnumerable<DocumentDto>> GetDocumentsByTaskAsync(int taskId)
        {
            var documents = await _documentRepository.FindByConditionAsync(d => d.TaskId == taskId);
            return _mapper.Map<IEnumerable<DocumentDto>>(documents);
        }

        public async Task AddDocumentAsync(IFormFile file, int taskId)
        {
            if (file.Length > 0)
            {
                var filePath = Path.Combine(_storagePath, file.FileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                var document = new EmployeeDocument
                {
                    FileName = file.FileName,
                    FilePath = filePath,
                    TaskId = taskId
                };

                await _documentRepository.AddAsync(document);
            }
        }

        public async Task DeleteDocumentAsync(int id)
        {
            var document = await _documentRepository.GetByIdAsync(id);
            if (document != null)
            {
                if (File.Exists(document.FilePath))
                {
                    File.Delete(document.FilePath);
                }
                await _documentRepository.DeleteAsync(document);
            }
        }

        public async Task<byte[]> GetDocumentFileAsync(int id)
        {
            var document = await _documentRepository.GetByIdAsync(id);
            if (document != null && System.IO.File.Exists(document.FilePath))
            {
                return await File.ReadAllBytesAsync(document.FilePath);
            }
            return null;
        }
    }
}