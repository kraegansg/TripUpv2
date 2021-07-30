using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripUp.Models
{
    public class TripListItem
    {
        public int TripId { get; set; }
        public string TripName { get; set; }
        //public int OwnerId { get; set; }

        //public int ItineraryId { get; set; }

        //public int PackId { get; set; }
        //public int ToDoListId { get; set; }
        public string Destination { get; set; }
        public string StartingLocation { get; set; }
        public string TravelBuddies { get; set; }



    }
}