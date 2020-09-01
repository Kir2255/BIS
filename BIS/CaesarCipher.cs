using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BIS
{
    public class CaesarCipher
    {
        const string ruAlphabet = "АБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ";
        const string enAlphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

        private string TextEncode(string text, int shift)
        {
            string fullAlphabet = null;
            switch (CheckTextLanguage(text))
            {
                case 1:
                    fullAlphabet = ruAlphabet + ruAlphabet.ToLower();
                    break;
                case 2:
                    fullAlphabet = enAlphabet + enAlphabet.ToLower();
                    break;
                default:
                    return null;
            }

            int alphabetCharCount = fullAlphabet.Length;
            StringBuilder newString = new StringBuilder();

            for (int i = 0; i < text.Length; i++)
            {
                char c = text[i];
                int index = fullAlphabet.IndexOf(c);

                if (index < 0)
                {
                    newString.Append(c.ToString());
                }
                else
                {
                    int encodeIndex = (alphabetCharCount + index + shift) % alphabetCharCount;
                    newString.Append(fullAlphabet[encodeIndex]);
                }
            }

            return newString.ToString();
        }

        public string Encrypt (string text, int shift) => TextEncode(text, shift);
        public string Decrypt (string text, int shift) => TextEncode(text, -shift);

        private int CheckTextLanguage(string text)
        {
            int result = 0;
            if(Regex.IsMatch(text, "^[А-Яа-я\\d\\W]+$"))
            {
                result = 1;
            }
            else if(Regex.IsMatch(text, "^[A-Za-z\\d\\W]+$"))
            {
                result = 2;
            }

            return result;
        }
    }
}
