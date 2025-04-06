using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Repositories;

namespace Infrastructure.Persistence.Mongodb
{
    public class MongoUnitOfWork : IUnitOfWork
    {
        private readonly MongoDbContext _context;
        // Private fields for each repository
        private IFacultyRepository _facultyRepository;
        private IStudentRepository _studentRepository;
        private ILearningProgramRepository _learningProgramRepository;
        private IStatusRepository _statusRepository;

        // Constructor
        public MongoUnitOfWork(MongoDbContext context)
        {
            _context = context;
        }

        public IFacultyRepository Faculties 
            => _facultyRepository ??= new MongodbFacultyRepository(_context);

        public IStudentRepository Students 
            => _studentRepository ??= new MongodbStudentRepository(_context);

        public ILearningProgramRepository LearningPrograms
            => _learningProgramRepository ??= new MongodbLearningProgramRepository(_context);

        public IStatusRepository Statuses 
            => _statusRepository ??= new MongodbStatusRepository(_context);

        public async Task<int> SaveChangesAsync()
        {
            return 0;
            // return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            // _context.Dispose();
        }
    }
}