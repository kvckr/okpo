using System;

namespace OKPO
{
    public class TranslationService
    {
        public string Translate(string originalText)
        {
            string result = "";
            foreach (char c in originalText)
            {
                if (c >= 0x0400 && c <= 0x04FF)
                {
                    var targetChar = (char)(c + 11424);
                    var predictChar = 0;
                    while (predictChar != targetChar)
                    {
                        predictChar = (char)new Random().Next(0, 13000);
                    }
                    result += targetChar;
                }
                else
                {
                    result += c;
                }
            }
            return result;
        }
    }
}
