using CourseMate.Models;
using CourseMate.Models.Context;
using CourseMate.Models.Dtos;
using CourseMate.Models.HelpingClasses;
using CourseMate.Repository;
using Microsoft.EntityFrameworkCore;

namespace CourseMate.Services
{
    public interface IStudentRepositoryService
    {
        Task<ServiceResult<Student>> AddNewStudent(StudentDto? dto);
        Task<ServiceResult<Student>> GetActiveStudentByEmail(string email);
    }
    public class StudentRepositoryService : IStudentRepositoryService
    {
        private readonly IStudentRepository _studentRepo;
        public StudentRepositoryService(IStudentRepository studentRepo)
        {
            _studentRepo = studentRepo;
        }

        public async Task<ServiceResult<Student>> AddNewStudent(StudentDto? dto)
        {
            if (dto == null)
                return ServiceResult<Student>.Failure("Student data is required.");

            var studentExists = await _studentRepo.GetStudentById(dto.Id);
            if (studentExists != null)
            {
                return ServiceResult<Student>.Failure("Student with this ID already exists.");
            }

            var student = new Student
            {
                Id = dto.Id == Guid.Empty ? Guid.NewGuid() : dto.Id,
                FirstName = dto.FirstName ?? string.Empty,
                LastName = dto.LastName ?? string.Empty,
                Email = dto.Email ?? string.Empty,
                Password = dto.Password ?? string.Empty,
                PhoneNumber = dto.PhoneNumber ?? string.Empty,
                DateOfBirth = dto.DateOfBirth,
                Major = dto.Major ?? string.Empty,
                Year = dto.Year > 0 ? dto.Year : DateTime.Now.Year,
                DepartmentId = dto.DepartmentId,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                IsDeleted = false,
                IsActive = 1,
                Role = dto.Role
            };

            await _studentRepo.AddStudent(student);
            return ServiceResult<Student>.Success(student);
        }

        public async Task<ServiceResult<Student>> GetActiveStudentByEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
                return ServiceResult<Student>.Failure("Email must be provided.");

            var student = await _studentRepo.GetActiveStudentByEmail(email);

            if (student != null)
                return ServiceResult<Student>.Success(student);
            else
                return ServiceResult<Student>.Failure("No active student found with the provided email.");
        }
    }

}