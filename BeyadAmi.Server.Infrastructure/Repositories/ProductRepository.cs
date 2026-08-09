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
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _db;

        public ProductRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<Product?> GetByIdAsync(int productId, CancellationToken cancellationToken = default)
        {
            return await _db.Products
                .Include(p => p.Purchases)
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.ProductId == productId, cancellationToken);
        }

        public async Task<List<Product>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _db.Products
                .Include(p => p.Purchases)
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }

        public async Task<List<Product>> SearchAsync(string? searchText, CancellationToken cancellationToken = default)
        {
            var query = _db.Products.Include(p => p.Purchases).AsNoTracking();

            if (!string.IsNullOrWhiteSpace(searchText))
                query = query.Where(p =>
                    p.ProductName!.Contains(searchText) ||
                    p.Model!.Contains(searchText) ||
                    p.Company!.Contains(searchText));

            return await query.ToListAsync(cancellationToken);
        }

        public async Task<bool> ExistsAsync(string productName, string model, string? company, CancellationToken cancellationToken = default)
        {
            return await _db.Products.AnyAsync(p =>
                p.ProductName == productName &&
                p.Model == model &&
                p.Company == company, cancellationToken);
        }

        public async Task<bool> HasPurchasesAsync(int productId, CancellationToken cancellationToken = default)
        {
            return await _db.Purchases.AnyAsync(p => p.ProductId == productId, cancellationToken);
        }

        public async Task AddAsync(Product product, CancellationToken cancellationToken = default)
        {
            await _db.Products.AddAsync(product, cancellationToken);
            await _db.SaveChangesAsync(cancellationToken);
        }

        public void Update(Product product)
        {
            var tracked = _db.ChangeTracker.Entries<Product>()
                .FirstOrDefault(e => e.Entity.ProductId == product.ProductId);

            if (tracked == null)
            {
                _db.Products.Attach(product);
                _db.Entry(product).State = EntityState.Modified;
            }
            else
            {
                tracked.CurrentValues.SetValues(product);
            }

            _db.SaveChanges();
        }

        public void Delete(Product product)
        {
            var tracked = _db.ChangeTracker.Entries<Product>()
                .FirstOrDefault(e => e.Entity.ProductId == product.ProductId);

            if (tracked == null)
                _db.Products.Attach(product);

            _db.Products.Remove(product);
            _db.SaveChanges();
        }
    }
}
