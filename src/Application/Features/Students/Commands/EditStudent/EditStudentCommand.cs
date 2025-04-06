using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Abstractions.Messaging;
using Domain.Entities.Students.Enums;

namespace Application.Features.Students.Commands.EditStudent;

public record UpdateStudentDto(
    string studentId,
    string name,
    DateTime dateObBirth,
    Gender gender,
    Guid? facultyId,
    string? address,
    string? email,
    string? phoneNumber,
    Guid? programId,
    Guid? statusId 
);

public record EditStudentCommand(UpdateStudentDto dto) : ICommand;