namespace Fabula.Web.Infrastructure.Extensions;

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
    /// <param name="serviceType"></param>
    /// <exception cref="InvalidOperationException"></exception>
    /// 
    public static void AddServices(this IServiceCollection services, Type serviceType)
    {
        Assembly? serviceAssembly = Assembly.GetAssembly(serviceType);

        if (serviceAssembly == null)
            throw new InvalidOperationException("Invalid service type provided!");

        IEnumerable<Type> serviceTypes = serviceAssembly
            .GetTypes()
            .Where(t => t.Name.EndsWith("Service") && !t.IsInterface);

        foreach (Type implementationType in serviceTypes)
        {
            Type? interfaceType = serviceType.GetInterface($"I{implementationType.Name}");

            if (interfaceType == null)
                throw new InvalidOperationException($"No interface exists for {implementationType.Name}!");

            if (interfaceType != null)
                services.AddScoped(interfaceType, implementationType);
        }
    }
} 
