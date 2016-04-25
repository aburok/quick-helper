using System.Windows;
using Autofac;
using QuickHelper.App_Start;
using QuickHelper.Card;

namespace QuickHelper
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static IContainer ServiceContainer { get; private set; }

        public App()
        {
            var builder = new ContainerBuilder();

            Bootstrap.Start(builder);

            ServiceContainer = builder.Build();

            this.Activated += App_Activated;
        }

        private void App_Activated(object sender, System.EventArgs e)
        {
            var cardReader = ServiceContainer.Resolve<CardReader>();
            cardReader.ReadAllFiles();
        }
    }
}
