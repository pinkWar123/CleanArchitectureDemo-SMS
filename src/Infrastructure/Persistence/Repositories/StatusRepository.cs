using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Domain.Entities.Statuses;
using Domain.Repositories;
using Infrastructure.Persistence;
using Infrastructure.Data;

namespace Infrastructure.Persistence.Repositories
{
    public class StatusRepository : SqlGenericRepository<Status>, IStatusRepository
    {
        public StatusRepository(ApplicationDbContext contextFactory) : base(contextFactory)
        {
        }
    }
}
