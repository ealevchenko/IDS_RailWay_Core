using EF_IDS.Abstract;
using EF_IDS.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_IDS.Concrete.Outgoing
{

    public class EFOutgoingCar : ILongRepository<OutgoingCar>
    {

        private EFDbContext db;

        public EFOutgoingCar(EFDbContext db)
        {

            this.db = db;
        }

        public DatabaseFacade Database
        {
            get { return this.db.Database; }
        }

        public IQueryable<OutgoingCar> Context
        {
            get { return db.OutgoingCars; }
        }

        public IEnumerable<OutgoingCar> Get()
        {
            try
            {
                return db.Select<OutgoingCar>();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        public OutgoingCar Get(long Id)
        {
            try
            {
                return db.Select<OutgoingCar>(Id);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        public void Add(OutgoingCar item)
        {
            try
            {
                db.Insert<OutgoingCar>(item);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public void Update(OutgoingCar item)
        {
            try
            {
                db.Update<OutgoingCar>(item);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public void AddOrUpdate(OutgoingCar item)
        {
            try
            {
                OutgoingCar dbEntry = db.OutgoingCars.Find(item.Id);
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
                OutgoingCar item = db.Delete<OutgoingCar>(id);
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

        public OutgoingCar Refresh(OutgoingCar item)
        {
            try
            {
                db.Entry(item).State = EntityState.Detached;
                return db.Select<OutgoingCar>(item.Id);
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

        public void Add(IEnumerable<OutgoingCar> items)
        {
            try
            {
                db.Inserts<OutgoingCar>(items);
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
                db.Delete<OutgoingCar>(items);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }


        public void Update(IEnumerable<OutgoingCar> items)
        {
            try
            {
                db.Updates<OutgoingCar>(items);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
