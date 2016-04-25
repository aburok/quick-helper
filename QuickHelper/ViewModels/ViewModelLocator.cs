using Autofac;

namespace QuickHelper.ViewModels
{
    public class ViewModelLocator
    {
        public MainViewModel Main => App.ServiceContainer.Resolve<MainViewModel>();
    }
}