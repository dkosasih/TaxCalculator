using Autofac;

namespace TaxCalculator.Ioc
{
    public class IocConfig
    {
        public static IContainer Container;

        public static void RegisterDependencies()
        {
            var builder = new ContainerBuilder();

            builder.RegisterModule(new CoreModule());

            Container = builder.Build();
        }
    }
}
