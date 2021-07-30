using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripUp.Data
{
    public class ToDoList
    {
        [Key]
        public int ToDoListId { get; set; }
        public Guid OwnerId { get; set; }

        //[ForeignKey(nameof(Trip))]
        //public int TripId { get; set; }
        //public virtual Trip Trip { get; set; }
        public string ToDoListName { get; set; }
        public string ToDoListMisc { get; set; }
        public string PetCareInstructions { get; set; }
        public string ChildCareInstructions { get; set; }
        public string HouseCareInstructions { get; set; }
    }
}