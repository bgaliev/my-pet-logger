using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Nito.AsyncEx.Synchronous;
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

            var logScope = new TrackLogScope<TTrackedObject, TAdditionalData>(logData, data => { Log(type, logData); });
            return logScope;
        }

        public Task<ITrackable> TrackAsync<TTrackedObject, TAdditionalData>(string type, TTrackedObject trackedObject,
            TAdditionalData additionalData)
        {
            throw new NotImplementedException();
        }

        public void Log<TLogData>(string type, TLogData logData)
        {
            LogAsync(type, logData).WaitAndUnwrapException();
        }

        public async Task LogAsync<TLogData>(string type, TLogData logData)
        {
            var userIdentifier = _httpContextAccessor?.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var log = new Log<TLogData>
            {
                CreatedAt = DateTime.UtcNow,
                Data = logData,
                Id = Guid.NewGuid().ToString(), //
                Type = type,
                UserId = userIdentifier
            };

            await _repository.PersistAsync(log);
        }
    }
}