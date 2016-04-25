namespace QuickHelper.Card
{
    public class CardModel
    {
        public string Id { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
        public string Tags { get; set; }
        public bool IsAnswerACode { get; set; }
    }
}