using System.Threading.Tasks;

namespace XLog.Core
{
    public interface ILogger : IObjectTracker
    {
        Task LogAsync<TLogData>(string type, TLogData logData);
    }
}