using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Common.ValueObjects.Names;
using Domain.Entities.Faculties;
using Domain.Entities.LearningPrograms;
using Domain.Entities.Statuses;
using Domain.Entities.Students.Enums;
using Domain.Entities.Students.ValueObjects;
using Domain.Entities.Students.ValueObjects.Address;
using Domain.Entities.Students.ValueObjects.Email;
using Domain.Entities.Students.ValueObjects.Phones;
using SharedKernel;

namespace Domain.Entities.Students
{
    public class Student : BaseEntity
    {
        public StudentId StudentId { get; private set; }
        public Name Name { get; private set; }
        public DateTime DateOfBirth { get; private set; }
        public Gender Gender { get; private set; }
        public Faculty? Faculty { get; private set; }
        public Address? Address { get; private set; }
        public Email? Email { get; private set; }
        public PhoneNumber? PhoneNumber { get; private set; }
        public LearningProgram? LearningProgram { get; private set; }
        public Status? Status { get; private set; }

        private Student() {}
        private Student(
            StudentId id,
            Name name,
            DateTime dob,
            Gender gender,
            Faculty? faculty,
            Address? address,
            Email? email,
            PhoneNumber? phoneNumber,
            LearningProgram? learningProgram,
            Status? status
        )
        {
            StudentId = id;
            Name = name;
            DateOfBirth = dob;
            Gender = gender;
            Faculty = faculty;
            Address = address;
            Email = email;
            PhoneNumber = phoneNumber;
            LearningProgram =learningProgram;
            Status = status;
        }

        public static Result<Student> Create(
            string studentId,
            string name,
            DateTime dob,
            Gender gender,
            string? address,
            string? email,
            string? phoneNumber,
            Faculty? faculty,
            LearningProgram? program,
            Status? status
        )
        {
            var id = StudentId.Create(studentId);
            if(id.IsFailure) return Result.Failure<Student>(id.Error);

            var studentName = Name.Create(name);
            if(studentName.IsFailure) return Result.Failure<Student>(studentName.Error);

            Address? studentAddress = null;
            if(!string.IsNullOrEmpty(address))
            {
                var createStudentAddress = Address.Create(address);
                if(createStudentAddress.IsFailure) return Result.Failure<Student>(createStudentAddress.Error);
                studentAddress = createStudentAddress.Value;
            }
            
            Email? studentEmail = null;
            if(!string.IsNullOrEmpty(email))
            {
                var createStudentEmail = Email.Create(email);
                if(createStudentEmail.IsFailure) return Result.Failure<Student>(createStudentEmail.Error);
                studentEmail = createStudentEmail.Value;
            }

            PhoneNumber? studentPhoneNumber = null;
            if(!string.IsNullOrEmpty(phoneNumber))
            {
                var createPhoneNumber = PhoneNumber.Create(phoneNumber);
                if(createPhoneNumber.IsFailure) return Result.Failure<Student>(createPhoneNumber.Error);
                studentPhoneNumber = createPhoneNumber.Value;
            }

            return Result.Success(
                new Student(id.Value,
                    studentName.Value,
                    dob,
                    gender,
                    faculty,
                    studentAddress,
                    studentEmail,
                    studentPhoneNumber,
                    program,
                    status                    
                ));
        }

        public Result Edit(
            string name,
            DateTime dob,
            Gender gender,
            string? address,
            string? email,
            string? phoneNumber)
        {
            var studentName = Name.Create(name);
            if(studentName.IsFailure) return Result.Failure<Student>(studentName.Error);

            if(!string.IsNullOrEmpty(address))
            {
                var createStudentAddress = Address.Create(address);
                if(createStudentAddress.IsFailure) return Result.Failure<Student>(createStudentAddress.Error);
                Address = createStudentAddress.Value;
            }
            
            if(!string.IsNullOrEmpty(email))
            {
                var createStudentEmail = Email.Create(email);
                if(createStudentEmail.IsFailure) return Result.Failure<Student>(createStudentEmail.Error);
                Email = createStudentEmail.Value;
            }

            if(!string.IsNullOrEmpty(phoneNumber))
            {
                var createPhoneNumber = PhoneNumber.Create(phoneNumber);
                if(createPhoneNumber.IsFailure) return Result.Failure<Student>(createPhoneNumber.Error);
                PhoneNumber = createPhoneNumber.Value;
            }

            Gender = gender;
            DateOfBirth = dob;

            return Result.Success();
        }

        public Result ChangeFaculty(Faculty faculty)
        {
            Faculty = faculty;
            return Result.Success();
        }

        public Result ChangeLearningProgram(LearningProgram program)
        {
            LearningProgram = program;
            return Result.Success();
        }

        public Result ChangeStatus(Status status)
        {
            Status = status;
            return Result.Success();
        }
    }
}