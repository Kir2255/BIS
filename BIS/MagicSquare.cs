using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIS
{
    public class MagicSquare
    {
        public int[,] magicSquare;
        private int d;
        public string resultMagicSquareString;
        public string magicSquareString;
        public string text;

        private void CreateMagicSquare(string line)
        {
            text = line.ToUpper().Replace(" ", "");
            d = (int)Math.Ceiling(Math.Sqrt(text.Length));

            if (d % 2 != 1)
                d++;

            StringBuilder magicSquareStringBuilder = new StringBuilder();  

            magicSquare = new int[d, d];

            for (int j = 0; j < d; j++)
            {
                for (int i = 0; i < d; i++)
                {
                    magicSquare[i, j] = d * (((i + 1) + (j + 1) - 1 + (d / 2)) % d) + (((i + 1) + 2 * (j + 1) - 2) % d) + 1;
                    magicSquareStringBuilder.Append(magicSquare[i, j].ToString() + "\t");
                }
                magicSquareStringBuilder.Append("\n");
            }

            magicSquareString = magicSquareStringBuilder.ToString();
        }

        public string Encrypt(string line)
        {
            CreateMagicSquare(line);

            StringBuilder resultMagicSquare = new StringBuilder();
            StringBuilder encryptedString = new StringBuilder();
            for (int j = 0; j < d; j++)
            {
                for (int i = 0; i < d; i++)
                {
                    if ((magicSquare[i, j] - 1) < text.Length)
                    {
                        resultMagicSquare.Append(text[magicSquare[i, j] - 1] + "\t");
                        encryptedString.Append(text[magicSquare[i, j] - 1]);
                    }
                    else
                    {
                        resultMagicSquare.Append(" " + "\t");
                        encryptedString.Append(" ");
                    }
                }
                resultMagicSquare.Append("\n");
            }
            resultMagicSquareString = resultMagicSquare.ToString();

            return encryptedString.ToString();
        }

        public string Decrypt(string line, int[,] magicSquare)
        {
            StringBuilder decryptedString = new StringBuilder();

            int[] indexes = new int[magicSquare.Length];

            for (int i = 0; i < magicSquare.Length; i++)
            {
                indexes[i] += i + 1;
            }

            char[] temp = line.ToArray();
            char[,] newTemp = new char[magicSquare.GetLength(0), magicSquare.GetLength(1)];

            for (int i = 0; i < magicSquare.GetLength(0); i++)
            {
                for (int j = 0; j < magicSquare.GetLength(1); j++)
                {
                    newTemp[i, j] = temp[i * magicSquare.GetLength(0) + j];
                }
            }


            for (int i = 0; i < indexes.Length; i++)
            {
                for (int j = 0; j < magicSquare.GetLength(0); j++)
                {
                    for (int k = 0; k < magicSquare.GetLength(1); k++)
                    {
                        if (indexes[i] == magicSquare[j, k])
                        {
                            decryptedString.Append(newTemp[k, j]);
                        }
                    }
                }
            }

            return decryptedString.ToString();
        }
    }
}
