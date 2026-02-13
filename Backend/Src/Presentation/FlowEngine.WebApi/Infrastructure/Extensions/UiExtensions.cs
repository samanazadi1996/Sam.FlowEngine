using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace FlowEngine.WebApi.Infrastructure.Extensions;

public static class UiExtensions
{
    public static IApplicationBuilder UseStaticUi(this WebApplication app)
    {
        var angularRoot = Path.Combine(app.Environment.WebRootPath);

        app.UseStaticFiles();

        app.MapFallback(async context =>
        {
            context.Response.ContentType = "text/html";
            await context.Response.SendFileAsync(Path.Combine(angularRoot, "index.html"));
        });

        return app;
    }
}