using System;

namespace OKPO
{
    public abstract class Translator
    {
        private const int speedRate = 3000;
        protected int sourceAlphabetStart;
        protected int sourceAlphabetEnd;
        protected int difference;
        protected int targetAlphabetSize;

        public string Translate(string text)
        {
            string result = "";
            foreach (char c in text)
            {
                if (c >= sourceAlphabetStart && c <= sourceAlphabetEnd)
                {
                    var charNumber = c - sourceAlphabetStart;
                    var cuttedCharNumber = sourceAlphabetStart + charNumber % targetAlphabetSize;
                    var targetChar = (char)(cuttedCharNumber + difference);
                    var predictChar = 0;
                    while (predictChar != targetChar)
                    {
                        predictChar = (char)new Random().Next(targetChar - speedRate, targetChar + speedRate);
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
