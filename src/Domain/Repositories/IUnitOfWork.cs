using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IFacultyRepository Faculties { get; }
        IStudentRepository Students { get; }
        ILearningProgramRepository LearningPrograms { get; }
        IStatusRepository Statuses { get; }
        Task<int> SaveChangesAsync();
    }
}