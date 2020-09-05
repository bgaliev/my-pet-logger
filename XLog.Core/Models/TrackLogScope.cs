using System;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Microsoft.Win32.SafeHandles;

namespace XLog.Core.Models
{
    public class TrackLogScope<TTrackedObject, TAdditionalData> : ITrackable
    {
        private bool _disposed;

        private readonly SafeHandle _safeHandle = new SafeFileHandle(IntPtr.Zero, true);

        private readonly ILogRepository _logRepository;

        private readonly Log<TrackLogData<TTrackedObject, TAdditionalData>> _log;


        public TrackLogScope(ILogRepository logRepository, Log<TrackLogData<TTrackedObject, TAdditionalData>> log)
        {
            _logRepository = logRepository;
            _log = log;
        }

        public void Dispose() => Dispose(true);

        // Protected implementation of Dispose pattern.
        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            if (disposing)
            {
                SaveChanges();
                _safeHandle?.Dispose();
            }

            _disposed = true;
        }

        public void SaveChanges()
        {
            if (_disposed)
            {
                return;
            }

            _log.Data.EndDate = DateTime.UtcNow;
            _logRepository.Persist(_log);
        }

        public Task SaveChangesAsync()
        {
            throw new NotImplementedException();
        }
    }
}