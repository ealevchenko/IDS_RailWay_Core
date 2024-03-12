using EF_IDS.Concrete;
using EF_IDS.Entities;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Collections.Generic;
using System.Collections.Concurrent;

namespace WebAPI.Repositories.Directory
{
    public class DirectoryCargoGroupRepository : IRepository<DirectoryCargoGroup>
    {
        // кэшируем клиентов в потокобезопасном словаре для повышения производительности
        private static ConcurrentDictionary<int, DirectoryCargoGroup> customersCache;

        private EFDbContext db;
        public DirectoryCargoGroupRepository(EFDbContext db)
        {
            this.db = db;
            // предварительно загружаем клиентов из обычного словаря с ключом
            // CustomerID,
            // а затем конвертируем в потокобезопасный ConcurrentDictionary
            if (customersCache == null)
            {
                customersCache = new ConcurrentDictionary<int, DirectoryCargoGroup>(db.DirectoryCargoGroups.ToDictionary(c => c.Id));
            }
        }
        public async Task<DirectoryCargoGroup> CreateAsync(DirectoryCargoGroup c)
        {
            // приводим CustomerID к верхнему регистру
            //c.CustomerID = c.CustomerID.ToUpper();
            // добавляем в базу данных с помощью EF Core
            EntityEntry<DirectoryCargoGroup> added = await db.DirectoryCargoGroups.AddAsync(c);
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
        private DirectoryCargoGroup UpdateCache(int id, DirectoryCargoGroup c)
        {
            DirectoryCargoGroup old;
            if (customersCache.TryGetValue(id, out old))
            {
                if (customersCache.TryUpdate(id, c, old))
                {
                    return c;
                }
            }
            return null;
        }
        public async Task<IEnumerable<DirectoryCargoGroup>> RetrieveAllAsync()
        {
            // для повышения производительности извлекаем из кэша
            return await Task.Run<IEnumerable<DirectoryCargoGroup>>(
            () => customersCache.Values);
        }
        public async Task<DirectoryCargoGroup> RetrieveAsync(int id)
        {
            return await Task.Run(() =>
            {
                DirectoryCargoGroup c;
                customersCache.TryGetValue(id, out c);
                return c;
            });
        }
        public async Task<bool> DeleteAsync(int id)
        {
            return await Task.Run(() =>
            {
                // удаляем из базы данных
                DirectoryCargoGroup c = db.DirectoryCargoGroups.Find(id);
                db.DirectoryCargoGroups.Remove(c);
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
        public async Task<DirectoryCargoGroup> UpdateAsync(int id, DirectoryCargoGroup c)
        {
            return await Task.Run(() =>
            {
                db.DirectoryCargoGroups.Update(c);
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
