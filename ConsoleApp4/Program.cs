using BIS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp4
{
    class Program
    {
        static void Main(string[] args)
        {
            ExponentialCipher cipher = new ExponentialCipher();
            string res = cipher.Encrypt("АБВ ГАВ");
            Console.WriteLine(res);
            Console.WriteLine(cipher.Decrypt(res));
            Console.ReadKey();
        }
    }
}
