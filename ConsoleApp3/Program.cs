using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BIS;

namespace ConsoleApp3
{
    class Program
    {
        static void Main(string[] args)
        {
            string res = MultipleGamma.Encrypt("Привет1 мир");
            Console.WriteLine(res);
            Console.WriteLine(MultipleGamma.Decrypt(res, MultipleGamma.gammas));
            Console.ReadKey();
        }
    }
}
