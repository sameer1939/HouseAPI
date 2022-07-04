using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Extensions
{
    public static class ConfigureExceptionHandler
    {
        public static void ConfigureExceptionHandling(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler(options =>
                {
                    options.Run(
                        async context =>
                        {
                            context.Response.StatusCode = 500;
                            var ex = context.Features.Get<IExceptionHandlerFeature>();
                            if (ex != null)
                                await context.Response.WriteAsync(ex.Error.Message);
                        });
                });
            }
        }
    }
}
