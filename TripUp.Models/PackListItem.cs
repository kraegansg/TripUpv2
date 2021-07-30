using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripUp.Models
{
    public class PackListItem
    {
        public int PackId { get; set; }
        public string PackName { get; set; }
        public string Clothes { get; set; }
        public string BathItems { get; set; }
        public string Essentials { get; set; }
        public string Other { get; set; }
    }
}
