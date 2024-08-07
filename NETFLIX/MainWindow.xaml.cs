using NETFLIX.Data;
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

namespace NETFLIX
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            LoadData();
            MainFrame.Content = new Pages.WelcomePage();
            WorkData.mainWindow = this;

        }
        void LoadData()
        {
            if (!WorkData.FavoritesDeserialize() || WorkData.favoritesList == null)
            {
                WorkData.favoritesList = new List<Entityes.Favorites>();
            }
            if (!WorkData.UsersDeserialize() || WorkData.usersList == null)
            {
                WorkData.usersList = new List<Entityes.User>();
            }
            if (!WorkData.SeriesDeserialize() || WorkData.seriesList == null)
            {
                WorkData.seriesList = new List<Entityes.Series>();
            }
            if (!WorkData.MarksDeserialize() || WorkData.usersMarksList == null)
            {
                WorkData.usersMarksList = new List<Entityes.UsersMarks>();
            }
        }
    }
}
