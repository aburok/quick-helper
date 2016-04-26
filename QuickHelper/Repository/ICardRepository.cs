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
        event Action<CardSetModel> Updated;
        event Action<CardSetModel> NotAdded;
    }

    public class CardSetRepository : ICardSetRepository
    {
        private ObservableCollection<CardSetModel> CardList { get; }
            = new ObservableCollection<CardSetModel>();

        public event Action<CardSetModel> Added;
        public event Action<CardSetModel> Updated;
        public event Action<CardSetModel> NotAdded;

        public void Add(CardSetModel cardSetModel)
        {
            if (cardSetModel == null 
                || cardSetModel.IdPrefix == null
                || cardSetModel.Questions ==null
                || cardSetModel.Questions.Any() == false)
            {
                NotAdded?.Invoke(cardSetModel);
                return;
            }

            var cardWithSameId = CardList.FirstOrDefault(c => c.IdPrefix == cardSetModel.IdPrefix);
            if (cardWithSameId != null)
            {
                foreach (var cardModel in cardSetModel.Questions)
                {
                    cardWithSameId.Questions.Add(cardModel);
                }
                Updated?.Invoke(cardWithSameId);
            }
            else
            {
                CardList.Add(cardSetModel);
                Added?.Invoke(cardSetModel);
            }
        }

        public IEnumerable<CardSetModel> GetAll()
        {
            return CardList;
        }

        public IEnumerable<CardModel> GetAllCards()
        {
            return CardList.SelectMany(cl => cl.Questions);
        }
    }
}