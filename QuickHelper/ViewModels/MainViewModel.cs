using System.Collections.ObjectModel;
using System.Linq;
using QuickHelper.Card;
using QuickHelper.Repository;

namespace QuickHelper.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private readonly ICardSetRepository _cardSetRepository;

        public MainViewModel(ICardSetRepository cardSetRepository)
        {
            _cardSetRepository = cardSetRepository;
            _cardSetRepository.Added += _cardSetRepository_Added;
        }

        private void _cardSetRepository_Added(CardSetModel obj)
        {
            if (obj == null || obj.Questions == null || obj.Questions.Any() == false)
                return;

            var notEmptyCardModel = obj.Questions.Where(q => q != null
                && string.IsNullOrWhiteSpace(q.Question) == false);

            foreach (var cardModel in notEmptyCardModel)
            {
                var cardViewModel = new CardViewModel(cardModel);
                CardList.Add(cardViewModel);
            }
        }

        private string _filterText;

        public string FilterText
        {
            get { return _filterText; }
            set
            {
                _filterText = value;
                RaisePropertyChanged();
                Filter();
            }
        }

        public ObservableCollection<string> FilesReaded { get; } = new ObservableCollection<string>();

        public ObservableCollection<CardViewModel> CardList { get; } = new ObservableCollection<CardViewModel>();

        private void Filter()
        {
            if (string.IsNullOrWhiteSpace(FilterText))
            {
                foreach (var cardViewModel in CardList)
                {
                    cardViewModel.IsVisible = true;
                }
                return;
            }

            var filterText = this.FilterText.Split(' ');
            foreach (var cardViewModel in CardList)
            {
                var visible = FilterCard(cardViewModel, filterText);
                cardViewModel.IsVisible = visible;
            }
        }

        private bool FilterCard(CardViewModel cardViewModel, string[] filterText)
        {
            return filterText.All(cardViewModel.FilterCardPart);
        }


        //public ICollectionView FilteredCards
        //{
        //    get
        //    {
        //        var source = CollectionViewSource.GetDefaultView(CardList);
        //        //source.Filter = p =>
        //    }
        //}
    }
}