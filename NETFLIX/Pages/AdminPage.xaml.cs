using Microsoft.Win32;
using NETFLIX.Data;
using NETFLIX.Entityes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace NETFLIX.Pages
{
    /// <summary>
    /// Логика взаимодействия для AdminPage.xaml
    /// </summary>
    public partial class AdminPage : Page
    {
        public ObservableCollection<AdminSeriesElement> SeriesElementsList { get; set; }
        public AdminSeriesElement SelectedSeries { get; set; }

        public AdminPage()
        {
            InitializeComponent();
            DataContext = this;
            SeriesElementsList = new ObservableCollection<AdminSeriesElement>();
            UploadSeriesElemsList();
        }
        void UploadSeriesElemsList()
        {
            SeriesElementsList.Clear();
            foreach(var x in WorkData.seriesList)
            {
                try
                {
                    SeriesElementsList.Add(new AdminSeriesElement(x));
                }
                catch { }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)//выход
        {
            WorkData.mainWindow.MainFrame.Content = new WelcomePage();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)//сохранить
        {
            var checkIsExistSeriesEntity = WorkData.seriesList.FirstOrDefault(x=>x.Title == TitleTextBox.Text);
            if (checkIsExistSeriesEntity != null)
            {
                MessageBox.Show("Сериал с таким названием уже существует");
                return;
            }
            Series newSeries = new Series();
            newSeries.Title = TitleTextBox.Text;
            newSeries.Genre = GenreTextBox.Text;
            newSeries.Cast = CastTextBox.Text;
            newSeries.Director = DirectorTextBox.Text;
            try
            {
                newSeries.ReleaseDate = ReleaseDateTextBox.SelectedDate.Value;
            }
            catch { newSeries.ReleaseDate = DateTime.Today; }
            newSeries.Rating = 0;
            newSeries.ManufacturerCountry = ManufacturerСountryTextBox.Text;
            newSeries.ImagePath = seriesImg;
            WorkData.seriesList.Add(newSeries);
            if (!WorkData.SeriesSerialize())
            {
                MessageBox.Show("Ошибка сохранения данных");
                return;
            }
            TitleTextBox.Text = "";
            GenreTextBox.Text = "";
            CastTextBox.Text = "";
            DirectorTextBox.Text = "";
            ReleaseDateTextBox.SelectedDate = null;
            ManufacturerСountryTextBox.Text = "";
            MessageBox.Show("Добавление информации прошло успешно!");
            UploadSeriesElemsList();
        }
        string seriesImg;
        private void Image_MouseDown(object sender, MouseButtonEventArgs e)//выбор картинки
        {
            try
            {   //открытие диалоговог окна
                OpenFileDialog openwnd = new OpenFileDialog
                {
                    Filter = "Image files(*.png)|*.png|Image files(*.jpg)|*.jpg"
                };
                openwnd.ShowDialog();
                SeriesImage.Source = new BitmapImage(new Uri(openwnd.FileName));
                seriesImg = openwnd.FileName;
            }
            catch
            {
                //SeriesImage.Source = new BitmapImage(new Uri("Images/image-not-found-4a963b95bf081c3ea02923dceaeb3f8085e1a654fc54840aac61a57a60903fef.png", UriKind.Relative));
               // seriesImg = SeriesImage.Source.ToString();
                return;
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)//удаление
        {
            try
            {
                if (SelectedSeries == null) { return; }
                if (MessageBox.Show("Вы действительно хотите удалить удалить?", "Удаление сериала", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    WorkData.seriesList.Remove(SelectedSeries.currSer);
                    if (WorkData.SeriesSerialize())
                    {
                        MessageBox.Show("Сериал успешно удален");
                        UploadSeriesElemsList();
                    }
                }
            }
            catch { }
        }
    }
}
