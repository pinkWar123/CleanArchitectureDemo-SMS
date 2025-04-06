using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities.Faculties;
using Domain.Repositories;

namespace Infrastructure.Persistence.Mongodb
{
    public class MongodbFacultyRepository : MongoGenericRepository<Faculty>, IFacultyRepository
    {
        public MongodbFacultyRepository(MongoDbContext context) : base(context)
        {
        }
    }
}