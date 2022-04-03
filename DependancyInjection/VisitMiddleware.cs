using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace DependancyInjection
{
    public class VisitMiddleware
    {
        private readonly RequestDelegate _next;
        private int _counter=0;

        public VisitMiddleware (RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext context,IVisit visit)
        {
            if (context.Request.Path.Value != null&&!context.Request.Path.Value.StartsWith("/favicon.ico"))
            {
                visit.Visitors++;
                _counter++;
                await context.Response.WriteAsync($"Count: {_counter} Visitors: {visit.Visitors}");
                await _next.Invoke(context);
            }
        }
    }
}
