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
    /// Логика взаимодействия для AdminSeriesElement.xaml
    /// </summary>
    public partial class AdminSeriesElement : UserControl
    {
        public Series currSer { get; set; }
        public AdminSeriesElement(Series series)
        {
            currSer = series;
            InitializeComponent();
            InitFields();
        }
        void InitFields() {
            SeriesTitleBox.Text = currSer.Title;
            int calcRating = WorkData.CalculateRating(currSer);
            if(calcRating == -1) { RatingBox.Text = "нет голосов"; }
            else
             RatingBox.Text = Convert.ToString(calcRating) + "%";
            GenreBox.Text = currSer.Genre;
            CastBox.Text = currSer.Cast;
            DirectorBox.Text = currSer.Director;
            DateBox.Text = currSer.ReleaseDate.Year.ToString();
            CountryBox.Text = currSer.ManufacturerCountry;
            SeriesImage.Source = WorkData.TryLoadPictureFromPath(currSer.ImagePath);
        }
    }
}
