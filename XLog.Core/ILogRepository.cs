using System.Threading.Tasks;
using XLog.Core.Models;

namespace XLog.Core
{
    public interface ILogRepository
    {
        Task PersistAsync<TLogData>(Log<TLogData> log);
    }
}