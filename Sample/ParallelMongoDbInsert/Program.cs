using System;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;

namespace ParallelMongoDbInsert
{
    class Program
    {
        static void Main(string[] args)
        {
            Parallel.For(0, 100_000, new ParallelOptions
            {
                MaxDegreeOfParallelism = 20
            }, (i) =>
            {
                var someObject = new A
                {
                    Field01 = i,
                    Field02 = i + 0.111,
                    Field03 = "value#" + i,
                    Field04 = DateTime.Now,
                    Field05 = new B
                    {
                        Field01 = i,
                        Field02 = i + 0.111,
                        Field04 = DateTime.Now
                    }
                };

                var saver = new Saver();
                var handler = new SomeAHandler(someObject, a => { saver.Save(a); });

                handler.Dispose();
            });
        }
    }

    internal class SomeAHandler : IDisposable
    {
        private readonly Action<A> _aHandler;

        public SomeAHandler(A a, Action<A> aHandler)
        {
            A = a;
            _aHandler = aHandler;
        }

        private A A { get; set; }

        public void Dispose()
        {
            _aHandler(A);
        }
    }

    internal class Saver
    {
        public void Save(A a)
        {
            var bsonDocument = a.ToBsonDocument();
            var mongoClient = new MongoClient("mongodb://127.0.0.1:27017");
            var mongoDatabase = mongoClient.GetDatabase("mongodbtest");
            var mongoCollection = mongoDatabase.GetCollection<BsonDocument>("samplelogs");
            mongoCollection.InsertOne(bsonDocument);
        }
    }


    class A
    {
        public int Field01 { get; set; }

        public double Field02 { get; set; }

        public string Field03 { get; set; }

        public DateTime Field04 { get; set; }

        public B Field05 { get; set; }
    }

    public class B
    {
        public int Field01 { get; set; }

        public double Field02 { get; set; }

        public DateTime Field04 { get; set; }
    }
}