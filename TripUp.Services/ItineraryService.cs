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
    public class ItineraryService
    {
        private readonly Guid _userId;

        public ItineraryService(Guid userId)
        {
            _userId = userId;
        }

        public bool CreateItinerary(ItineraryCreate model)
        {
            var entity =
                new Itinerary()
                {
                    OwnerId = _userId,
                    ItineraryName = model.ItineraryName,
                    PitStop = model.PitStop,
                    TravelDistance = model.TravelDistance,
                    TravelTime = model.TravelTime,
                };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Itineraries.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<ItineraryListItem> GetItineraries()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                    .Itineraries
                    .Where(e => e.OwnerId == _userId)
                    .Select(
                        e =>
                            new ItineraryListItem
                            {
                                ItineraryId = e.ItineraryId,
                                ItineraryName = e.ItineraryName,
                                PitStop = e.PitStop,
                                TravelDistance = e.TravelDistance,
                                TravelTime = e.TravelTime
                            }
                                );
                return query.ToArray();
            }
        }

        public ItineraryDetail GetItineraryById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .Itineraries
                    .Single(e => e.ItineraryId == id && e.OwnerId == _userId);
                return
                    new ItineraryDetail
                    {
                        ItineraryName = entity.ItineraryName,
                        PitStop = entity.PitStop,
                        TravelDistance = entity.TravelDistance,
                        TravelTime = entity.TravelTime
                    };
            }
        }

        public bool UpdateItinerary(ItineraryEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Itineraries
                        .Single(e => e.ItineraryId == model.ItineraryId && e.OwnerId == _userId);

                entity.ItineraryName = model.ItineraryName;
                entity.PitStop = model.PitStop;
                entity.TravelDistance = model.TravelDistance;
                entity.TravelTime = model.TravelTime;

                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeleteItinerary(int ItineraryId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Itineraries
                        .Single(e => e.ItineraryId == ItineraryId && e.OwnerId == _userId);
                ctx.Itineraries.Remove(entity);
                return ctx.SaveChanges() == 1;
            }
        }
    }
}


