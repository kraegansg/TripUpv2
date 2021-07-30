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
    public class PackService
    {
        private readonly Guid _userId;

        public PackService(Guid userId)
        {
            _userId = userId;
        }

        public bool CreatePack(PackCreate model)
        {
            var entity =
                new Pack()
                {
                    OwnerId = _userId,
                    PackName = model.PackName,
                    Clothes = model.Clothes,
                    BathItems = model.BathItems,
                    Essentials = model.Essentials,
                    Other = model.Other
                };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Packs.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<PackListItem> GetPacks()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                    .Packs
                    .Where(e => e.OwnerId == _userId)
                    .Select(
                        e =>
                            new PackListItem
                            {
                                PackId = e.PackId,
                                PackName = e.PackName,
                                Clothes = e.Clothes,
                                BathItems = e.BathItems,
                                Essentials = e.Essentials,
                                Other = e.Other
                            }
                                );
                return query.ToArray();
            }
        }

        public PackDetail GetPackById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .Packs
                    .Single(e => e.PackId == id && e.OwnerId == _userId);
                return
                    new PackDetail
                    {
                        PackName = entity.PackName,
                        Clothes = entity.Clothes,
                        BathItems = entity.BathItems,
                        Essentials = entity.Essentials,
                        Other = entity.Other
                    };
            }
        }

        public bool UpdatePack(PackEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Packs
                        .Single(e => e.PackId == model.PackId && e.OwnerId == _userId);

                entity.PackName = model.PackName;
                entity.Clothes = model.Clothes;
                entity.BathItems = model.BathItems;
                entity.Essentials = model.Essentials;
                entity.Other = model.Other;

                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeletePack(int packId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Packs
                        .Single(e => e.PackId == packId && e.OwnerId == _userId);
                ctx.Packs.Remove(entity);
                return ctx.SaveChanges() == 1;
            }
        }
    }
}
