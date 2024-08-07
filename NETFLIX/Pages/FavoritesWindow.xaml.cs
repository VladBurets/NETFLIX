using NETFLIX.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Shapes;

namespace NETFLIX.Pages
{
    /// <summary>
    /// Логика взаимодействия для FavoritesWindow.xaml
    /// </summary>
    public partial class FavoritesWindow : Window
    {
        public ObservableCollection<UserSeriesElement> UserFavorSeriesElems { get; set; }
        public FavoritesWindow()
        {
            InitializeComponent();
            UserFavorSeriesElems = new ObservableCollection<UserSeriesElement>();
            DataContext = this;
            UploadSeries();
        }
        void UploadSeries()
        {
            foreach (var x in WorkData.seriesList)
            {
                try
                {
                    UserSeriesElement favSer = new UserSeriesElement(x);
                    if (favSer.isFavorite)
                    {
                        favSer.ShowStar = Visibility.Hidden;
                        UserFavorSeriesElems.Add(favSer);
                    }
                    else { continue; }
                }
                catch { }
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)//export
        {
            ExportWindow wnd = new ExportWindow(UserFavorSeriesElems);
            wnd.ShowDialog();
        }

        private void Button_Click(object sender, RoutedEventArgs e)//search
        {
            Regex reg = new Regex(SearchTextBox.Text);
            UserFavorSeriesElems.Clear();
            foreach (var x in WorkData.seriesList)
            {
                UserSeriesElement favSer = new UserSeriesElement(x);
                if (favSer.isFavorite && reg.IsMatch(WorkData.SeriesToExportText(x)))
                {
                    favSer.ShowStar = Visibility.Hidden;
                    UserFavorSeriesElems.Add(favSer);
                }
                else { continue; }
            }
        }
    }
}
