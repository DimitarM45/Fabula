namespace Fabula.Web.Infrastructure.Extensions;

using Ganss.Xss;

using Microsoft.Extensions.DependencyInjection;

using System.Reflection;

/// <summary>
/// Extension methods used in conjunction with the WebApplicationBuilder object in Program.cs
/// </summary>

public static class WebApplicationBuilderExtensions
{
    /// <summary>
    /// Registers all services with their interfaces and implementations from a given assembly.
    /// The assembly is located using the random service type provided.
    /// </summary>
    /// <param name="serviceType">Type of the random service given in order to find the necessary assembly</param>
    /// <exception cref="InvalidOperationException"></exception>

    public static IServiceCollection AddApplicationServices(this IServiceCollection services, Type serviceType)
    {
        Assembly? serviceAssembly = Assembly.GetAssembly(serviceType);

        if (serviceAssembly == null)
            throw new InvalidOperationException("Invalid service type provided!");

        IEnumerable<Type> serviceTypes = serviceAssembly
            .GetTypes()
            .Where(t => t.Name.EndsWith("Service") && !t.IsInterface);

        foreach (Type implementationType in serviceTypes)
        {
            Type? interfaceType = implementationType.GetInterface($"I{implementationType.Name}");

            if (interfaceType == null)
                throw new InvalidOperationException($"No interface exists for {implementationType.Name}!");

            services.AddScoped(interfaceType, implementationType);
        }

        return services;
    }

    /// <summary>
    /// Registers all third party services. 
    /// </summary>

    public static IServiceCollection AddThirdPartyServices(this IServiceCollection services)
    {
        services.AddScoped<IHtmlSanitizer, HtmlSanitizer>();

        return services;
    }
}
