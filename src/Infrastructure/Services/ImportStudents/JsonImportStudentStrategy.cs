using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Application.Features.Students.Commands.CreateStudent;
using Application.Features.Students.Commands.ImportStudents;
using Domain.Entities.Faculties;
using Domain.Entities.LearningPrograms;
using Domain.Entities.Statuses;
using Domain.Entities.Students;
using Domain.Repositories;
using SharedKernel;

namespace Infrastructure.Services.ImportStudents
{
    public class JsonStudentImportStrategy : IStudentImportStrategy
    {
        private readonly IUnitOfWork _unitOfWork;

        public JsonStudentImportStrategy(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<IEnumerable<Student>>> ImportStudentsAsync(Stream fileStream)
        {
            // Configure JsonSerializer options to match camelCase properties in DTO.
            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                PropertyNameCaseInsensitive = true
            };

            // Deserialize the JSON stream into a list of CreateStudentDto.
            var dtos = await JsonSerializer.DeserializeAsync<List<CreateStudentDto>>(fileStream, options);
            if (dtos == null)
                return Result.Failure<IEnumerable<Student>>(ImportError.InvalidFormat);

            var students = new List<Student>();

            foreach (var dto in dtos)
            {
                // Check if a student with the given studentId already exists.
                var existingStudent = await _unitOfWork.Students.GetStudentByStudentId(dto.studentId);
                if (existingStudent != null)
                {
                    // For this example, return a conflict error; alternatively, you may skip duplicates.
                    return Result.Failure<IEnumerable<Student>>(StudentError.StudentHasExisted);
                }

                // Retrieve Faculty if provided.
                Faculty? faculty = null;
                if (dto.facultyId != Guid.Empty)
                {
                    var existingFaculty = await _unitOfWork.Faculties.GetById((Guid)dto.facultyId!);
                    if (existingFaculty == null)
                        return Result.Failure<IEnumerable<Student>>(FacultyError.FacultyNotFound);
                    faculty = existingFaculty;
                }

                // Retrieve LearningProgram if provided.
                LearningProgram? program = null;
                if (dto.programId != Guid.Empty)
                {
                    var existingProgram = await _unitOfWork.LearningPrograms.GetById((Guid)dto.programId!);
                    if (existingProgram == null)
                        return Result.Failure<IEnumerable<Student>>(LearningProgramError.LearningProgramNotFound);
                    program = existingProgram;
                }

                // Retrieve Status if provided.
                Status? status = null;
                if (dto.statusId != Guid.Empty)
                {
                    var existingStatus = await _unitOfWork.Statuses.GetById((Guid)dto.statusId!);
                    if (existingStatus == null)
                        return Result.Failure<IEnumerable<Student>>(StatusError.StatusNotFound);
                    status = existingStatus;
                }

                // Create the student using your domain's factory method.
                var studentResult = Student.Create(
                    dto.studentId,
                    dto.name,
                    dto.dateObBirth,
                    dto.gender,
                    dto.address,
                    dto.email,
                    dto.phoneNumber,
                    faculty,
                    program,
                    status
                );

                if (studentResult.IsFailure)
                    return Result.Failure<IEnumerable<Student>>(studentResult.Error);

                students.Add(studentResult.Value);
            }

            return Result.Success<IEnumerable<Student>>(students);
        }
    }
}