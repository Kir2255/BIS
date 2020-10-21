using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using BIS;

namespace BISWpf
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MagicSquare magicSquare;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ChangeVisibility()
        {
            CaesarCipherCanvas.Visibility = Visibility.Hidden;
            MagicSquareCanvas.Visibility = Visibility.Hidden;
            RSACanvas.Visibility = Visibility.Hidden;
        }

        private void CaesarMenuItem_Click(object sender, RoutedEventArgs e)
        {
            ChangeVisibility();
            CaesarCipherCanvas.Visibility = Visibility.Visible;
        }

        private void EncryptButton_Click(object sender, RoutedEventArgs e)
        {
            if (Regex.IsMatch(ShiftStepBox.Text, "^\\d+$"))
            {
                EncryptedText.Content = "";
                DecryptedText.Content = "";

                CaesarCipher caesarCipher = new CaesarCipher();
                string temp = caesarCipher.Encrypt(InputStringToEncodeBox.Text, Convert.ToInt32(ShiftStepBox.Text));

                if (temp == null)
                {
                    MessageBox.Show("Неверный ввод строки для шифрования.", "Ошибка");
                }
                else
                {
                    EncryptedText.Content = "Encrypted string: " + temp;
                }
            }
        }

        private void DecryptButton_Click(object sender, RoutedEventArgs e)
        {
            if (Regex.IsMatch(ShiftStepBox.Text, "^\\d+$"))
            {
                DecryptedText.Content = "";

                CaesarCipher caesarCipher = new CaesarCipher();
                string temp = caesarCipher.Decrypt(EncryptedText.Content.ToString().Replace("Encrypted string: ", ""), Convert.ToInt32(ShiftStepBox.Text));
                if (temp == null)
                {
                    MessageBox.Show("Строка для дешифорвания отсутствует.", "Ошибка");
                }
                else
                {
                    DecryptedText.Content = "Decrypted string: " + temp;
                }
            }
        }

        private void SquareEncryptButton_Click(object sender, RoutedEventArgs e)
        {
            EncryptedMagicStringLabel.Content = "";
            MagicSquareTextLabel.Content = "";
            DecryptedMagicStringLabel.Content = "";
            MagicSquareLabel.Content = "";

            if (MagicStringToEncodeBox.Text == null)
            {
                MessageBox.Show("Неверный ввод строки для шифрования.", "Ошибка");
            }
            else
            {
                magicSquare = new MagicSquare();

                EncryptedMagicStringLabel.Content = "Encrypted string: " + magicSquare.Encrypt(MagicStringToEncodeBox.Text);
                MagicSquareLabel.Content = magicSquare.magicSquareString;
                MagicSquareTextLabel.Content = magicSquare.resultMagicSquareString;
            }
        }

        private void MagicSquareMenuItem_Click(object sender, RoutedEventArgs e)
        {
            ChangeVisibility();
            MagicSquareCanvas.Visibility = Visibility.Visible;
        }

        private void SquareDecryptButton_Click(object sender, RoutedEventArgs e)
        {
            DecryptedMagicStringLabel.Content = "";

            if (EncryptedMagicStringLabel.Content == null)
            {
                MessageBox.Show("Строка для дешифорвания отсутствует.", "Ошибка");
            }
            else
            {
                DecryptedMagicStringLabel.Content = "Decrypted string: " + magicSquare.Decrypt(EncryptedMagicStringLabel.Content.ToString().Replace("Encrypted string: ", ""), magicSquare.magicSquare);
            }
        }

        private void RSAEncryptButton_Click(object sender, RoutedEventArgs e)
        {
            if (RSAToEncodeBox.Text.Length > 0)
            {
                if (PrimePNumberBox.Text.Length > 0 && PrimeQNumberBox.Text.Length > 0)
                {
                    long p = Convert.ToInt64(PrimePNumberBox.Text);
                    long q = Convert.ToInt64(PrimeQNumberBox.Text);

                    if (RSA.IsTheNumberSimple(q) && RSA.IsTheNumberSimple(p))
                    {
                        long n = p * q;
                        long m = (p - 1) * (q - 1);
                        long d = RSA.Calculate_d(m);
                        long e_ = RSA.Calculate_e(d, m);

                        List<string> result = new RSA().Encrypt(RSAToEncodeBox.Text.Trim().ToUpper(), e_, n);

                        foreach (string item in result)
                        {
                            EncryptedRSAStringLabel.Content += item + " ";
                        }

                        DKeyNumberBox.Text = d.ToString();
                        NKeyNumberBox.Text = n.ToString();
                    }
                    else
                    {
                        MessageBox.Show("p или q - не простые числа!");
                    }
                }
                else
                {
                    MessageBox.Show("Введите p и q!");
                }
            }
            else
            {
                MessageBox.Show("Введите тескт для шифрования");
            }
        }

        private void RSADecryptButton_Click(object sender, RoutedEventArgs e)
        {
            if (EncryptedRSAStringLabel.Content.ToString().Length > 0)
            {
                if (DKeyNumberBox.Text.Length > 0 && NKeyNumberBox.Text.Length > 0)
                {
                    long d = Convert.ToInt64(DKeyNumberBox.Text);
                    long n = Convert.ToInt64(NKeyNumberBox.Text);

                    List<string> input = EncryptedRSAStringLabel.Content.ToString().Trim().Split(' ').ToList();
                    DecryptedRSAStringLabel.Content = new RSA().Decrypt(input, d, n);
                }
                else
                {
                    MessageBox.Show("Отсутствует секретный ключ");
                }
            }
            else
            {
                MessageBox.Show("Текст для дешифровки отсутсвует");
            }
        }

        private void RSAMenuItem_Click(object sender, RoutedEventArgs e)
        {
            ChangeVisibility();
            RSACanvas.Visibility = Visibility.Visible;
        }
    }
}
