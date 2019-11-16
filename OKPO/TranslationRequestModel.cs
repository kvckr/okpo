namespace OKPO
{
    public class TranslationRequestModel
    {
        public enum Language
        {
            Russian,
            Japanese
        }

        public Language SourceLanguage { get; set; }

        public Language TargetLanguage { get; set; }

        public string Text { get; set; }
    }
}
