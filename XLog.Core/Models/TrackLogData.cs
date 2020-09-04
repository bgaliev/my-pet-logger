using System;
using XLog.Core.Helpers;

namespace XLog.Core.Models
{
    public class TrackLogData<TTrackedObject, TAdditionalData>
    {
        private TTrackedObject _oldValue;

        public TAdditionalData AdditionalData { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public TTrackedObject Old
        {
            get => _oldValue;
            set => _oldValue = value.Copy();
        }

        public TTrackedObject New { get; set; }
    }
}