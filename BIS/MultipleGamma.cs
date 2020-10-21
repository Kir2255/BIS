using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BIS
{
    public class MultipleGamma
    {
        const string ruAlphabet = "АБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ";
        const string enAlphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        public static List<string> gammas = new List<string>();

        public static string GenerateRandomString(int wordLength, string fullAlphabet)
        {
            Random rnd = new Random();
            StringBuilder buffer = new StringBuilder();            

            while(buffer.Length != wordLength)
            {
                int index = rnd.Next(0, fullAlphabet.Length);
                buffer.Append(fullAlphabet[index]);
                fullAlphabet = fullAlphabet.Remove(index, 1);
            }

            return buffer.ToString();
        }

        public static string Encrypt(string text)
        {
            string fullAlphabet = ChoseAlphabet(CheckTextLanguage(text));
            int alphabetCharCount = fullAlphabet.Length;
            StringBuilder result = new StringBuilder();


            List<string> words = text.Trim().ToUpper().Split(' ').ToList();
            foreach (string word in words)
            {
                string gamma = GenerateRandomString(word.Length, fullAlphabet);
                gammas.Add(gamma);

                for (int i = 0; i < word.Length; i++)
                {
                    char c = word[i];
                    int index = fullAlphabet.IndexOf(c);

                    if (index < 0)
                    {
                        result.Append(c.ToString());
                    }
                    else
                    {
                        int encodeIndex = (index + fullAlphabet.IndexOf(gamma[i])) % alphabetCharCount;
                        result.Append(fullAlphabet[encodeIndex]);
                    }
                }

                result.Append(" ");
            }

            return result.ToString();
        }

        public static string Decrypt(string text, List<string> gammas)
        {
            string fullAlphabet = ChoseAlphabet(CheckTextLanguage(text));
            int alphabetCharCount = fullAlphabet.Length;
            StringBuilder result = new StringBuilder();


            List<string> words = text.Trim().ToUpper().Split(' ').ToList();
            for (int i = 0; i < words.Count; i++)
            {
                string gamma = gammas[i];

                for (int j = 0; j < words[i].Length; j++)
                {
                    char c = words[i][j];
                    int index = fullAlphabet.IndexOf(c);

                    if (index < 0)
                    {
                        result.Append(c.ToString());
                    }
                    else
                    {
                        int encodeIndex = (index + alphabetCharCount - fullAlphabet.IndexOf(gamma[j])) % alphabetCharCount;
                        result.Append(fullAlphabet[encodeIndex]);
                    }
                }

                result.Append(" ");
            }

            return result.ToString();
        }

        private static int CheckTextLanguage(string text)
        {
            int result = 0;
            if (Regex.IsMatch(text, "^[А-Яа-я\\d\\W]+$"))
            {
                result = 1;
            }
            else if (Regex.IsMatch(text, "^[A-Za-z\\d\\W]+$"))
            {
                result = 2;
            }

            return result;
        }

        private static string ChoseAlphabet(int choise)
        {
            string fullAlphabet = String.Empty;
            switch (choise)
            {
                case 1:
                    fullAlphabet = ruAlphabet;
                    break;
                case 2:
                    fullAlphabet = enAlphabet;
                    break;
                default:
                    return String.Empty;
            }

            return fullAlphabet;
        }
    }
}
