using GalaSoft.MvvmLight;

namespace QuickHelper.ViewModels
{
    public class BaseViewModel : ViewModelBase
    {
        private bool _isLoading;

        public virtual bool IsLoading
        {
            get { return _isLoading; }
            set { _isLoading = value; RaisePropertyChanged(); }
        }
    }
}