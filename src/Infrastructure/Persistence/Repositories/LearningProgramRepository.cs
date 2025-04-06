using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Domain.Entities.LearningPrograms;
using Domain.Repositories;
using Infrastructure.Persistence;
using Infrastructure.Data;

namespace Infrastructure.Persistence.Repositories
{
    public class LearningProgramRepository : SqlGenericRepository<LearningProgram>, ILearningProgramRepository
    {
        public LearningProgramRepository(ApplicationDbContext contextFactory) : base(contextFactory)
        {
        }
    }
}
