using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BIS;

namespace ConsoleApp2
{
    class Program
    {
        private const string Line = "Меня зовут Кирилл";

        static void Main(string[] args)
        {
            MagicSquare magicSquare = new MagicSquare();

            Console.WriteLine(magicSquare.Encrypt(Line));
            Console.WriteLine(magicSquare.magicSquareString);
            Console.WriteLine(magicSquare.resultMagicSquareString);
            Console.WriteLine(magicSquare.Decrypt(magicSquare.Encrypt(Line), magicSquare.magicSquare));
            Console.ReadKey();
        }
    }
}

