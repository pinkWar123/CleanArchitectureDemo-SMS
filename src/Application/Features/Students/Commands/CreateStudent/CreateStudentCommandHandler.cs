using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Abstractions.Messaging;
using Domain.Entities.Faculties;
using Domain.Entities.LearningPrograms;
using Domain.Entities.Statuses;
using Domain.Entities.Students;
using Domain.Repositories;
using Microsoft.VisualBasic;
using SharedKernel;

namespace Application.Features.Students.Commands.CreateStudent
{
    public class CreateStudentCommandHandler : ICommandHandler<CreateStudentCommand, Student>
    {
        private readonly IUnitOfWork _unitOfWork;
        public CreateStudentCommandHandler(IUnitOfWork unitOrWork)
        {
            _unitOfWork = unitOrWork;
        }
        public async Task<Result<Student>> Handle(CreateStudentCommand request, CancellationToken cancellationToken)
        {
            var studentId = request.dto.studentId;
            var existingStudent = await _unitOfWork.Students.GetStudentByStudentId(studentId);
            if(existingStudent != null)
            {
                return Result.Failure<Student>(StudentError.StudentHasExisted);
            }

            Faculty? faculty = null;
            if(request.dto.facultyId.HasValue && request.dto.facultyId != Guid.Empty)
            {
                var existingFaculty = await _unitOfWork.Faculties.GetById((Guid)request.dto.facultyId!);
                if(existingFaculty == null) return Result.Failure<Student>(FacultyError.FacultyNotFound);
                faculty = existingFaculty;
            }

            LearningProgram? program = null;
            if(request.dto.programId.HasValue && request.dto.programId != Guid.Empty)
            {
                var existingProgram = await _unitOfWork.LearningPrograms.GetById((Guid)request.dto.programId!);
                if(existingProgram == null) return Result.Failure<Student>(LearningProgramError.LearningProgramNotFound);
                program = existingProgram;
            }

            Status? status = null;
            if(request.dto.statusId.HasValue && request.dto.statusId != Guid.Empty)
            {
                var existingStatus = await _unitOfWork.Statuses.GetById((Guid) request.dto.statusId!);
                if(existingStatus == null) return Result.Failure<Student>(StatusError.StatusNotFound);
                status = existingStatus;
            }

            var student = Student.Create(
                studentId,
                request.dto.name,
                request.dto.dateObBirth,
                request.dto.gender,
                request.dto.address,
                request.dto.email,
                request.dto.phoneNumber,
                faculty,
                program,
                status
            );
            if(student.IsFailure) return Result.Failure<Student>(student.Error);

            await _unitOfWork.Students.Create(student.Value);
            await _unitOfWork.SaveChangesAsync();

            return Result.Success<Student>(student.Value);
        }
    }
}