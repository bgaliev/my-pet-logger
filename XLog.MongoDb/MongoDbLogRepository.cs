using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using XLog.Core;
using XLog.Core.Models;

namespace XLog.MongoDb
{
    public class MongoDbLogRepository : ILogRepository
    {
        private readonly MongoDbOptions _options;

        public MongoDbLogRepository(IOptions<MongoDbOptions> options)
        {
            _options = options.Value;
        }

        public async Task PersistAsync<TLogData>(Log<TLogData> log)
        {
            var bsonDocument = log.ToBsonDocument();
            var mongoClient = new MongoClient(_options.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(_options.Database);
            var mongoCollection = mongoDatabase.GetCollection<BsonDocument>(_options.CollectionName);
            await mongoCollection.InsertOneAsync(bsonDocument);
        }

        public void Dispose()
        {
            
        }
    }
}