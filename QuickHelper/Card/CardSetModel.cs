using System.Collections.Generic;

namespace QuickHelper.Card
{
    public class CardSetModel
    {
        public string FilePath { get; set; }

        public string Tags { get; set; }
        public string IdPrefix { get; set; }
        public IEnumerable<CardModel> Questions { get; set; }

        public void Init()
        {
            foreach (var cardModel in Questions)
            {
                cardModel.Tags = this.Tags;
            }
        }
    }
}