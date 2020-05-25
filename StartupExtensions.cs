using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleMultiPageApp
{
    public static class StartupExtensions
    {
        /// <summary>
        /// This is to force ApiControllers to return a 401 when access not authorised rather than redirect to the login page.
        /// </summary>
        /// <param name="services"></param>
        public static void ConfigureApplicationCookieForApiControllerAuthorization(this IServiceCollection services)
        {
            // https://github.com/dotnet/aspnetcore/issues/9039#issuecomment-479667297
            services.ConfigureApplicationCookie(o =>
            {
                o.Events = new CookieAuthenticationEvents()
                {
                    OnRedirectToLogin = (ctx) =>
                    {
                        if (ctx.Request.Path.StartsWithSegments("/api") && ctx.Response.StatusCode == 200)
                        {
                            ctx.Response.StatusCode = 401;
                        }

                        return Task.CompletedTask;
                    },
                    OnRedirectToAccessDenied = (ctx) =>
                    {
                        if (ctx.Request.Path.StartsWithSegments("/api") && ctx.Response.StatusCode == 200)
                        {
                            ctx.Response.StatusCode = 403;
                        }

                        return Task.CompletedTask;
                    }
                };
            });
        }

        /// <summary>
        /// Set WebRootFileProvider to include "ClientApp/dist" for Webpack outputs.
        /// This also enables us to use asp-append-version on script tags.
        /// </summary>
        /// <param name="env"></param>
        public static void ConfigureClientApp(this IWebHostEnvironment env, string root)
        {
            // https://github.com/aspnet/Mvc/issues/7459#issuecomment-371969518
            var fileProviders = (env.WebRootFileProvider as CompositeFileProvider).FileProviders.ToList();
            fileProviders.Add(new PhysicalFileProvider(Path.Combine(env.ContentRootPath, root)));
            env.WebRootFileProvider = new CompositeFileProvider(fileProviders);
        }
    }
}