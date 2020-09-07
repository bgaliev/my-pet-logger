using System;

namespace XLog.Core.Models
{
    public class TrackLogData<TTrackedObject, TAdditionalData>
    {
        public TAdditionalData AdditionalData { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public TTrackedObject Old { get; set; }

        public TTrackedObject New { get; set; }
    }
}