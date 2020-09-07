using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;

namespace XLog.AspNetCore.Razor
{
    public class XLogPageFilter : IAsyncPageFilter
    {
        public async Task OnPageHandlerExecutionAsync(PageHandlerExecutingContext context,
            PageHandlerExecutionDelegate next)
        {
            var log = new XLogPageData();
            log.ProcessStart = DateTime.UtcNow;
            log.Area = context.ActionDescriptor.AreaName;
            log.IpAddress = context.HttpContext.Connection.RemoteIpAddress.ToString();
            log.Port = context.HttpContext.Connection.RemotePort.ToString();
            log.HttpMethod = context.HttpContext.Request.Method;
            log.Page = context.HandlerInstance.GetType().Name;
            log.HandlerMethod = context.HandlerMethod.Name;

            await next();
        }

        public Task OnPageHandlerSelectionAsync(PageHandlerSelectedContext context)
        {
            return Task.CompletedTask;
        }
    }
}