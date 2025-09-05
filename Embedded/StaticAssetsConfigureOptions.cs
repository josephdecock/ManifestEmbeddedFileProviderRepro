using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Options;

internal class StaticAssetsConfigureOptions(IWebHostEnvironment environment) : IPostConfigureOptions<StaticFileOptions>
{
    public void PostConfigure(string? name, StaticFileOptions options)
    {
        options.ContentTypeProvider = options.ContentTypeProvider ?? new FileExtensionContentTypeProvider();
        if (options.FileProvider == null && environment.WebRootFileProvider == null)
        {
            throw new InvalidOperationException("Missing FileProvider.");
        }
        options.FileProvider = options.FileProvider ?? environment.WebRootFileProvider;

        var assembly = GetType().Assembly;
        var filesProvider = new ManifestEmbeddedFileProvider(assembly);
        options.FileProvider = new CompositeFileProvider(options.FileProvider, filesProvider);
    }
}

public static class EmbeddedExtensions
{
    public static void AddEmbeddedFiles(this WebApplicationBuilder builder) => builder.Services.ConfigureOptions<StaticAssetsConfigureOptions>();
}
