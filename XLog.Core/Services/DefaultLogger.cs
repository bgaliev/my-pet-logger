using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using XLog.Core.Helpers;
using XLog.Core.Models;

namespace XLog.Core.Services
{
    public class DefaultLogger : ILogger
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        private readonly ILogRepository _repository;

        public DefaultLogger(IHttpContextAccessor httpContextAccessor, ILogRepository repository)
        {
            _httpContextAccessor = httpContextAccessor;
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

            var logScope = new TrackLogScope<TTrackedObject, TAdditionalData>(logData, data => { Log(type, data); });
            return logScope;
        }

        public Task<ITrackable> TrackAsync<TTrackedObject, TAdditionalData>(string type, TTrackedObject trackedObject,
            TAdditionalData additionalData)
        {
            return Task.FromResult(Track(type, trackedObject, additionalData));
        }

        public void Log<TLogData>(string type, TLogData logData)
        {
            _repository.Persist(BuildLog(type, logData));
        }

        public async Task LogAsync<TLogData>(string type, TLogData logData)
        {
            await _repository.PersistAsync(BuildLog(type, logData));
        }

        private Log<TLogData> BuildLog<TLogData>(string type, TLogData logData)
        {
            var userIdentifier = _httpContextAccessor?.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var logId = _httpContextAccessor?.HttpContext?.TraceIdentifier ?? Guid.NewGuid().ToString();

            return new Log<TLogData>
            {
                CreatedAt = DateTime.UtcNow,
                Data = logData,
                Id = logId,
                Type = type,
                UserId = userIdentifier
            };
        }
    }
}