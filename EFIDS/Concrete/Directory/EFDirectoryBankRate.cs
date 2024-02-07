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

    public class EFDirectoryBankRate : IRepository<DirectoryBankRate>
    {

        private EFDbContext db;

        public EFDirectoryBankRate(EFDbContext db)
        {

            this.db = db;
        }

        public DatabaseFacade Database
        {
            get { return this.db.Database; }
        }

        public IQueryable<DirectoryBankRate> Context
        {
            get { return db.DirectoryBankRates; }
        }

        public IEnumerable<DirectoryBankRate> Get()
        {
            try
            {
                return db.Select<DirectoryBankRate>();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        public DirectoryBankRate Get(int Id)
        {
            try
            {
                return db.Select<DirectoryBankRate>(Id);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        public void Add(DirectoryBankRate item)
        {
            try
            {
                db.Insert<DirectoryBankRate>(item);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public void Update(DirectoryBankRate item)
        {
            try
            {
                db.Update<DirectoryBankRate>(item);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public void AddOrUpdate(DirectoryBankRate item)
        {
            try
            {
                DirectoryBankRate dbEntry = db.DirectoryBankRates.Find(item.Id);
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
                DirectoryBankRate item = db.Delete<DirectoryBankRate>(id);
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

        public DirectoryBankRate Refresh(DirectoryBankRate item)
        {
            try
            {
                db.Entry(item).State = EntityState.Detached;
                return db.Select<DirectoryBankRate>(item.Id);
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

        public void Add(IEnumerable<DirectoryBankRate> items)
        {
            try
            {
                db.Inserts<DirectoryBankRate>(items);
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
                db.Delete<DirectoryBankRate>(items);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }


        public void Update(IEnumerable<DirectoryBankRate> items)
        {
            try
            {
                db.Updates<DirectoryBankRate>(items);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
