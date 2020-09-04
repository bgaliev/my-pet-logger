using System.Collections.Generic;

namespace XLog.Core.Models
{
    public class HttpRequestLogData<TAdditionalData>
    {
        public TAdditionalData AdditionalData { get; set; }

        public string Path { get; set; }

        public string Query { get; set; }

        public string IpAddress { get; set; }

        public int Port { get; set; }

        public Dictionary<string, string> RequestCookies { get; set; }

        public Dictionary<string, string> RequestHeaders { get; set; }

        public string RequestBody { get; set; }

        public Dictionary<string, string> ResponseCookies { get; set; }

        public Dictionary<string, string> ResponseHeaders { get; set; }

        public string ResponseBody { get; set; }

        public int ResponseStatusCode { get; set; }
    }
}