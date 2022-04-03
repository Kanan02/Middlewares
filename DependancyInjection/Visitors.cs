using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;

namespace DependancyInjection
{
    public static class Visitors
    {
        public static void AddVisitorService(this IServiceCollection services)
        {
            services.AddTransient<IVisit, Visit>();

        }
        public static void UseVisitors(this IApplicationBuilder app)
        {
            app.UseMiddleware<VisitMiddleware>();
            
        }
    }
}
