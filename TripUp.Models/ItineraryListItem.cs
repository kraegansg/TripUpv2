using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripUp.Models
{
    public class ItineraryListItem
    {
        public int ItineraryId { get; set; }
        public string ItineraryName { get; set; }
        public string PitStop { get; set; }
        public int TravelDistance { get; set; }
        public DateTime TravelTime { get; set; }

    }
}
