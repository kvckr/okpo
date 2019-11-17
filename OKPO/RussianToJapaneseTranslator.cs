namespace OKPO
{
    public class RussianToJapaneseTranslator : Translator
    {
        public RussianToJapaneseTranslator()
        {
            sourceAlphabetStart = 0x0400;
            sourceAlphabetEnd = 0x04FF;
            difference = 11424;
            targetAlphabetSize = 95;
        }
    }
}
