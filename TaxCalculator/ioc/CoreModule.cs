using Autofac;
using TaxCalculator.Core;

namespace TaxCalculator.Ioc
{
    class CoreModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(ThisAssembly)
                .Except<FileManipulator>()
                .AsImplementedInterfaces();
            
            builder.RegisterType<FileManipulator>().AsSelf();

        }

    }
}
