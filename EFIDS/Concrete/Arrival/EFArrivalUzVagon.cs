using EF_IDS.Abstract;
using EF_IDS.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_IDS.Concrete.Arrival
{

    public class EFArrivalUzVagon : ILongRepository<ArrivalUzVagon>
    {

        private EFDbContext db;

        public EFArrivalUzVagon(EFDbContext db)
        {

            this.db = db;
        }

        public DatabaseFacade Database
        {
            get { return this.db.Database; }
        }

        public IQueryable<ArrivalUzVagon> Context
        {
            get { return db.ArrivalUzVagons; }
        }

        public IEnumerable<ArrivalUzVagon> Get()
        {
            try
            {
                return db.Select<ArrivalUzVagon>();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        public ArrivalUzVagon Get(long Id)
        {
            try
            {
                return db.Select<ArrivalUzVagon>(Id);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        public void Add(ArrivalUzVagon item)
        {
            try
            {
                db.Insert<ArrivalUzVagon>(item);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public void Update(ArrivalUzVagon item)
        {
            try
            {
                db.Update<ArrivalUzVagon>(item);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public void AddOrUpdate(ArrivalUzVagon item)
        {
            try
            {
                ArrivalUzVagon dbEntry = db.ArrivalUzVagons.Find(item.Id);
                if (dbEntry == null)
                {
                    Add(item);
                }
                else
                {
                    Update(item);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

        }

        public void Delete(long id)
        {
            try
            {
                ArrivalUzVagon item = db.Delete<ArrivalUzVagon>(id);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public int Save()
        {
            try
            {
                return db.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return -1;
            }
        }

        public ArrivalUzVagon Refresh(ArrivalUzVagon item)
        {
            try
            {
                db.Entry(item).State = EntityState.Detached;
                return db.Select<ArrivalUzVagon>(item.Id);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Add(IEnumerable<ArrivalUzVagon> items)
        {
            try
            {
                db.Inserts<ArrivalUzVagon>(items);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public void Delete(IEnumerable<long> items)
        {
            try
            {
                db.Delete<ArrivalUzVagon>(items);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }


        public void Update(IEnumerable<ArrivalUzVagon> items)
        {
            try
            {
                db.Updates<ArrivalUzVagon>(items);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
