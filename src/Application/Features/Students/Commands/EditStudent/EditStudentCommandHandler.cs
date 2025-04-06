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
using SharedKernel;

namespace Application.Features.Students.Commands.EditStudent
{
    public class EditStudentCommandHandler : ICommandHandler<EditStudentCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        public EditStudentCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Result> Handle(EditStudentCommand request, CancellationToken cancellationToken)
        {
            var studentId = request.dto.studentId;
            var student = await _unitOfWork.Students.GetStudentByStudentId(studentId);
            if(student == null) return Result.Failure<Student>(StudentError.StudentNotFound);

            var edit = student.Edit(
                request.dto.name,
                request.dto.dateObBirth,
                request.dto.gender,
                request.dto.address,
                request.dto.email,
                request.dto.phoneNumber
            );
            if(edit.IsFailure) return Result.Failure<Student>(edit.Error);

            var facultyId = request.dto.facultyId;
            if(facultyId != Guid.Empty)
            {
                var faculty = await _unitOfWork.Faculties.GetById((Guid)facultyId!);
                if(faculty == null) return Result.Failure(FacultyError.FacultyNotFound);
                student.ChangeFaculty(faculty);
            }

            var programId = request.dto.programId;
            if(programId != Guid.Empty)
            {
                var program = await _unitOfWork.LearningPrograms.GetById((Guid)programId!);
                if(program == null) return Result.Failure(LearningProgramError.LearningProgramNotFound);
                student.ChangeLearningProgram(program);
            }

            var statusId = request.dto.statusId;
            if(statusId != Guid.Empty)
            {
                var status = await _unitOfWork.Statuses.GetById((Guid)statusId!);
                if(status == null) return Result.Failure(StatusError.StatusNotFound);
                student.ChangeStatus(status);
            }
            
            await _unitOfWork.SaveChangesAsync();

            return edit;
        }
    }
}