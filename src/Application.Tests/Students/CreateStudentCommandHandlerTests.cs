using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moq;
using Domain.Repositories;
using Application.Features.Students.Commands.CreateStudent;
using Domain.Entities.Students.Enums;
using Domain.Entities.Students;
using Domain.Entities.Students.ValueObjects;
using Domain.Entities.Faculties;
using Domain.Entities.LearningPrograms;
using Domain.Entities.Statuses;

namespace Application.Tests.Students
{
    public class CreateStudentCommandHandlerTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly CreateStudentCommandHandler _handler;
        private readonly StudentBuilder builder = new();
        public CreateStudentCommandHandlerTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();

            // Instantiate the command handler with the mocked unit of work.
            _handler = new CreateStudentCommandHandler(_unitOfWorkMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnFailure_WhenStudentAlreadyExists()
        {
            // Arrange: Create a DTO for a student.
            var studentId = "22127345";
            var dto = new CreateStudentDto(
                studentId,
                "Test Student",
                DateTime.UtcNow.AddYears(-20),
                Gender.Male,
                null,  // No faculty
                null,  // No address
                null,  // No email
                null,  // No phone number
                null,  // No learning program
                null   // No status
            );
            var command = new CreateStudentCommand(dto);

            // Setup the student repository to simulate that a student with the same ID already exists.
            var mockStudent = builder.Build();
            _unitOfWorkMock.Setup(repo => repo.Students.GetStudentByStudentId(studentId))
                .ReturnsAsync(mockStudent.Value); // Simulate existing student.

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert: Expect a failure because the student already exists.
            Assert.True(result.IsFailure);
            Assert.Equal(StudentError.StudentHasExisted, result.Error);
        }

        [Fact]
        public async Task Handle_ShouldReturnSuccess_WhenDataIsValid()
        {
            // Arrange: Create a DTO for a new student.
            var validStudentResult = builder.Build();
            var student = validStudentResult.Value;
            var dto = new CreateStudentDto(
                student.StudentId.Value,
                student.Name.Value,
                student.DateOfBirth,
                student.Gender,
                student?.Faculty?.Id ?? null,  // No faculty
                student?.Address?.ToString() ?? null,
                student?.Email?.Value ?? null,
                student?.PhoneNumber?.Value ?? null,
                student?.LearningProgram?.Id ?? null,  // No learning program
                student?.Status?.Id ?? null   // No status
            );
            var command = new CreateStudentCommand(dto);

            // Setup the repository to simulate that no existing student is found.
            _unitOfWorkMock.Setup(repo => repo.Students.GetStudentByStudentId("S123"))
                .ReturnsAsync((Student)null);

            // Setup the Create method to simply return the student that is provided.
            _unitOfWorkMock.Setup(repo => repo.Students.Create(It.IsAny<Student>()))
                .ReturnsAsync((Student student) => student);

            // Setup SaveChanges to return 1 (simulating one change saved).
            _unitOfWorkMock.Setup(u => u.SaveChangesAsync())
                .ReturnsAsync(1);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert: Expect success and that the created student's studentId is "S123".
            Assert.True(result.IsSuccess);
            Assert.Equal(student.StudentId.Value, result.Value.StudentId.Value);
        }

        [Fact]
        public async Task Handle_ShouldReturnFailure_WhenFacultyNotFound()
        {
            // Arrange: provide a FacultyId, but simulate no faculty found.
            var validStudentResult = builder.Build();
            var student = validStudentResult.Value;
            var dto = new CreateStudentDto(
                student.StudentId.Value,
                student.Name.Value,
                student.DateOfBirth,
                student.Gender,
                Guid.NewGuid(), // non-null FacultyId
                null, null, null, null, null
            );
            var command = new CreateStudentCommand(dto);
            _unitOfWorkMock.Setup(repo => repo.Students.GetStudentByStudentId(student.StudentId.Value))
                .ReturnsAsync((Student)null);
            // Simulate that faculty lookup returns null.
            _unitOfWorkMock.Setup(repo => repo.Faculties.GetById(It.IsAny<Guid>()))
                .ReturnsAsync((Faculty)null);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.IsFailure);
            Assert.Equal(FacultyError.FacultyNotFound, result.Error);
        }

        [Fact]
        public async Task Handle_ShouldReturnFailure_WhenLearningProgramNotFound()
        {
            // Arrange: provide a ProgramId, but simulate no learning program found.
            var validStudentResult = builder.Build();
            var student = validStudentResult.Value;
            var dto = new CreateStudentDto(
                student.StudentId.Value,
                student.Name.Value,
                student.DateOfBirth,
                student.Gender,
                null, 
                null, null, null, Guid.NewGuid(), null
            );
            var command = new CreateStudentCommand(dto);
            _unitOfWorkMock.Setup(repo => repo.Students.GetStudentByStudentId(student.StudentId.Value))
                .ReturnsAsync((Student)null);
            _unitOfWorkMock.Setup(repo => repo.LearningPrograms.GetById(It.IsAny<Guid>()))
                .ReturnsAsync((LearningProgram)null);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.IsFailure);
            Assert.Equal(LearningProgramError.LearningProgramNotFound, result.Error);
        }

        [Fact]
        public async Task Handle_ShouldReturnFailure_WhenStatusNotFound()
        {
            // Arrange: provide a StatusId, but simulate no status found.
            var validStudentResult = builder.Build();
            var student = validStudentResult.Value;
            var dto = new CreateStudentDto(
                student.StudentId.Value,
                student.Name.Value,
                student.DateOfBirth,
                student.Gender,
                null, 
                null, null, null, null, Guid.NewGuid()
            );
            var command = new CreateStudentCommand(dto);
            _unitOfWorkMock.Setup(repo => repo.Students.GetStudentByStudentId(student.StudentId.Value))
                .ReturnsAsync((Student)null);
            _unitOfWorkMock.Setup(repo => repo.Statuses.GetById(It.IsAny<Guid>()))
                .ReturnsAsync((Status)null);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.IsFailure);
            Assert.Equal(StatusError.StatusNotFound, result.Error);
        }

        [Fact]
        public async Task Handle_ShouldReturnFailure_WhenStudentCreationFails()
        {
            // Arrange: simulate failure from the domain's Student.Create method.
            // We'll use a faulty DTO input that causes a failure.
            // For example, an invalid student id format, or an empty name.
            var validStudentResult = builder.Build();
            var student = validStudentResult.Value;
            var invalidStudentId = "invalid";
            var dto = new CreateStudentDto(
                invalidStudentId,
                student.Name.Value,
                student.DateOfBirth,
                student.Gender,
                null, 
                null, null, null, null, null
            );
            var command = new CreateStudentCommand(dto);
            _unitOfWorkMock.Setup(repo => repo.Students.GetStudentByStudentId(invalidStudentId))
                .ReturnsAsync((Student)null);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert: We expect a failure due to invalid student data.
            Assert.True(result.IsFailure);
            // The exact error message depends on your domain validation.
        }
    }
}