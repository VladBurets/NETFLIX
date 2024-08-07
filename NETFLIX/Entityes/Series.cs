using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NETFLIX.Entityes
{
    [Serializable]
    public class Series
    {
        public string Title { get; set; }
        public string Genre { get; set; }
        public string Director { get; set; }
        public string Cast { get; set; }
        public double Rating { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string ManufacturerCountry { get; set; }
        public string ImagePath { get; set; }
        public int PositivVotesCount { get; set; }
        public int NegativeVotesCount { get; set; }
    }
}
