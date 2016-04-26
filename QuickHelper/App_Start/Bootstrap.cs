using Autofac;
using QuickHelper.Card;
using QuickHelper.Common;
using QuickHelper.Configuration;
using QuickHelper.Files;
using QuickHelper.Repository;
using QuickHelper.ViewModels;

namespace QuickHelper.App_Start
{
    static internal class Bootstrap
    {
        public static void Start(ContainerBuilder builder)
        {
            builder.RegisterType<JsonAppConfigProvider>()
                .As<IAppConfigProvider>()
                .SingleInstance();

            builder.Register(c => c.Resolve<IAppConfigProvider>().GetConfig())
                .As<IAppConfig>();

            builder.RegisterType<FileWatcher>()
                .As<IFileWatcher>()
                .SingleInstance();

            builder.RegisterType<MainViewModel>();

            builder.RegisterType<CardSetRepository>()
                .As<ICardSetRepository>()
                .SingleInstance();

            builder.RegisterType<CardReader>()
                .SingleInstance();

            builder.RegisterType<Logger>()
                .As<ILogger>()
                .SingleInstance();
        }
    }
}
