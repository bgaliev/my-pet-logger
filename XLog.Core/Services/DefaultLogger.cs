using System;
using System.Threading.Tasks;
using XLog.Core.Helpers;
using XLog.Core.Models;

namespace XLog.Core.Services
{
    public class DefaultLogger : ILogger
    {
        private readonly ILogRepository _repository;

        public DefaultLogger(ILogRepository repository)
        {
            _repository = repository;
        }

        public ITrackable Track<TTrackedObject, TAdditionalData>(string type, TTrackedObject trackedObject,
            TAdditionalData additionalData)
        {
            var logData = new TrackLogData<TTrackedObject, TAdditionalData>
            {
                Old = trackedObject.Copy(),
                New = trackedObject,
                AdditionalData = additionalData,
                StartDate = DateTime.UtcNow,
                EndDate = default
            };

            var logScope = new TrackLogScope<TTrackedObject, TAdditionalData>(_repository, Create(type, logData));
            return logScope;
        }

        public Task<ITrackable> TrackAsync<TTrackedObject, TAdditionalData>(string type, TTrackedObject trackedObject,
            TAdditionalData additionalData)
        {
            throw new NotImplementedException();
        }

        public void Log<TLogData>(string type, TLogData logData)
        {
            _repository.Persist(Create(type, logData));
        }

        public Task LogAsync<TLogData>(string type, TLogData logData)
        {
            throw new NotImplementedException();
        }

        // ReSharper disable once MemberCanBeMadeStatic.Local
        private Log<TLogData> Create<TLogData>(string type, TLogData logData)
        {
            return new Log<TLogData>
            {
                Id = Guid.NewGuid().ToString(),
                CreatedAt = DateTime.UtcNow,
                Data = logData,
                Type = type,
                UserId = "sample-user-id"
            };
        }
    }
}