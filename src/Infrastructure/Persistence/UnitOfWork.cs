using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Interface;
using Domain.Repositories;
using Infrastructure.Data;
using Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private readonly IDbContextFactory<ApplicationDbContext> _factory;
        // Private fields for each repository
        private IFacultyRepository _facultyRepository;
        private IStudentRepository _studentRepository;
        private ILearningProgramRepository _learningProgramRepository;
        private IStatusRepository _statusRepository;

        // Constructor
        public UnitOfWork(IDbContextFactory<ApplicationDbContext> factory)
        {
            _factory = factory;
            _context = _factory.CreateDbContext();
        }

        public IFacultyRepository Faculties 
            => _facultyRepository ??= new FacultyRepository(_context);

        public IStudentRepository Students 
            => _studentRepository ??= new StudentRepository(_context);

        public ILearningProgramRepository LearningPrograms
            => _learningProgramRepository ??= new LearningProgramRepository(_context);

        public IStatusRepository Statuses 
            => _statusRepository ??= new StatusRepository(_context);

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}