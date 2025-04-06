using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Application.Features.Students.Commands.CreateStudent;
using Application.Features.Students.Commands.ImportStudents;
using CsvHelper;
using Domain.Entities.Faculties;
using Domain.Entities.LearningPrograms;
using Domain.Entities.Statuses;
using Domain.Entities.Students;
using Domain.Repositories;
using SharedKernel;

namespace Infrastructure.Services.ImportStudents
{
    public class CsvStudentImportStrategy : IStudentImportStrategy
    {
        private readonly IUnitOfWork _unitOfWork;

        public CsvStudentImportStrategy(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<IEnumerable<Student>>> ImportStudentsAsync(Stream fileStream)
        {
            using var reader = new StreamReader(fileStream);
            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
            // Parse CSV into DTOs
            var dtos = csv.GetRecords<CreateStudentDto>().ToList();

            var students = new List<Student>();

            foreach (var dto in dtos)
            {
                // Check if the student already exists
                var existingStudent = await _unitOfWork.Students.GetStudentByStudentId(dto.studentId);
                if (existingStudent != null)
                {
                    // For this example, we return a Conflict error.
                    // Alternatively, you might skip this record and continue.
                    return Result.Failure<IEnumerable<Student>>(StudentError.StudentHasExisted);
                }

                // Retrieve related Faculty if specified
                Faculty? faculty = null;
                if (dto.facultyId != Guid.Empty)
                {
                    var existingFaculty = await _unitOfWork.Faculties.GetById((Guid)dto.facultyId!);
                    if (existingFaculty == null)
                        return Result.Failure<IEnumerable<Student>>(FacultyError.FacultyNotFound);
                    faculty = existingFaculty;
                }

                // Retrieve related Learning Program if specified
                LearningProgram? program = null;
                if (dto.programId != Guid.Empty)
                {
                    var existingProgram = await _unitOfWork.LearningPrograms.GetById((Guid)dto.programId!);
                    if (existingProgram == null)
                        return Result.Failure<IEnumerable<Student>>(LearningProgramError.LearningProgramNotFound);
                    program = existingProgram;
                }

                // Retrieve related Status if specified
                Status? status = null;
                if (dto.statusId != Guid.Empty)
                {
                    var existingStatus = await _unitOfWork.Statuses.GetById((Guid)dto.statusId!);
                    if (existingStatus == null)
                        return Result.Failure<IEnumerable<Student>>(StatusError.StatusNotFound);
                    status = existingStatus;
                }

                // Create the student using your domain method.
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