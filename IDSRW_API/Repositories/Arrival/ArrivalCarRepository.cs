using EF_IDS.Concrete;
using EF_IDS.Entities;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Collections.Generic;
using System.Collections.Concurrent;

namespace WebAPI.Repositories.Arrival
{
    public class ArrivalCarRepository : ILongRepository<ArrivalCar>
    {
        // кэшируем клиентов в потокобезопасном словаре для повышения производительности
        private static ConcurrentDictionary<long, ArrivalCar> customersCache;

        private EFDbContext db;
        public ArrivalCarRepository(EFDbContext db)
        {
            this.db = db;
            // предварительно загружаем клиентов из обычного словаря с ключом
            // CustomerID,
            // а затем конвертируем в потокобезопасный ConcurrentDictionary
            if (customersCache == null)
            {
                customersCache = new ConcurrentDictionary<long, ArrivalCar>(db.ArrivalCars.ToDictionary(c => c.Id));
            }
        }
        public async Task<ArrivalCar> CreateAsync(ArrivalCar c)
        {
            // приводим CustomerID к верхнему регистру
            //c.CustomerID = c.CustomerID.ToUpper();
            // добавляем в базу данных с помощью EF Core
            EntityEntry<ArrivalCar> added = await db.ArrivalCars.AddAsync(c);
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
        private ArrivalCar UpdateCache(long id, ArrivalCar c)
        {
            ArrivalCar old;
            if (customersCache.TryGetValue(id, out old))
            {
                if (customersCache.TryUpdate(id, c, old))
                {
                    return c;
                }
            }
            return null;
        }
        public async Task<IEnumerable<ArrivalCar>> RetrieveAllAsync()
        {
            // для повышения производительности извлекаем из кэша
            return await Task.Run<IEnumerable<ArrivalCar>>(
            () => customersCache.Values);
        }
        public async Task<ArrivalCar> RetrieveAsync(long id)
        {
            return await Task.Run(() =>
            {
                // для повышения производительности извлекаем из кэша
                //id = id.ToUpper();
                ArrivalCar c;
                customersCache.TryGetValue(id, out c);
                return c;
            });
        }
        public async Task<bool> DeleteAsync(long id)
        {
            return await Task.Run(() =>
            {
                // удаляем из базы данных
                ArrivalCar c = db.ArrivalCars.Find(id);
                db.ArrivalCars.Remove(c);
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
        public async Task<ArrivalCar> UpdateAsync(long id, ArrivalCar c)
        {
            return await Task.Run(() =>
            {
                // нормализуем идентификатор клиента
                //id = id.ToUpper();
                //c.CustomerID = c.CustomerID.ToUpper();
                // обновляем в базе данных
                db.ArrivalCars.Update(c);
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
