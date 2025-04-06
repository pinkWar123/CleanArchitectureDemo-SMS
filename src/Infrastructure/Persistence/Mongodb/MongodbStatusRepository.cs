using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities.Statuses;
using Domain.Repositories;

namespace Infrastructure.Persistence.Mongodb
{
    public class MongodbStatusRepository : MongoGenericRepository<Status>, IStatusRepository
    {
        public MongodbStatusRepository(MongoDbContext context) : base(context)
        {
        }
    }
}