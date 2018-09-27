using System.Collections.Generic;

namespace QuickHelper.Card
{
    public class CardSetModel
    {

        public string FilePath { get; set; }

        public string Subject { get; set; }

        public string Tags { get; set; }
        public string IdPrefix { get; set; }
        public ICollection<CardModel> Questions { get; set; }

        public CardSetModel()
        {
            Questions = new List<CardModel>();
        }

        public void Init()
        {
            foreach (var cardModel in Questions)
            {
                cardModel.Tags += " " + this.Tags;
            }
        }

        public static CardSetModel Empty = new CardSetModel();
    }
}