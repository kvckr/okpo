using System;
using System.Collections.Generic;
using System.Linq;

namespace OKPO
{
    public class TranslationService
    {
        private readonly Dictionary<string, Func<Translator>> translators;

        public TranslationService()
        {
            translators = new Dictionary<string, Func<Translator>>
            {
                { "Russian|Japanese", () => new RussianToJapaneseTranslator()},
                { "Japanese|Russian", () => new JapaneseToRussianTranslator()}
            };
        }

        public string Translate(Language sourceLanguage, Language targetLanguage, string text)
        {
            var translator = translators[sourceLanguage.ToString() + "|" + targetLanguage.ToString()]();
            var result = translator.Translate(text);
            return result;
        }

        public List<LanguageDetailsModel> GetSupportedDestinations(Language sourceLanguage)
        {
            var a = translators.Keys.Where(x => x.Split('|')[0] == sourceLanguage.ToString());

            List<LanguageDetailsModel> availableDestinations = new List<LanguageDetailsModel>();
            foreach (var item in a)
            {
                if (Enum.TryParse(item.Split('|')[1], out Language language))
                {
                    availableDestinations.Add(GetLanguageDetails(language));
                };
            }
            return availableDestinations;
        }

        public List<LanguageDetailsModel> GetLanguagesDetails()
        {
            List<LanguageDetailsModel> languageCodes = new List<LanguageDetailsModel>();
            foreach (Language language in (Language[])Enum.GetValues(typeof(Language)))
            {
                languageCodes.Add(GetLanguageDetails(language));
            }
            return languageCodes;
        }

        private LanguageDetailsModel GetLanguageDetails(Language language)
        {
            return new LanguageDetailsModel
            {
                Code = (int)language,
                Language = language.ToString()
            };
        }
    }
}
