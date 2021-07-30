using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripUp.Data;
using TripUp.Models;
using TripUpv2.Models;

namespace TripUp.Services
{
    public class ToDoListService
    {
        private readonly Guid _userId;

        public ToDoListService(Guid userId)
        {
            _userId = userId;
        }

        public bool CreateToDoList(ToDoListCreate model)
        {
            var entity =
                new ToDoList()
                {
                    OwnerId = _userId,
                    ToDoListName = model.ToDoListName,
                    ToDoListMisc = model.ToDoListMisc,
                    PetCareInstructions = model.PetCareInstructions,
                    ChildCareInstructions = model.ChildCareInstructions,
                    HouseCareInstructions = model.HouseCareInstructions
                };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.ToDoLists.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<ToDoListItem> GetToDoLists()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                    .ToDoLists
                    .Where(e => e.OwnerId == _userId)
                    .Select(
                        e =>
                            new ToDoListItem
                            {
                                ToDoListId = e.ToDoListId,
                                ToDoListName = e.ToDoListName,
                                ToDoListMisc = e.ToDoListMisc,
                                PetCareInstructions = e.PetCareInstructions,
                                ChildCareInstructions = e.ChildCareInstructions,
                                HouseCareInstructions = e.HouseCareInstructions
                            }
                                );
                return query.ToArray();
            }
        }

        public ToDoListDetail GetToDoListById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .ToDoLists
                    .Single(e => e.ToDoListId == id && e.OwnerId == _userId);
                return
                    new ToDoListDetail
                    {
                        ToDoListName = entity.ToDoListName,
                        ToDoListMisc = entity.ToDoListMisc,
                        PetCareInstructions = entity.PetCareInstructions,
                        ChildCareInstructions = entity.ChildCareInstructions,
                        HouseCareInstructions = entity.HouseCareInstructions
                    };
            }
        }

        public bool UpdateToDoList(ToDoListEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .ToDoLists
                        .Single(e => e.ToDoListId == model.ToDoListId && e.OwnerId == _userId);

                entity.ToDoListName = model.ToDoListName;
                entity.ToDoListMisc = model.ToDoListMisc;
                entity.PetCareInstructions = model.PetCareInstructions;
                entity.ChildCareInstructions = model.ChildCareInstructions;
                entity.HouseCareInstructions = model.HouseCareInstructions;

                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeleteToDoList(int toDoListId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .ToDoLists
                        .Single(e => e.ToDoListId == toDoListId && e.OwnerId == _userId);
                ctx.ToDoLists.Remove(entity);
                return ctx.SaveChanges() == 1;
            }
        }
    }
}
