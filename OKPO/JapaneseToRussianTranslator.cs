namespace OKPO
{
    public class JapaneseToRussianTranslator : Translator
    {
        public JapaneseToRussianTranslator()
        {
            sourceAlphabetStart = 0x30A0;
            sourceAlphabetEnd = 0x30FF;
            difference = -11424;
            targetAlphabetSize = 255;
        }
    }
}
