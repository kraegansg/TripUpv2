using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripUp.Models
{
    public class ToDoListCreate
    {
        [Required]
        public string ToDoListName { get; set; }
        public string ToDoListMisc { get; set; }
        public string PetCareInstructions { get; set; }
        public string ChildCareInstructions { get; set; }
        public string HouseCareInstructions { get; set; }
    }
}
