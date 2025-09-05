This repo demonstrates an issue in .NET 10.0.100-preview.7.25380.108 where the ManifestEmbeddedFileProvider will fail to load a manifest.

This repo consists of a minimal class library that embeds a css file, and a web application that attempts to use the embedded resource.

To reproduce, run the project with `dotnet run` and observe that the manifest can't be loaded:
```
Unhandled exception. System.InvalidOperationException: Could not load the embedded file manifest 'Microsoft.Extensions.FileProviders.Embedded.Manifest.xml' for assembly 'Embedded'.
   at Microsoft.Extensions.FileProviders.Embedded.Manifest.ManifestParser.Parse(Assembly assembly, String name)
   at Microsoft.Extensions.FileProviders.ManifestEmbeddedFileProvider..ctor(Assembly assembly)
   at StaticAssetsConfigureOptions.PostConfigure(String name, StaticFileOptions options) in /Users/josephdecock/src/josephdecock/ManifestEmbeddedFileProvider/Embedded/StaticAssetsConfigureOptions.cs:line 20
   at Microsoft.Extensions.Options.OptionsFactory`1.Create(String name)
   at Microsoft.Extensions.Options.UnnamedOptionsManager`1.get_Value()
   at Microsoft.AspNetCore.StaticFiles.StaticFileMiddleware..ctor(RequestDelegate next, IWebHostEnvironment hostingEnv, IOptions`1 options, ILoggerFactory loggerFactory)
   at System.Reflection.ConstructorInvoker.InterpretedInvoke(Object obj, IntPtr* args)
   at System.Reflection.ConstructorInvoker.InvokeDirectByRefWithFewArgs(Span`1 copyOfArgs)
   at System.Reflection.ConstructorInvoker.InvokeDirectByRef(Object arg1, Object arg2, Object arg3, Object arg4)
   at System.Reflection.ConstructorInvoker.InvokeImpl(Object arg1, Object arg2, Object arg3, Object arg4)
   at Microsoft.Extensions.DependencyInjection.ActivatorUtilities.ConstructorMatcher.CreateInstance(IServiceProvider provider)
   at Microsoft.Extensions.DependencyInjection.ActivatorUtilities.CreateInstance(IServiceProvider provider, Type instanceType, Object[] parameters)
   at Microsoft.AspNetCore.Builder.UseMiddlewareExtensions.ReflectionMiddlewareBinder.CreateMiddleware(RequestDelegate next)
   at Microsoft.AspNetCore.Builder.ApplicationBuilder.Build()
   at Microsoft.AspNetCore.Builder.StaticAssetDevelopmentRuntimeHandler.EnableSupport(IEndpointRouteBuilder endpoints, StaticAssetsEndpointConventionBuilder builder, IWebHostEnvironment environment, List`1 descriptors)
   at Microsoft.AspNetCore.Builder.StaticAssetsEndpointRouteBuilderExtensions.MapStaticAssets(IEndpointRouteBuilder endpoints, String staticAssetsManifestPath)
   at Program.<Main>$(String[] args) in /Users/josephdecock/src/josephdecock/ManifestEmbeddedFileProvider/Web/Program.cs:line 26
```

Also note that if you change the .NET version in global.json to 10.0.100-preview.6.25358.103, the issue no longer occurs.
