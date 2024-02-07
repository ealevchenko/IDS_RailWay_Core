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

    public class EFOutgoingUzVagon : ILongRepository<OutgoingUzVagon>
    {

        private EFDbContext db;

        public EFOutgoingUzVagon(EFDbContext db)
        {

            this.db = db;
        }

        public DatabaseFacade Database
        {
            get { return this.db.Database; }
        }

        public IQueryable<OutgoingUzVagon> Context
        {
            get { return db.OutgoingUzVagons; }
        }

        public IEnumerable<OutgoingUzVagon> Get()
        {
            try
            {
                return db.Select<OutgoingUzVagon>();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        public OutgoingUzVagon Get(long Id)
        {
            try
            {
                return db.Select<OutgoingUzVagon>(Id);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        public void Add(OutgoingUzVagon item)
        {
            try
            {
                db.Insert<OutgoingUzVagon>(item);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public void Update(OutgoingUzVagon item)
        {
            try
            {
                db.Update<OutgoingUzVagon>(item);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public void AddOrUpdate(OutgoingUzVagon item)
        {
            try
            {
                OutgoingUzVagon dbEntry = db.OutgoingUzVagons.Find(item.Id);
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
                OutgoingUzVagon item = db.Delete<OutgoingUzVagon>(id);
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

        public OutgoingUzVagon Refresh(OutgoingUzVagon item)
        {
            try
            {
                db.Entry(item).State = EntityState.Detached;
                return db.Select<OutgoingUzVagon>(item.Id);
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

        public void Add(IEnumerable<OutgoingUzVagon> items)
        {
            try
            {
                db.Inserts<OutgoingUzVagon>(items);
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
                db.Delete<OutgoingUzVagon>(items);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }


        public void Update(IEnumerable<OutgoingUzVagon> items)
        {
            try
            {
                db.Updates<OutgoingUzVagon>(items);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
