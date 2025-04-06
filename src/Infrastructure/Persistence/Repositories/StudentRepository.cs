using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities.Students;
using Domain.Entities.Students.ValueObjects;
using Domain.Repositories;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories
{
    public class StudentRepository : SqlGenericRepository<Student>, IStudentRepository
    {
        public StudentRepository(ApplicationDbContext contextFactory) : base(contextFactory)
        {
        }

        public Task CreateMany(IEnumerable<Student> students)
        {
            throw new NotImplementedException();
        }

        public override async Task<IEnumerable<Student>> GetAll()
        {
            return await _context.Students
                .Include(s => s.Faculty)
                .Include(s => s.LearningProgram)
                .Include(s => s.Status)
                .ToListAsync();
        }

        public async Task<Student?> GetStudentByStudentId(string id)
        {
            var studentId = StudentId.Create(id);
            if(studentId.IsFailure) return null;
            return await _context.Students
                .Include(s => s.Faculty)
                .Include(s => s.LearningProgram)
                .Include(s => s.Status)
                .FirstOrDefaultAsync(s => s.StudentId == studentId.Value);
        }
    }
}