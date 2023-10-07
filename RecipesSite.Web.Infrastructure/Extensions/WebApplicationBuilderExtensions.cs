namespace RecipesSite.Web.Infrastructure.Extensions
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;
    using RecipesSite.Data.Models;
    using System.Reflection;
    using static Common.GeneralApplicationConstants;

    public static class WebApplicationBuilderExtensions
    {
        public static void AddServices(this IServiceCollection services, Type service)
        {
            Assembly? serviceAssembly=Assembly.GetAssembly(service);

            if (serviceAssembly == null)
            {
                throw new InvalidOperationException("This service does not exist!");
            }
            else
            {
                List<Type> servicesImplementations = serviceAssembly.GetTypes().Where(t => t.Name.EndsWith("Service") && !t.IsInterface).ToList();

                foreach(var serviceImplementation in servicesImplementations)
                {
                    string serviceName=serviceImplementation.Name;

                    if (serviceName == null)
                    {
                        throw new InvalidOperationException("This service does not exist!");
                    }
                    else
                    {
                        Type serviceInterface = serviceImplementation.GetInterface($"I{serviceName}")!;

                        services.AddScoped(serviceInterface, serviceImplementation);
                    }
                }

            }
        }

        public static IApplicationBuilder SeedAdministrator(this IApplicationBuilder app, string adminEmail)
        {
            using IServiceScope scopedServices = app.ApplicationServices.CreateScope();

            IServiceProvider serviceProvider = scopedServices.ServiceProvider;

            UserManager<ApplicationUser> userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            RoleManager<IdentityRole<Guid>> roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole<Guid>>>();

            Task.Run(async () =>
            {
                if(await roleManager.RoleExistsAsync(AdminRoleName))
                {
                    return;
                }

                IdentityRole<Guid> role = new IdentityRole<Guid>(AdminRoleName);

                await roleManager.CreateAsync(role);

                ApplicationUser administrator= await userManager.FindByEmailAsync(adminEmail);

                await userManager.AddToRoleAsync(administrator, AdminRoleName);

            }).GetAwaiter().GetResult();

            return app;
        }
    }
}
