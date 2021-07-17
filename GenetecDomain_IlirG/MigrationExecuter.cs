using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GenetecDomain_IlirG
{
    public static class MigrationExecuter
    {
        public static async Task Run(IServiceProvider provider)
        {
            var assembly = typeof(Genetec_IlirGContext).Assembly;
            var dbContextType = typeof(DbContext);
            var contextTypes = assembly.GetExportedTypes()
                .Where(x => dbContextType.IsAssignableFrom(x))
                .Where(x => x.GetCustomAttribute<DisableMigrationsAttribute>() == null)
                .ToArray();

            using var scope = provider.GetRequiredService<IServiceScopeFactory>().CreateScope();
            foreach (var contextType in contextTypes)
            {
                await using var context = scope.ServiceProvider.GetRequiredService(contextType) as DbContext;

                if (context != null)
                    await context.Database.MigrateAsync();
            }
        }
    }
}
