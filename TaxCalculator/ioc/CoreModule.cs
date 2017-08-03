using Autofac;
using TaxCalculator.core;

namespace TaxCalculator.ioc
{
    class CoreModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(ThisAssembly)
                .Except<FileManipulator>()
                .AsImplementedInterfaces();
            
            builder.RegisterType<FileManipulator>().AsSelf();
            //builder.RegisterType<NormalEmployee>().AsSelf();

        }

    }
}
