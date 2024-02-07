using EF_IDS.Abstract;
using EF_IDS.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_IDS.Concrete.Directory
{

    public class EFGivcRequest : IRepository<GivcRequest>
    {

        private EFDbContext db;

        public EFGivcRequest(EFDbContext db)
        {

            this.db = db;
        }

        public DatabaseFacade Database
        {
            get { return this.db.Database; }
        }

        public IQueryable<GivcRequest> Context
        {
            get { return db.GivcRequests; }
        }

        public IEnumerable<GivcRequest> Get()
        {
            try
            {
                return db.Select<GivcRequest>();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        public GivcRequest Get(int Id)
        {
            try
            {
                return db.Select<GivcRequest>(Id);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        public void Add(GivcRequest item)
        {
            try
            {
                db.Insert<GivcRequest>(item);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public void Update(GivcRequest item)
        {
            try
            {
                db.Update<GivcRequest>(item);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public void AddOrUpdate(GivcRequest item)
        {
            try
            {
                GivcRequest dbEntry = db.GivcRequests.Find(item.Id);
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

        public void Delete(int id)
        {
            try
            {
                GivcRequest item = db.Delete<GivcRequest>(id);
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

        public GivcRequest Refresh(GivcRequest item)
        {
            try
            {
                db.Entry(item).State = EntityState.Detached;
                return db.Select<GivcRequest>(item.Id);
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

        public void Add(IEnumerable<GivcRequest> items)
        {
            try
            {
                db.Inserts<GivcRequest>(items);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public void Delete(IEnumerable<int> items)
        {
            try
            {
                db.Delete<GivcRequest>(items);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
        public void Update(IEnumerable<GivcRequest> items)
        {
            try
            {
                db.Updates<GivcRequest>(items);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
