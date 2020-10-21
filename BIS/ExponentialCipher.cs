using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BIS
{
    public class ExponentialCipher
    {
        const string ruAlphabet = "АБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ";
        const string enAlphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

        public int a = 3;
        public int p = 991;

        public ExponentialCipher() { }
        public ExponentialCipher(int a, int p)
        {
            if (IsPrimeNumber(p))
            {
                this.p = p;

                if (a >= 1 && a <= p)
                {
                    this.a = a;
                }
            }
        }

        public string Encrypt(string text)
        {
            string fullAlphabet = ChoseAlphabet(CheckTextLanguage(text));
            StringBuilder result = new StringBuilder();

            List<string> words = text.Trim().ToUpper().Split(' ').ToList();
            foreach (string word in words)
            {
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
                        result.Append(Math.Pow(this.a, index + 1) % this.p);
                        if (i != word.Length - 1)
                        {
                            result.Append("-");
                        }
                        
                    }
                }

                result.Append(" ");
            }

            return result.ToString();
        }

        public string Decrypt(string text)
        {
            string fullAlphabet = ChoseAlphabet(CheckTextLanguage(text));
            StringBuilder result = new StringBuilder();

            List<string> words = text.Trim().ToUpper().Split(' ').ToList();
            for(int i = 0; i < words.Count; i++)
            {
                List<string> numbers = words[i].Trim().Split('-').ToList();
                foreach (string number in numbers)
                {
                    int temp = 0;
                    while (true)
                    {
                        var x = Math.Log(this.p * temp + Convert.ToInt32(number)) / Math.Log(this.a);
                        temp++;
                        if (x == Math.Truncate(x))
                        {
                            result.Append(fullAlphabet[Convert.ToInt32(x) - 1]);
                            break;
                        }
                    }
                }

               /* for (int j = 0; j < numbers.Count; j++)
                {
                    int temp = 1;
                    while (true)
                    {
                        var x = Math.Log(Convert.ToInt32(numbers[i]) * temp + this.p) / Math.Log(this.a);
                        temp++;
                        if (x == Math.Truncate(x))
                        {
                            result.Append(fullAlphabet[Convert.ToInt32(x)]);
                        }
                    }
                }*/

                result.Append(" ");
            }

            return result.ToString();
        }

        private int CheckTextLanguage(string text)
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

        private string ChoseAlphabet(int choise)
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

        private bool IsPrimeNumber(int n)
        {
            var result = true;

            if (n > 1)
            {
                for (var i = 2u; i < n; i++)
                {
                    if (n % i == 0)
                    {
                        result = false;
                        break;
                    }
                }
            }
            else
            {
                result = false;
            }

            return result;
        }
    }
}
