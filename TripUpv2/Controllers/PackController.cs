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
    public class PackController : Controller
    {
        // GET: Trip
        public ActionResult Index()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new PackService(userId);
            var model = service.GetPacks();
            return View(model);
        }

        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PackCreate model)
        {
            if (!ModelState.IsValid) return View(model);

            var service = CreatePackService();

            if (service.CreatePack(model))
            {
                TempData["SaveResult"] = "Trip created successfully.";
                return RedirectToAction("Index");
            };
            ModelState.AddModelError("", "Trip could not be created.");
            return View(model);

        }

        public ActionResult Details(int id)
        {
            var svc = CreatePackService();
            var model = svc.GetPackById(id);

            return View(model);
        }

        public ActionResult Edit(int id)
        {
            var service = CreatePackService();
            var detail = service.GetPackById(id);
            var model =
                new PackEdit
                {
                    PackId = detail.PackId,
                    PackName = detail.PackName,
                    Clothes = detail.Clothes,
                    BathItems = detail.BathItems,
                    Essentials = detail.Essentials,
                    Other = detail.Other

                };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, PackEdit model)
        {
            if (!ModelState.IsValid) return View(model);

            if (model.PackId != id)
            {
                ModelState.AddModelError("", "Id Mismatch");
                return View(model);
            }

            var service = CreatePackService();

            if (service.UpdatePack(model))
            {
                TempData["SaveResult"] = "Your Pack was updated.";
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Your Pack could not be updated.");
            return View();
        }

        public ActionResult Delete(int id)
        {
            var svc = CreatePackService();
            var model = svc.GetPackById(id);

            return View(model);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(int id)
        {
            var service = CreatePackService();
            service.DeletePack(id);
            TempData["SaveResult"] = "Your Trip was deleted.";
            return RedirectToAction("Index");
        }

        private PackService CreatePackService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new PackService(userId);
            return service;
        }
    }
}