using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripUp.Models
{
    public class TripCreate
    {
        [Required]
        public string TripName { get; set; }
        public string Destination { get; set; }
        public string StartingLocation { get; set; }
        public string TravelBuddies { get; set; }
    }
}
