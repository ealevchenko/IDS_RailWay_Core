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

    public class EFDirectoryWagonsRent : IRepository<DirectoryWagonsRent>
    {

        private EFDbContext db;

        public EFDirectoryWagonsRent(EFDbContext db)
        {

            this.db = db;
        }

        public DatabaseFacade Database
        {
            get { return this.db.Database; }
        }

        public IQueryable<DirectoryWagonsRent> Context
        {
            get { return db.DirectoryWagonsRents; }
        }

        public IEnumerable<DirectoryWagonsRent> Get()
        {
            try
            {
                return db.Select<DirectoryWagonsRent>();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        public DirectoryWagonsRent Get(int Id)
        {
            try
            {
                return db.Select<DirectoryWagonsRent>(Id);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        public void Add(DirectoryWagonsRent item)
        {
            try
            {
                db.Insert<DirectoryWagonsRent>(item);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public void Update(DirectoryWagonsRent item)
        {
            try
            {
                db.Update<DirectoryWagonsRent>(item);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public void AddOrUpdate(DirectoryWagonsRent item)
        {
            try
            {
                DirectoryWagonsRent dbEntry = db.DirectoryWagonsRents.Find(item.Id);
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
                DirectoryWagonsRent item = db.Delete<DirectoryWagonsRent>(id);
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

        public DirectoryWagonsRent Refresh(DirectoryWagonsRent item)
        {
            try
            {
                db.Entry(item).State = EntityState.Detached;
                return db.Select<DirectoryWagonsRent>(item.Id);
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

        public void Add(IEnumerable<DirectoryWagonsRent> items)
        {
            try
            {
                db.Inserts<DirectoryWagonsRent>(items);
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
                db.Delete<DirectoryWagonsRent>(items);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }


        public void Update(IEnumerable<DirectoryWagonsRent> items)
        {
            try
            {
                db.Updates<DirectoryWagonsRent>(items);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
