using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Domain.Entities.Faculties;
using Domain.Repositories;
using Infrastructure.Persistence;
using Infrastructure.Data;

namespace Infrastructure.Persistence.Repositories
{
    public class FacultyRepository : SqlGenericRepository<Faculty>, IFacultyRepository
    {
        public FacultyRepository(ApplicationDbContext contextFactory) : base(contextFactory)
        {
        }
    }
}
