using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BIS
{
    public class SwivelGrate
    {
        const string ruAlphabet = "АБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ";
        const string enAlphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

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

        private char GetRandomChar(string alphabet)
        {
            Random rnd = new Random();
            return alphabet[rnd.Next(0, alphabet.Length)];
        }

        private int[][] CreateEmptyGrate(int size)
        {

            int[][] r = new int[size][];
            for (int i = 0; i < size; i++)
            {
                r[i] = new int[size];
            }

            return r;
        }

        private void InsertInfo(int[][] part, int[][] matrix, int fromRow, int fromCol)
        {
            if (fromRow < 0 || fromRow > matrix.Length || fromCol < 0 || fromCol > matrix.Length)
            {
                throw new Exception("Incorrect params");
            }

            for (int i = 0; i < part.Length; i++)
            {
                for (int j = 0; j < part.Length; j++)
                {
                    matrix[i + fromRow][j + fromCol] = part[i][j];
                }
            }
        }

        private int[][] RotateGrate(int[][] grate)
        {
            int[][] rotated = this.CreateEmptyGrate(grate.Length);
            for (int row = 0; row < grate.Length; row++)
            {
                int col = rotated.Length - row - 1;
                for (int i = 0; i < rotated.Length; i++)
                {
                    rotated[i][col] = grate[row][i];
                }
            }

            return rotated;
        }

        /*private int[][] CreateDesk(int partSize)
        {
            int[][] part = this.CreateEmptyGrate(partSize);
            int n = 1;
            for (int i = 0; i < part.Length; i++)
            {
                for (int j = 0; j < part.Length; j++)
                {
                    part[i][j] = n++;

                }
            }

            int[][] desk = this.CreateEmptyGrate();
            this.InsertInfo(part, desk, 0, 0);
        }*/
    }
}
