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

    public class EFDirectoryCargoEtsng : IRepository<DirectoryCargoEtsng>
    {

        private EFDbContext db;

        public EFDirectoryCargoEtsng(EFDbContext db)
        {

            this.db = db;
        }

        public DatabaseFacade Database
        {
            get { return this.db.Database; }
        }

        public IQueryable<DirectoryCargoEtsng> Context
        {
            get { return db.DirectoryCargoEtsngs; }
        }

        public IEnumerable<DirectoryCargoEtsng> Get()
        {
            try
            {
                return db.Select<DirectoryCargoEtsng>();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        public DirectoryCargoEtsng Get(int Id)
        {
            try
            {
                return db.Select<DirectoryCargoEtsng>(Id);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        public void Add(DirectoryCargoEtsng item)
        {
            try
            {
                db.Insert<DirectoryCargoEtsng>(item);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public void Update(DirectoryCargoEtsng item)
        {
            try
            {
                db.Update<DirectoryCargoEtsng>(item);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public void AddOrUpdate(DirectoryCargoEtsng item)
        {
            try
            {
                DirectoryCargoEtsng dbEntry = db.DirectoryCargoEtsngs.Find(item.Id);
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
                DirectoryCargoEtsng item = db.Delete<DirectoryCargoEtsng>(id);
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

        public DirectoryCargoEtsng Refresh(DirectoryCargoEtsng item)
        {
            try
            {
                db.Entry(item).State = EntityState.Detached;
                return db.Select<DirectoryCargoEtsng>(item.Id);
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

        public void Add(IEnumerable<DirectoryCargoEtsng> items)
        {
            try
            {
                db.Inserts<DirectoryCargoEtsng>(items);
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
                db.Delete<DirectoryCargoEtsng>(items);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }


        public void Update(IEnumerable<DirectoryCargoEtsng> items)
        {
            try
            {
                db.Updates<DirectoryCargoEtsng>(items);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
