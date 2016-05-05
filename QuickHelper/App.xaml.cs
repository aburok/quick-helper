using System.Windows.Forms;
using Autofac;
using NHotkey.Wpf;
using QuickHelper.App_Start;
using QuickHelper.Card;
using Application = System.Windows.Application;

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
