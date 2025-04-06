using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities.Students;

namespace Domain.Repositories
{
    public interface IStudentRepository : IGenericRepository<Student>
    {
        Task<Student?> GetStudentByStudentId(string id);
        Task CreateMany(IEnumerable<Student> students);
    }
}