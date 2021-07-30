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
    public class ItineraryController : Controller
    {
        // GET: Trip
        public ActionResult Index()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new ItineraryService(userId);
            var model = service.GetItineraries();
            return View(model);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ItineraryCreate model)
        {
            if (ModelState.IsValid) return View(model);

            var service = CreateItineraryService();

            if (service.CreateItinerary(model))
            {
                TempData["SaveResult"] = "Trip created successfully.";
                return RedirectToAction("Index");
            };
            ModelState.AddModelError("", "Trip could not be created.");
            return View(model);

        }

        public ActionResult Details(int id)
        {
            var svc = CreateItineraryService();
            var model = svc.GetItineraryById(id);

            return View(model);
        }

        public ActionResult Edit(int id)
        {
            var service = CreateItineraryService();
            var detail = service.GetItineraryById(id);
            var model =
                new ItineraryEdit
                {
                    ItineraryId = detail.ItineraryId,
                    ItineraryName = detail.ItineraryName,
                    PitStop = detail.PitStop,
                    TravelDistance = detail.TravelDistance,
                    TravelTime = detail.TravelTime

                };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, ItineraryEdit model)
        {
            if (!ModelState.IsValid) return View(model);

            if (model.ItineraryId != id)
            {
                ModelState.AddModelError("", "Id Mismatch");
                return View(model);
            }

            var service = CreateItineraryService();

            if (service.UpdateItinerary(model))
            {
                TempData["SaveResult"] = "Your Itinerary was updated.";
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Your Itinerary could not be updated.");
            return View();
        }

        public ActionResult Delete(int id)
        {
            var svc = CreateItineraryService();
            var model = svc.GetItineraryById(id);

            return View(model);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(int id)
        {
            var service = CreateItineraryService();
            service.DeleteItinerary(id);
            TempData["SaveResult"] = "Your Trip was deleted.";
            return RedirectToAction("Index");
        }

        private ItineraryService CreateItineraryService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new ItineraryService(userId);
            return service;
        }
    }
}