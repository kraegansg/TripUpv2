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
    public class TripController : Controller
    {

        public ActionResult Index()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new TripService(userId);
            var model = service.GetTrips();
            return View(model);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TripCreate model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var service = CreateTripService();

            if (service.CreateTrip(model))
            {
                TempData["SaveResult"] = "Trip created successfully.";
                return RedirectToAction("Index");
            };
            ModelState.AddModelError("", "Trip could not be created.");
            return View(model);

        }

        public ActionResult Details(int id)
        {
            var svc = CreateTripService();
            var model = svc.GetTripById(id);

            return View(model);
        }

        public ActionResult Edit(int id)
        {
            var service = CreateTripService();
            var detail = service.GetTripById(id);
            var model =
                new TripEdit
                {
                    TripId = detail.TripId,
                    TripName = detail.TripName,
                    Destination = detail.Destination,
                    StartingLocation = detail.StartingLocation,
                    TravelBuddies = detail.TravelBuddies,

                };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, TripEdit model)
        {
            if (!ModelState.IsValid) return View(model);

            if (model.TripId != id)
            {
                ModelState.AddModelError("", "Id Mismatch");
                return View(model);
            }

            var service = CreateTripService();

            if (service.UpdateTrip(model))
            {
                TempData["SaveResult"] = "Your Trip was updated.";
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Your Trip could not be updated.");
            return View();
        }

        public ActionResult Delete(int id)
        {
            var svc = CreateTripService();
            var model = svc.GetTripById(id);

            return View(model);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(int id)
        {
            var service = CreateTripService();
            service.DeleteTrip(id);
            TempData["SaveResult"] = "Your Trip was deleted.";
            return RedirectToAction("Index");
        }

        private TripService CreateTripService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new TripService(userId);
            return service;
        }
    }
}