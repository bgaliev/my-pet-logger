using System;

namespace XLog.Core.Models
{
    public sealed class Log<TLogData>
    {
        public string Type { get; set; }

        public string Id { get; set; }

        public string UserId { get; set; }

        public DateTime CreatedAt { get; set; }

        public TLogData Data { get; set; }
    }
}