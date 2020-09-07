using System;

namespace XLog.Core.Models
{
    public class TrackLogScope<TTrackedObject, TAdditionalData> : ITrackable
    {
        private readonly Action<TrackLogData<TTrackedObject, TAdditionalData>> _onDispose;

        private readonly TrackLogData<TTrackedObject, TAdditionalData> _trackLogData;
        private bool _disposed;

        public TrackLogScope(TrackLogData<TTrackedObject, TAdditionalData> trackLogData,
            Action<TrackLogData<TTrackedObject, TAdditionalData>> onDispose)
        {
            _trackLogData = trackLogData;
            _onDispose = onDispose;
        }

        public void Dispose()
        {
            Dispose(true);
        }

        private void SaveChanges()
        {
            _trackLogData.EndDate = DateTime.UtcNow;
            _onDispose(_trackLogData);
        }

        // Protected implementation of Dispose pattern.
        protected virtual void Dispose(bool disposing)
        {
            if (_disposed) return;

            if (disposing) SaveChanges();

            _disposed = true;
        }
    }
}