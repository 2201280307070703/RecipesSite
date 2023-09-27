namespace RecipesSite.Web.Infrastructure.Extensions
{
    using Microsoft.Extensions.DependencyInjection;
    using System.Reflection;

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
    }
}
