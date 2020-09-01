using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BIS;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            CaesarCipher caesarCipher = new CaesarCipher();
            string s = caesarCipher.Encrypt("Привет, меня зовут Кирилл.", 6);
            Console.WriteLine(s);
            Console.WriteLine(caesarCipher.Decrypt(s, 6));
            Console.ReadKey();
        }
    }
}
