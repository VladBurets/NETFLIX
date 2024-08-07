using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NETFLIX.Entityes
{
    [Serializable]
    public class Favorites
    {
        public string UserEmail { get; set; }
        public string SeriesTitle { get; set; }
    }
}
