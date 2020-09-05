using System.Threading.Tasks;
using XLog.Core.Models;

namespace XLog.Core
{
    public interface ILogRepository
    {
        void Persist<TLogData>(Log<TLogData> log);

        Task PersistAsync<TLogData>(Log<TLogData> log);
    }
}