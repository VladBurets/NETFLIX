using NETFLIX.Data;
using NETFLIX.Entityes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    /// Логика взаимодействия для LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            WorkData.mainWindow.MainFrame.Content = new WelcomePage();
        }

         /////////////////////////////////////////// <кнопка зарегистрироваться>

        private void Label_MouseDown(object sender, MouseButtonEventArgs e)
        {

            WorkData.mainWindow.MainFrame.Content = new RegisterPage();
        }

        private void Label_MouseEnter(object sender, MouseEventArgs e)
        {
            Label label = sender as Label;
            label.Foreground = Brushes.Blue;
        }

        private void Label_MouseLeave(object sender, MouseEventArgs e)
        {
            Label label = sender as Label;
            label.Foreground = Brushes.White;
        }

        /////////////////////////////////////////// </кнопка зарегистрироваться>

        private void Button_Click_1(object sender, RoutedEventArgs e)//вход
        {
            if(PasswordTextBox.Password == "admin" &&  LoginTextBox.Text == "admin")
            {
                WorkData.mainWindow.MainFrame.Content = new AdminPage();
                return;
            }
            User currentUser = WorkData.usersList.FirstOrDefault(x=>x.Password==PasswordTextBox.Password && x.Email==LoginTextBox.Text);
            if(currentUser == null) { MessageBox.Show("Неверный логин или пароль"); return; }
            else
            {
                WorkData.currentUser = currentUser;
                WorkData.mainWindow.MainFrame.Content = new UserPage();
            }
        }
    }
}
