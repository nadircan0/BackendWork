using System;
using Core.Utilities.IoC;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDependencyResolvers(this IServiceCollection serviceColelction, ICoreModule[] modules)
    {
        foreach (var module in modules)
        {
            module.Load(serviceColelction);
            
        }

    return ServiceTool.Create(serviceColelction);

    }
}
