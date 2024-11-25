using System;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Crautnot.Installers
{
    public static class InstallerExtension
    {
        public static void InstallServicesInAssembly(
            this IServiceCollection services,
            IConfiguration configuration,
            params Type[] types) {
            var installers = types
                             .Select(t => t.GetTypeInfo().Assembly)
                             .SelectMany(assembly => assembly.GetExportedTypes())
                             .Where(x => typeof(IInstaller).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract)
                             .Select(Activator.CreateInstance)
                             .Cast<IInstaller>()
                             .ToList();

            installers.ForEach(installer => installer.InstallServices(services, configuration));
        }
    }
}
