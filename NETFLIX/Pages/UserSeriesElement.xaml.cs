using NETFLIX.Data;
using NETFLIX.Entityes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
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
    /// Логика взаимодействия для UserSeriesElement.xaml
    /// </summary>
    public partial class UserSeriesElement : UserControl
    {
        public Series currSer { get; set; }
        public bool isFavorite { get; set; }
        public Visibility ShowStar { get; set; } = Visibility.Visible;
        public UsersMarks currUserMark { get; set; }
        public UserSeriesElement(Series series)
        {
            currSer = series;
            InitializeComponent();
            DataContext = this;
            InitFields();
            InitMark();
            
        }

        void InitMark()
        {
            currUserMark = WorkData.usersMarksList.FirstOrDefault(x=>x.UserEmail == WorkData.currentUser.Email && x.SeriesTitle == currSer.Title);
            if(currUserMark == null )
            {
                LikeImg.Source = new BitmapImage(new Uri("/Images/LikeWhite.png", UriKind.Relative));
                DislikeImg.Source = new BitmapImage(new Uri("/Images/DislikeWhite.png", UriKind.Relative));
                return;
            }
            if (currUserMark.IsLike)
            {
                LikeImg.Source = new BitmapImage(new Uri("/Images/LikeBlack.png", UriKind.Relative));
                DislikeImg.Source = new BitmapImage(new Uri("/Images/DislikeWhite.png", UriKind.Relative));
            }
            if (!currUserMark.IsLike)
            {
                DislikeImg.Source = new BitmapImage(new Uri("/Images/DislikeBlack.png", UriKind.Relative));
                LikeImg.Source = new BitmapImage(new Uri("/Images/LikeWhite.png", UriKind.Relative));
            }
        }
        void InitFields()
        {
            SeriesTitleBox.Text = currSer.Title;
            int calcRating = WorkData.CalculateRating(currSer);
            if (calcRating == -1) { RatingBox.Text = "нет голосов"; }
            else
                RatingBox.Text = Convert.ToString(calcRating) + "%";
            GenreBox.Text = currSer.Genre;
            CastBox.Text = currSer.Cast;
            DirectorBox.Text = currSer.Director;
            DateBox.Text = currSer.ReleaseDate.Year.ToString();
            CountryBox.Text = currSer.ManufacturerCountry;
            SeriesImage.Source = WorkData.TryLoadPictureFromPath(currSer.ImagePath);
            isFavorite = CheckIsAFavorite();
            SetImageOnStartup();
        }
        bool CheckIsAFavorite()
        {
            Favorites fvr = WorkData.favoritesList.FirstOrDefault(x => x.UserEmail == WorkData.currentUser.Email && x.SeriesTitle == currSer.Title);
            if (fvr != null) { return true; }
            return false;
        }
        void SetImageOnStartup()
        {
            if (isFavorite)
            {
                FavoriteImage.Source = new BitmapImage(new Uri("/Images/YellowStar.png", UriKind.Relative));
                return;
            }
            if (!isFavorite)
            {
                FavoriteImage.Source = new BitmapImage(new Uri("/Images/BlackStar.png", UriKind.Relative));
                return;
            }
        }

        private void FavoriteImage_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (isFavorite) { WorkData.favoritesList.Remove(WorkData.favoritesList.FirstOrDefault(x => x.UserEmail == WorkData.currentUser.Email && x.SeriesTitle == currSer.Title));
                isFavorite = false;
                WorkData.FavoritesSerialize();
                FavoriteImage.Source = new BitmapImage(new Uri("/Images/BlackStar.png", UriKind.Relative));
                MessageBox.Show("Сериал успешно убран из списка избранных");
                return;
            }
            if (!isFavorite)
            {
                WorkData.favoritesList.Add(new Favorites() { SeriesTitle = currSer.Title, UserEmail = WorkData.currentUser.Email });
                isFavorite = true;
                WorkData.FavoritesSerialize();
                FavoriteImage.Source = new BitmapImage(new Uri("/Images/YellowStar.png", UriKind.Relative));
                MessageBox.Show("Сериал успешно добавлен в список избранных");
                return;
            }
        }

        private void FavoriteImage_MouseEnter(object sender, MouseEventArgs e)
        {
            Image img = sender as Image;
            if (isFavorite)
            {
                img.Source = new BitmapImage(new Uri("/Images/BlackStar.png", UriKind.Relative));
            }
            else
            {
                img.Source = new BitmapImage(new Uri("/Images/YellowStar.png", UriKind.Relative));
            }
        }

        private void FavoriteImage_MouseLeave(object sender, MouseEventArgs e)
        {
            Image img = sender as Image;
            if (!isFavorite)
            {
                img.Source = new BitmapImage(new Uri("/Images/BlackStar.png", UriKind.Relative));
            }
            else
            {
                img.Source = new BitmapImage(new Uri("/Images/YellowStar.png", UriKind.Relative));
            }
        }
        bool IsFirstTimeVote(bool isLike)
        {
            if (currUserMark == null)
            {
                WorkData.usersMarksList.Add(new UsersMarks() { IsLike = isLike, SeriesTitle = currSer.Title, UserEmail = WorkData.currentUser.Email });
                if (isLike) { currSer.PositivVotesCount++; } else { currSer.NegativeVotesCount++;}

                return true;
            }
            return false;
        }
        void SaveAndUpdateVoteData()
        {
            WorkData.SeriesSerialize();
            WorkData.MarksSerialize();
            InitMark();
            int calcRating = WorkData.CalculateRating(currSer);
            if (calcRating == -1) { RatingBox.Text = "нет голосов"; }
            else
                RatingBox.Text = Convert.ToString(calcRating) + "%";
            WorkData.usrPage.InitCategories();
        }
        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (IsFirstTimeVote(true)) {  SaveAndUpdateVoteData(); return; }
            if (currUserMark.IsLike) { WorkData.usersMarksList.Remove(currUserMark); currUserMark = null; currSer.PositivVotesCount--; SaveAndUpdateVoteData(); return; }
            if (!currUserMark.IsLike) { currUserMark.IsLike = true; currSer.PositivVotesCount++; currSer.NegativeVotesCount--; SaveAndUpdateVoteData(); return; }
        }

        private void Image_MouseDown_1(object sender, MouseButtonEventArgs e)
        {
            if(IsFirstTimeVote(false)) { SaveAndUpdateVoteData(); return; }
            if(!currUserMark.IsLike) { WorkData.usersMarksList.Remove(currUserMark); currUserMark = null; currSer.NegativeVotesCount--; SaveAndUpdateVoteData();return; }
            if (currUserMark.IsLike) { currUserMark.IsLike = false; currSer.PositivVotesCount--; currSer.NegativeVotesCount++; SaveAndUpdateVoteData(); return; }
        }
    }
}
