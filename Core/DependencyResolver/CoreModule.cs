using System;
using Core.Utilities.IoC;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Core.DependencyResolver;

public class CoreModule : ICoreModule
{
    public void Load(IServiceCollection seviceCollection)
    {
        seviceCollection.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
    }
}
