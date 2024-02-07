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

    public class EFArrivalSostav : ILongRepository<ArrivalSostav>
    {

        private EFDbContext db;

        public EFArrivalSostav(EFDbContext db)
        {

            this.db = db;
        }

        public DatabaseFacade Database
        {
            get { return this.db.Database; }
        }

        public IQueryable<ArrivalSostav> Context
        {
            get { return db.ArrivalSostavs; }
        }

        public IEnumerable<ArrivalSostav> Get()
        {
            try
            {
                return db.Select<ArrivalSostav>();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        public ArrivalSostav Get(long Id)
        {
            try
            {
                return db.Select<ArrivalSostav>(Id);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        public void Add(ArrivalSostav item)
        {
            try
            {
                db.Insert<ArrivalSostav>(item);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public void Update(ArrivalSostav item)
        {
            try
            {
                db.Update<ArrivalSostav>(item);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public void AddOrUpdate(ArrivalSostav item)
        {
            try
            {
                ArrivalSostav dbEntry = db.ArrivalSostavs.Find(item.Id);
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
                ArrivalSostav item = db.Delete<ArrivalSostav>(id);
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

        public ArrivalSostav Refresh(ArrivalSostav item)
        {
            try
            {
                db.Entry(item).State = EntityState.Detached;
                return db.Select<ArrivalSostav>(item.Id);
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

        public void Add(IEnumerable<ArrivalSostav> items)
        {
            try
            {
                db.Inserts<ArrivalSostav>(items);
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
                db.Delete<ArrivalSostav>(items);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }


        public void Update(IEnumerable<ArrivalSostav> items)
        {
            try
            {
                db.Updates<ArrivalSostav>(items);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
