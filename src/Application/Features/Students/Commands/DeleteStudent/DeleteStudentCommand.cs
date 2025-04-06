
using Application.Abstractions.Messaging;

namespace Application.Features.Students.Commands.DeleteStudent;

public record DeleteStudentCommand(string studentId) : ICommand;