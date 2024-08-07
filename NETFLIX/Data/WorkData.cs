using NETFLIX.Entityes;
using NETFLIX.Pages;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace NETFLIX.Data
{
    public class WorkData
    {
        public static MainWindow mainWindow { get; set; }
        public static UserPage usrPage { get; set; }
        public static List<User> usersList { get; set; }
        public static List<Series> seriesList { get; set; }
        public static List<UsersMarks > usersMarksList { get; set; }
        public static User currentUser { get; set; }
        public static List<Favorites> favoritesList { get; set;}
        static string UsersDataPath = "UsersData.xml";
        static string SeriesDataPath = "SeriesData.xml";
        static string FavoritesDataPath = "FavoritesData.xml";
        static string UsersMarksDataPath = "UsersMarksData.xml";
        public static int CalculateRating(Series series)
        {
            if(series.NegativeVotesCount ==0 && series.PositivVotesCount ==0) return -1;
            int summVotes = series.NegativeVotesCount + series.PositivVotesCount;
            return (int)(((double)series.PositivVotesCount / (double)summVotes) * 100); 

        }
        public static string SeriesToExportText(Series series)
        {
            string rating = "";
            int ratingInt = WorkData.CalculateRating(series);
            if (ratingInt == -1)
            {
                rating = "нет голосов";
            }
            else rating = ratingInt.ToString()+ "%"; 
            return $"Название: {series.Title}\nЖанр: {series.Genre}\nРежиссёр: {series.Director}\nАктёрский состав: {series.Cast}\nРейтинг: {rating}" +
                $"\nГод релиза: {series.ReleaseDate}\nСтрана производства: {series.ManufacturerCountry}\n///////////////////////////////////////////////\n";
        }
        public static BitmapImage TryLoadPictureFromPath(string path)
        {
            try
            {
                return new BitmapImage(new Uri(path));
            }
            catch { return new BitmapImage(new Uri("/Images/image-not-found-4a963b95bf081c3ea02923dceaeb3f8085e1a654fc54840aac61a57a60903fef.png", UriKind.Relative)); }
        }
        public static bool UsersDeserialize()
        {
            try
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<User>));
                using (FileStream fs = new FileStream(UsersDataPath, FileMode.OpenOrCreate))
                {
                    usersList = xmlSerializer.Deserialize(fs) as List<User>;
                }
                return true;
            }
            catch {
                return false;
            }
        }
        public static bool MarksDeserialize()
        {
            try
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<UsersMarks>));
                using (FileStream fs = new FileStream(UsersMarksDataPath, FileMode.OpenOrCreate))
                {
                    usersMarksList = xmlSerializer.Deserialize(fs) as List<UsersMarks>;
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
        public static bool SeriesDeserialize()
        {
            try
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Series>));
                using (FileStream fs = new FileStream(SeriesDataPath, FileMode.OpenOrCreate))
                {
                    seriesList = xmlSerializer.Deserialize(fs) as List<Series>;
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
        public static bool FavoritesDeserialize()
        {
            try
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Favorites>));
                using (FileStream fs = new FileStream(FavoritesDataPath, FileMode.OpenOrCreate))
                {
                    favoritesList = xmlSerializer.Deserialize(fs) as List<Favorites>;
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
        public static bool UsersSerialize()
        {
            try
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<User>));
                using (FileStream fs = new FileStream(UsersDataPath, FileMode.Create))
                {
                    xmlSerializer.Serialize(fs, usersList);
                }
                return true;
            }
            catch { return false; }
        }
        public static bool SeriesSerialize()
        {
            try
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Series>));
                using (FileStream fs = new FileStream(SeriesDataPath, FileMode.Create))
                {
                    xmlSerializer.Serialize(fs, seriesList);
                }
                return true;
            }
            catch {  return false; }
        }
        public static bool FavoritesSerialize()
        {
            try
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Favorites>));
                using (FileStream fs = new FileStream(FavoritesDataPath, FileMode.Create))
                {
                    xmlSerializer.Serialize(fs, favoritesList);
                }
                return true;
            }
            catch { return false; }
        }
        public static bool MarksSerialize()
        {
            try
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<UsersMarks>));
                using (FileStream fs = new FileStream(UsersMarksDataPath, FileMode.Create))
                {
                    xmlSerializer.Serialize(fs, usersMarksList);
                }
                return true;
            }
            catch { return false; }
        }
    }
}
