using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripUp.Data
{
    public class Itinerary
    {
        [Key]
        public int ItineraryId { get; set; }
        public Guid OwnerId { get; set; }
        public string ItineraryName { get; set; }
        public string PitStop { get; set; }
        public int TravelDistance { get; set; }
        public DateTime TravelTime { get; set; }

    }
}

