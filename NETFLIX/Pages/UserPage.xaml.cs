using NETFLIX.Data;
using NETFLIX.Entityes;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace NETFLIX.Pages
{
    /// <summary>
    /// Логика взаимодействия для UserPage.xaml
    /// </summary>
    public partial class UserPage : Page
    {
        public ObservableCollection<UserSeriesElement> UserSeriesElems { get; set; }
        public UserPage()
        {
            InitializeComponent();
            WorkData.usrPage = this;
            DataContext = this;
            UserSeriesElems = new ObservableCollection<UserSeriesElement>();
            EmailLabel.Content = WorkData.currentUser.Email;
            UploadSeries();
            InitCategories();
        }
       public void InitCategories()
        {
            try
            {
                GenresComboBox.Items.Clear();
                CountryComboBox.Items.Clear();
                RatingComboBox.Items.Clear();
                bool isUnique = true;
                foreach (var x in WorkData.seriesList)
                {
                    foreach (var z in GenresComboBox.Items)
                    {
                        if (x.Genre == z.ToString())
                        {
                            isUnique = false; break;
                        }
                    }
                    if (isUnique) { GenresComboBox.Items.Add(x.Genre); }
                }
                isUnique = true;
                foreach (var x in WorkData.seriesList)
                {
                    foreach (var z in CountryComboBox.Items)
                    {
                        if (x.ManufacturerCountry == z.ToString())
                        {
                            isUnique = false; break;
                        }
                    }
                    if (isUnique) { CountryComboBox.Items.Add(x.ManufacturerCountry); }
                }
                isUnique = true;
                foreach (var x in UserSeriesElems)
                {
                    foreach (var z in RatingComboBox.Items)
                    {
                        if (x.RatingBox.Text == z.ToString())
                        {
                            isUnique = false; break;
                        }
                    }
                    if (isUnique) { RatingComboBox.Items.Add(x.RatingBox.Text); }
                }
            }
            catch { }
        }
        void UploadSeries()
        {
            foreach(var x in WorkData.seriesList)
            {
                try
                {
                    UserSeriesElems.Add(new UserSeriesElement(x));
                }
                catch { }
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)//выход
        {
            WorkData.currentUser = null;
            WorkData.mainWindow.MainFrame.Content = new WelcomePage();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)//избранное
        {
            FavoritesWindow wnd = new FavoritesWindow();
            wnd.ShowDialog();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Regex reg = new Regex(SearchTextBox.Text);
            Regex genrReg = new Regex(GenresComboBox.Text);
            Regex ratingReg = new Regex(RatingComboBox.Text);
            Regex countryReg = new Regex(CountryComboBox.Text);
            Regex directorReg = new Regex(DirectorTextBox.Text);
            Regex castReg = new Regex(CustTextBox.Text);
            UserSeriesElems.Clear();   
            bool ratingCheck;
            foreach (var x in WorkData.seriesList)
            {
                if ( WorkData.CalculateRating(x) != -1){ ratingCheck = ratingReg.IsMatch(WorkData.CalculateRating(x).ToString() + "%"); }
                else ratingCheck = ratingReg.IsMatch("нет голосов");
                if (reg.IsMatch(WorkData.SeriesToExportText(x)) && genrReg.IsMatch(x.Genre) && ratingCheck && countryReg.IsMatch(x.ManufacturerCountry)
                    && directorReg.IsMatch(x.Director) && castReg.IsMatch(x.Cast))
                {
                    UserSeriesElems.Add(new UserSeriesElement(x));
                }
                else { continue; }
            }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            SearchTextBox.Text = "";
            RatingComboBox.Text = "";
            GenresComboBox.Text = "";
            CountryComboBox.Text = "";
            DirectorTextBox.Text = "";
            CustTextBox.Text = "";
        }
    }
}
