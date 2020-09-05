using System.Threading.Tasks;

namespace XLog.Core
{
    public interface ILogger : IObjectTracker
    {
        void Log<TLogData>(string type, TLogData logData);

        Task LogAsync<TLogData>(string type, TLogData logData);
    }
}