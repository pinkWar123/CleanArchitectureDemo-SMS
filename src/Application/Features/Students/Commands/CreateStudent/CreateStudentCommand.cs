using Application.Abstractions.Messaging;
using Domain.Entities.Students;
using Domain.Entities.Students.Enums;

namespace Application.Features.Students.Commands.CreateStudent;

public record CreateStudentDto(
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

public record CreateStudentCommand(CreateStudentDto dto) : ICommand<Student>;