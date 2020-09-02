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
    }
}
