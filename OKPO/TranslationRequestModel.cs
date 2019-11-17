namespace OKPO
{
    public enum Language
    {
        Russian,
        Japanese
    }

    public class TranslationRequestModel
    {
        public Language SourceLanguage { get; set; }

        public Language TargetLanguage { get; set; }

        public string Text { get; set; }
    }
}
