using System;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Utilities.IoC;

public interface ICoreModule
{

    void Load(IServiceCollection seviceCollection);

}
