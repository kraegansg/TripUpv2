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
    public class TripService
    {
        private readonly Guid _userId;

        public TripService(Guid userId)
        {
            _userId = userId;
        }

        public bool CreateTrip(TripCreate model)
        {
            var entity =
                new Trip()
                {
                    OwnerId = _userId,

                    TripName = model.TripName,
                    Destination = model.Destination,
                    StartingLocation = model.StartingLocation,
                    TravelBuddies = model.TravelBuddies,
                };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Trips.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<TripListItem> GetTrips()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                    .Trips
                    .Where(e => e.OwnerId == _userId)
                    .Select(
                        e =>
                            new TripListItem
                            {
                                TripId = e.TripId,
                                TripName = e.TripName,
                                Destination = e.Destination,
                                StartingLocation = e.StartingLocation,
                                TravelBuddies = e.TravelBuddies
                            }
                                );
                return query.ToArray();
            }
        }

        public TripDetail GetTripById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .Trips
                    .Single(e => e.TripId == id && e.OwnerId == _userId);
                return
                    new TripDetail
                    {
                        TripName = entity.TripName,
                        Destination = entity.Destination,
                        StartingLocation = entity.StartingLocation,
                        TravelBuddies = entity.TravelBuddies
                    };
            }
        }

        public bool UpdateTrip(TripEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Trips
                        .Single(e => e.TripId == model.TripId && e.OwnerId == _userId);

                entity.TripName = model.TripName;
                entity.Destination = model.Destination;
                entity.StartingLocation = model.StartingLocation;
                entity.TravelBuddies = model.TravelBuddies;

                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeleteTrip(int tripId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Trips
                        .Single(e => e.TripId == tripId && e.OwnerId == _userId);
                ctx.Trips.Remove(entity);
                return ctx.SaveChanges() == 1;
            }
        }
    }
}

