using System;
using System.Text.Json;
using System.Threading.Tasks;
using XLog.Core.Models;

namespace XLog.Core.Services
{
    public class MockLogRepository : ILogRepository
    {
        public Task PersistAsync<TLogData>(Log<TLogData> log)
        {
            var serializerOptions = new JsonSerializerOptions
            {
                WriteIndented = true,
                IgnoreNullValues = true,
            };
            var serialize = JsonSerializer.Serialize(log, typeof(Log<TLogData>), serializerOptions);
            Console.WriteLine(serialize);
            return Task.CompletedTask;
        }

        public void Dispose()
        {
        }
    }
}