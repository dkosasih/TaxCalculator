using Autofac;
using TaxCalculator.Core;

namespace TaxCalculator.Ioc
{
    public class IocConfig
    {
        public static IContainer Container;

        public static void RegisterDependencies(string period)
        {
            var builder = new ContainerBuilder();

            builder.RegisterModule(new CoreModule());

            Container = builder.Build();

        }
    }
}
