using System;
using System.Threading.Tasks;

namespace XLog.Core
{
    public interface IObjectTracker
    {
        ITrackable Track<TTrackedObject, TAdditionalData>(string type, TTrackedObject trackedObject,
            TAdditionalData additionalData);

        Task<ITrackable> TrackAsync<TTrackedObject, TAdditionalData>(string type, TTrackedObject trackedObject,
            TAdditionalData additionalData);
    }

    public interface ITrackable : IDisposable
    {
        void SaveChanges();
        
        Task SaveChangesAsync();
    }
}