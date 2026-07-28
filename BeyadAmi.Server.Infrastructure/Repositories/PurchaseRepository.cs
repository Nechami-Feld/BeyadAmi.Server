using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BeyadAmi.Server.Application.Interfaces.Repositories;
using BeyadAmi.Server.Domain.Entities;
using BeyadAmi.Server.Infrastructure.Persistence;

namespace BeyadAmi.Server.Infrastructure.Repositories
{
    public class PurchaseRepository : IPurchaseRepository
    {
        private readonly ApplicationDbContext _db;

        public PurchaseRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<Purchase?> GetByIdAsync(int purchaseId, CancellationToken cancellationToken = default)
        {
            return await _db.Purchases
                .Include(p => p.Store)
                .Include(p => p.Product)
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.PurchaseId == purchaseId, cancellationToken);
        }

        public async Task<List<Purchase>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _db.Purchases
                .Include(p => p.Store)
                .Include(p => p.Product)
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }

        public async Task<List<Purchase>> GetByStoreAsync(int storeId, CancellationToken cancellationToken = default)
        {
            return await _db.Purchases
                .Include(p => p.Store)
                .Include(p => p.Product)
                .AsNoTracking()
                .Where(p => p.StoreId == storeId)
                .ToListAsync(cancellationToken);
        }

        public async Task<List<Purchase>> GetByProductAsync(int productId, CancellationToken cancellationToken = default)
        {
            return await _db.Purchases
                .Include(p => p.Store)
                .Include(p => p.Product)
                .AsNoTracking()
                .Where(p => p.ProductId == productId)
                .ToListAsync(cancellationToken);
        }

        public async Task AddAsync(Purchase purchase, CancellationToken cancellationToken = default)
        {
            await _db.Purchases.AddAsync(purchase, cancellationToken);
            await _db.SaveChangesAsync(cancellationToken);
        }

        public void Update(Purchase purchase)
        {
            var tracked = _db.ChangeTracker.Entries<Purchase>()
                .FirstOrDefault(e => e.Entity.PurchaseId == purchase.PurchaseId);

            if (tracked == null)
            {
                _db.Purchases.Attach(purchase);
                _db.Entry(purchase).State = EntityState.Modified;
            }
            else
            {
                tracked.CurrentValues.SetValues(purchase);
            }

            _db.SaveChanges();
        }

        public void Delete(Purchase purchase)
        {
            var tracked = _db.ChangeTracker.Entries<Purchase>()
                .FirstOrDefault(e => e.Entity.PurchaseId == purchase.PurchaseId);

            if (tracked == null)
                _db.Purchases.Attach(purchase);

            _db.Purchases.Remove(purchase);
            _db.SaveChanges();
        }
    }
}
