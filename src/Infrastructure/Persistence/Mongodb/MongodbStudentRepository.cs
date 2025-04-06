using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities.Students;
using Domain.Repositories;
using MongoDB.Driver;

namespace Infrastructure.Persistence.Mongodb
{
    public class MongodbStudentRepository : MongoGenericRepository<Student>, IStudentRepository
    {
        public MongodbStudentRepository(MongoDbContext context) : base(context)
        {
            
        }

        public async Task CreateMany(IEnumerable<Student> students)
        {
            await _collection.InsertManyAsync(students);
        }

        public async Task<Student?> GetStudentByStudentId(string id)
        {
            var filter = Builders<Student>.Filter.Eq(s => s.StudentId.Value, id);
            return await _collection.Find(filter).FirstOrDefaultAsync();
        }
    }
}