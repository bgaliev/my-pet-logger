using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace XLog.AspNetCore.Razor
{
    public class XLogPageData
    {
        public string IpAddress { get; set; }

        public string Port { get; set; }

        public string HttpMethod { get; set; }

        public DateTime ProcessStart { get; set; }

        public DateTime ProcessEnd { get; set; }

        public string Area { get; set; }

        public string Page { get; set; }

        public string User { get; set; }

        public string HandlerMethod { get; set; }

        public string HandlerInstance { get; set; }

        public IDictionary<string, object> HandlerArguments { get; set; }

        public IDictionary<string, object> BindProperties { get; set; }

        public ModelValidationState ModelValidationState { get; set; }

        public IReadOnlyDictionary<string, ModelStateEntry> ModelStateDictionary { get; set; }
    }
}