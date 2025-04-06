using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities.LearningPrograms;
using Domain.Repositories;

namespace Infrastructure.Persistence.Mongodb
{
    public class MongodbLearningProgramRepository : MongoGenericRepository<LearningProgram>, ILearningProgramRepository
    {
        public MongodbLearningProgramRepository(MongoDbContext context) : base(context)
        {
        }
    }
}