using NETFLIX.Data;
using NETFLIX.Entityes;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace NETFLIX.Pages
{
    /// <summary>
    /// Логика взаимодействия для RegisterPage.xaml
    /// </summary>
    public partial class RegisterPage : Page
    {
        public RegisterPage()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)//назад
        {
            WorkData.mainWindow.MainFrame.Content = new LoginPage();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)//зарегистрироваться
        {
            Regex emailRegex = new Regex(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$");
            Regex passwordRegex = new Regex(@"^(?=.{8,16}$)(?=.*?[a-z])(?=.*?[A-Z])(?=.*?[0-9]).*$");
            if (!emailRegex.IsMatch(EmailTextBox.Text)) { MessageBox.Show("Неправильный формат электронной почты"); return; }
            var checkEmailUser = WorkData.usersList.FirstOrDefault(x => x.Email == EmailTextBox.Text);
            if (checkEmailUser != null) { MessageBox.Show("Пользователь с такой электронной почтой уже зарегистрирован"); return; }
            if (!passwordRegex.IsMatch(PasswordBox.Password)) { MessageBox.Show("В пароле должны быть: цифра, буквы нижнего и верхнего регистра, длина от 8 до 16 символов"); return; }
            if (PasswordBox.Password != RepeatPasswordBox.Password) { MessageBox.Show("Пароли не совпадают"); return; }
            User newUser = new User();
            newUser.Email = EmailTextBox.Text;
            newUser.Password = PasswordBox.Password;
            WorkData.usersList.Add(newUser);
            if (!WorkData.UsersSerialize())
            {
                MessageBox.Show("Не удалось зарегистрироваться");
                return;
            }
            MessageBox.Show("Регистрация прошла успешно!");
            WorkData.mainWindow.MainFrame.Content = new LoginPage();
        }
    }
}
