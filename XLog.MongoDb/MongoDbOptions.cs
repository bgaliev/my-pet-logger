namespace XLog.MongoDb
{
    public class MongoDbOptions
    {
        public string ConnectionString { get; set; }

        public string Database { get; set; }

        public string CollectionName { get; set; } = "logs";
    }
}