using System;
using System.Threading.Tasks;
using XLog.Core.Models;

namespace XLog.Core.Services
{
    public class MockLogRepository : ILogRepository
    {
        public void Persist<TLogData>(Log<TLogData> log)
        {
            throw new NotImplementedException();
        }

        public Task PersistAsync<TLogData>(Log<TLogData> log)
        {
            throw new NotImplementedException();
        }
    }
}