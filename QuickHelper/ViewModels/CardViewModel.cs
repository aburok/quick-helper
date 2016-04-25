using System.Linq;
using QuickHelper.Card;

namespace QuickHelper.ViewModels
{
    public class CardViewModel : BaseViewModel
    {
        private bool _isVisible;

        public CardViewModel(CardModel cardModel)
        {
            Question = cardModel.Question;
            Answer = cardModel.Answer;
            if (string.IsNullOrWhiteSpace(cardModel.Tags) == false)
            {
                Tags = cardModel.Tags.Split(' ', ';', ',');
            }
            IsVisible = true;
        }

        public string Question { get; set; }
        public string Answer { get; set; }
        public string[] Tags { get; set; }

        public string TagsCombined
        {
            get
            {
                if (this.Tags != null && this.Tags.Any())
                {
                return string.Join(" ", this.Tags);
                }
                return string.Empty;
            }
        }

        public bool IsVisible
        {
            get { return _isVisible; }
            set { _isVisible = value; RaisePropertyChanged(); }
        }

        public bool FilterCardPart(string textPart)
        {
            return this.Question.ToLower().Contains(textPart.ToLower())
                   || this.Answer.ToLower().Contains(textPart.ToLower())
                   || (this.Tags != null && this.Tags.Any(t=> t.ToLower().Contains(textPart.ToLower())));
        }
    }
}