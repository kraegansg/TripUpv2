using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripUp.Data
{
    public class Trip
    {
        [Key]
        public int TripId { get; set; }

        [Required]
        [Display(Name = "Name Your Trip")]
        public string TripName { get; set; }

        public Guid OwnerId { get; set; }

        //[ForeignKey(nameof(Itinerary))]
        //public int ItineraryId { get; set; }
        //public virtual Itinerary Itinerary { get; set; }

        //[ForeignKey(nameof(Pack))]
        //public int PackId { get; set; }
        //public virtual Pack Pack { get; set; }

        //[ForeignKey(nameof(ToDoList))]
        //public int ToDoListId { get; set; }
        //public virtual ToDoList ToDoList { get; set; }

        [Required]
        public string Destination { get; set; }
        [Required]
        public string StartingLocation { get; set; }
        public string TravelBuddies { get; set; }
    }
}