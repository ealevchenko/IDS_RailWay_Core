using EF_IDS.Concrete;
using EF_IDS.Entities;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Collections.Generic;
using System.Collections.Concurrent;

namespace WebAPI.Repositories.Directory
{
    public class DirectoryCargoEtsngRepository : IRepository<DirectoryCargoEtsng>
    {
        // кэшируем клиентов в потокобезопасном словаре для повышения производительности
        private static ConcurrentDictionary<int, DirectoryCargoEtsng> customersCache;

        private EFDbContext db;
        public DirectoryCargoEtsngRepository(EFDbContext db)
        {
            this.db = db;
            // предварительно загружаем клиентов из обычного словаря с ключом
            // CustomerID,
            // а затем конвертируем в потокобезопасный ConcurrentDictionary
            if (customersCache == null)
            {
                customersCache = new ConcurrentDictionary<int, DirectoryCargoEtsng>(db.DirectoryCargoEtsngs.ToDictionary(c => c.Id));
            }
        }
        public async Task<DirectoryCargoEtsng> CreateAsync(DirectoryCargoEtsng c)
        {
            // приводим CustomerID к верхнему регистру
            //c.CustomerID = c.CustomerID.ToUpper();
            // добавляем в базу данных с помощью EF Core
            EntityEntry<DirectoryCargoEtsng> added = await db.DirectoryCargoEtsngs.AddAsync(c);
            int affected = await db.SaveChangesAsync();
            if (affected == 1)
            {
                // если клиент новый, то добавляем в кэш, иначе вызываем
                // метод UpdateCache
                return customersCache.AddOrUpdate(c.Id, c, UpdateCache);
            }
            else
            {
                return null;
            }
        }
        private DirectoryCargoEtsng UpdateCache(int id, DirectoryCargoEtsng c)
        {
            DirectoryCargoEtsng old;
            if (customersCache.TryGetValue(id, out old))
            {
                if (customersCache.TryUpdate(id, c, old))
                {
                    return c;
                }
            }
            return null;
        }
        public async Task<IEnumerable<DirectoryCargoEtsng>> RetrieveAllAsync()
        {
            // для повышения производительности извлекаем из кэша
            return await Task.Run<IEnumerable<DirectoryCargoEtsng>>(
            () => customersCache.Values);
        }
        public async Task<DirectoryCargoEtsng> RetrieveAsync(int id)
        {
            return await Task.Run(() =>
            {
                DirectoryCargoEtsng c;
                customersCache.TryGetValue(id, out c);
                return c;
            });
        }
        public async Task<bool> DeleteAsync(int id)
        {
            return await Task.Run(() =>
            {
                // удаляем из базы данных
                DirectoryCargoEtsng c = db.DirectoryCargoEtsngs.Find(id);
                db.DirectoryCargoEtsngs.Remove(c);
                int affected = db.SaveChanges();
                if (affected == 1)
                {
                    // удаляем из кэша
                    return Task.Run(() => customersCache.TryRemove(id, out c));
                }
                else
                {
                    return null;
                }
            });
        }
        public async Task<DirectoryCargoEtsng> UpdateAsync(int id, DirectoryCargoEtsng c)
        {
            return await Task.Run(() =>
            {
                db.DirectoryCargoEtsngs.Update(c);
                int affected = db.SaveChanges();
                if (affected == 1)
                {
                    // обновляем в кэше
                    return Task.Run(() => UpdateCache(id, c));
                }
                return null;
            });
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
    }
}
