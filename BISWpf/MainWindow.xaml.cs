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
        public MainWindow()
        {
            InitializeComponent();
        }

        private void CaesarMenuItem_Click(object sender, RoutedEventArgs e)
        {
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
    }
}
