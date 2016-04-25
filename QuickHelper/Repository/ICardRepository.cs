using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using QuickHelper.Card;

namespace QuickHelper.Repository
{
    public interface ICardSetRepository
    {
        void Add(CardSetModel cardSetModel);
        IEnumerable<CardSetModel> GetAll();
        IEnumerable<CardModel> GetAllCards();

        event Action<CardSetModel> Added;
    }

    public class CardSetRepository : ICardSetRepository
    {
        private ObservableCollection<CardSetModel> CardList { get; } 
            = new ObservableCollection<CardSetModel>();

        public event Action<CardSetModel> Added;

        public void Add(CardSetModel cardSetModel)
        {
            CardList.Add(cardSetModel);
            Added?.Invoke(cardSetModel);
        }

        public IEnumerable<CardSetModel> GetAll()
        {
            return CardList;
        }

        public IEnumerable<CardModel> GetAllCards()
        {
            return CardList.SelectMany(cl=> cl.Questions);
        }
    }
}