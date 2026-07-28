using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using BeyadAmi.Server.Application.Interfaces.Repositories;
using BeyadAmi.Server.Domain.Entities;
using BeyadAmi.Server.Infrastructure.Persistence;

namespace BeyadAmi.Server.Infrastructure.Repositories
{
    public class StoreRepository : IStoreRepository
    {
        private readonly ApplicationDbContext _db;

        public StoreRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<Store?> GetByIdAsync(int storeId, CancellationToken cancellationToken = default)
        {
            return await _db.Stores
                .Include(s => s.StoreProducts)
                    .ThenInclude(sp => sp.Product)
                .AsNoTracking()
                .FirstOrDefaultAsync(s => s.StoreId == storeId, cancellationToken);
        }

        public async Task<List<Store>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _db.Stores
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }

        public async Task<bool> ExistsAsync(int storeId, CancellationToken cancellationToken = default)
        {
            return await _db.Stores.AnyAsync(s => s.StoreId == storeId, cancellationToken);
        }

        public async Task<bool> HasProductsAsync(int storeId, CancellationToken cancellationToken = default)
        {
            return await _db.StoreProducts.AnyAsync(sp => sp.StoreId == storeId, cancellationToken);
        }

        public async Task AddAsync(Store store, CancellationToken cancellationToken = default)
        {
            await _db.Stores.AddAsync(store, cancellationToken);
            await _db.SaveChangesAsync(cancellationToken);
        }

        public void Update(Store store)
        {
            var tracked = _db.ChangeTracker.Entries<Store>().FirstOrDefault(e => e.Entity.StoreId == store.StoreId);
            if (tracked == null)
            {
                _db.Stores.Attach(store);
                _db.Entry(store).State = EntityState.Modified;
            }
            else
            {
                tracked.CurrentValues.SetValues(store);
            }
            _db.SaveChanges();
        }

        public void Delete(Store store)
        {
            var tracked = _db.ChangeTracker.Entries<Store>().FirstOrDefault(e => e.Entity.StoreId == store.StoreId);
            if (tracked == null)
            {
                _db.Stores.Attach(store);
            }

            _db.Stores.Remove(store);
            _db.SaveChanges();
        }
    }
}
