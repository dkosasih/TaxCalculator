using Autofac;
using TaxCalculator.Core;

namespace TaxCalculator.Ioc
{
    public class CoreModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(ThisAssembly)
                .Except<FileManipulator>()
                .Except<PayCalculator>()
                .AsImplementedInterfaces();

            builder.RegisterType<FileManipulator>().AsSelf();
            builder.RegisterType<Runner>().AsSelf();

            builder.RegisterType<MonthlyPayCalculator>().As<PayCalculator>().Keyed<PayCalculator>("Monthly");
            builder.RegisterType<BiWeeklyPayCalculator>().As<PayCalculator>().Keyed<PayCalculator>("BiWeekly");
        }
    }
}
