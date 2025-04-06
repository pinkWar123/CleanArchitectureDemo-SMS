using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities.Students;
using Domain.Entities.Students.Enums;
using SharedKernel;

namespace Application.Tests.Students
{
    public class StudentBuilder
    {
        // Default values for demonstration.
        private string _studentId = "22127345";
        private string _name = "Default Name";
        private DateTime _dateOfBirth = DateTime.UtcNow.AddYears(-20);
        private Gender _gender = Gender.Male;
        private string? _address = null;
        private string? _email = null;
        private string? _phoneNumber = null;
        private Guid? _facultyId = null;
        private Guid? _programId = null;
        private Guid? _statusId = null;

        public StudentBuilder WithStudentId(string studentId)
        {
            _studentId = studentId;
            return this;
        }

        public StudentBuilder WithName(string name)
        {
            _name = name;
            return this;
        }

        public StudentBuilder WithDateOfBirth(DateTime dob)
        {
            _dateOfBirth = dob;
            return this;
        }

        public StudentBuilder WithGender(Gender gender)
        {
            _gender = gender;
            return this;
        }

        public StudentBuilder WithAddress(string address)
        {
            _address = address;
            return this;
        }

        public StudentBuilder WithEmail(string email)
        {
            _email = email;
            return this;
        }

        public StudentBuilder WithPhoneNumber(string phoneNumber)
        {
            _phoneNumber = phoneNumber;
            return this;
        }

        public StudentBuilder WithFacultyId(Guid facultyId)
        {
            _facultyId = facultyId;
            return this;
        }

        public StudentBuilder WithProgramId(Guid programId)
        {
            _programId = programId;
            return this;
        }

        public StudentBuilder WithStatusId(Guid statusId)
        {
            _statusId = statusId;
            return this;
        }

        /// <summary>
        /// Builds the Student object using the domain's factory method.
        /// Returns a Result<Student> so that caller can decide how to handle failures.
        /// </summary>
        public Result<Student> Build()
        {
            // Call the domain factory method. Adjust the order and parameters as needed.
            var studentResult = Student.Create(
                _studentId,
                _name,
                _dateOfBirth,
                _gender,
                _address,
                _email,
                _phoneNumber,
                null,
                null,
                null
            );
            return studentResult;
        }
    }
}