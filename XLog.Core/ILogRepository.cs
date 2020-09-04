using System;
using System.Threading.Tasks;
using XLog.Core.Models;

namespace XLog.Core
{
    public interface ILogRepository : IDisposable
    {
        Task PersistAsync<TLogData>(Log<TLogData> log);
    }
}