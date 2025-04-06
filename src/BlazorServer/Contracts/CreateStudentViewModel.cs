using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorServer.Contracts
{
    public class CreateStudentViewModel
    {
        
        public string StudentId { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public DateTime DateObBirth { get; set; } = DateTime.UtcNow;
        public Domain.Entities.Students.Enums.Gender Gender { get; set; }
        public Guid? FacultyId { get; set; }
        public string? Address { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public Guid? ProgramId { get; set; }
        public Guid? StatusId { get; set; }
    }

}