using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TripUp.Models;
using TripUp.Services;

namespace TripUpv2.Controllers
{

    [Authorize]
    public class ToDoListController : Controller
    {
        // GET: ToDoList
        public ActionResult Index()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new ToDoListService(userId);
            var model = service.GetToDoLists();
            return View(model);
        }
        public ActionResult Create()
        {
            return View();
        }

        //Add code here VVVV
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ToDoListCreate model)
        {
            if (ModelState.IsValid) return View(model);

            var service = CreateToDoListService();

            if (service.CreateToDoList(model))
            {
                TempData["SaveResult"] = "To-Do List created successfully.";
                return RedirectToAction("Index");
            };
            ModelState.AddModelError("", "To-Do List could not be created.");
            return View(model);

        }

        public ActionResult Details(int id)
        {
            var svc = CreateToDoListService();
            var model = svc.GetToDoListById(id);

            return View(model);
        }

        public ActionResult Edit(int id)
        {
            var service = CreateToDoListService();
            var detail = service.GetToDoListById(id);
            var model =
                new ToDoListEdit
                {
                    ToDoListId = detail.ToDoListId,
                    ToDoListName = detail.ToDoListName,
                    ToDoListMisc = detail.ToDoListMisc,
                    PetCareInstructions = detail.PetCareInstructions,
                    ChildCareInstructions = detail.ChildCareInstructions,
                    HouseCareInstructions = detail.HouseCareInstructions

                };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, ToDoListEdit model)
        {
            if (!ModelState.IsValid) return View(model);

            if (model.ToDoListId != id)
            {
                ModelState.AddModelError("", "Id Mismatch");
                return View(model);
            }

            var service = CreateToDoListService();

            if (service.UpdateToDoList(model))
            {
                TempData["SaveResult"] = "Your To-Do List was updated.";
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Your To-Do List could not be updated.");
            return View();
        }

        public ActionResult Delete(int id)
        {
            var svc = CreateToDoListService();
            var model = svc.GetToDoListById(id);

            return View(model);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(int id)
        {
            var service = CreateToDoListService();
            service.DeleteToDoList(id);
            TempData["SaveResult"] = "Your Trip was deleted.";
            return RedirectToAction("Index");
        }

        private ToDoListService CreateToDoListService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new ToDoListService(userId);
            return service;
        }
    }
}
