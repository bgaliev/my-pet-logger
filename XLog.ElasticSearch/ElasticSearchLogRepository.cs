using System;
using System.Threading.Tasks;
using Elasticsearch.Net;
using XLog.Core;
using XLog.Core.Models;

namespace XLog.ElasticSearch
{
    public class ElasticSearchLogRepository : ILogRepository
    {
        public async Task PersistAsync<TLogData>(Log<TLogData> log)
        {
            var settings = new ConnectionConfiguration(new Uri("http://localhost:9200"))
                .RequestTimeout(TimeSpan.FromMinutes(2));
            
            var lowlevelClient = new ElasticLowLevelClient(settings);

            var stringResponse = await lowlevelClient.IndexAsync<StringResponse>(log.Type, log.Id, PostData.Serializable(log));

            if (!stringResponse.Success)
            {
                Console.WriteLine(stringResponse.DebugInformation);                
            }
        }
    }
}