using System;
using System.Threading.Tasks;
using Freddy.Application.Commands.Products;
using Freddy.Persistance.DbContexts;

namespace Freddy.Persistance.Products
{
    public class ProductCommandRepository : IProducts
    {
        private readonly DatabaseContext _context;

        public ProductCommandRepository(DatabaseContext context)
        {
            _context = context;
        }

        public Task Add(Product product)
        {
            var info = product.Info;

            _context.Products.Add(new ProductEntity
            {
                Code = info.Code,
                Id = product.Id,
                Name = info.Name,
                Size = info.Size
            });

            return _context.SaveChangesAsync();
        }

        public async Task Delete(Guid productId)
        {
            var productEntity = await _context.Products.FindAsync(productId);

            if (productEntity is null)
                return;
            
            _context.Products.Remove(productEntity);
            await _context.SaveChangesAsync();
        }

        public async Task<Product> Get(Guid productId)
        {
            var productEntity = await _context.Products.FindAsync(productId);

            if (productEntity is null)
                return null;
            
            var productInfo = new ProductInfo(productEntity.Code, productEntity.Name, productEntity.Size);
            return new Product(productEntity.Id, productInfo);
        }

        public async Task Update(Product product)
        {
            var info = product.Info;
            var productEntity = await _context.Products.FindAsync(product.Id);

            productEntity.Code = info.Code;
            productEntity.Name = info.Name;
            productEntity.Size = info.Size;

            await _context.SaveChangesAsync();
        }
    }
}