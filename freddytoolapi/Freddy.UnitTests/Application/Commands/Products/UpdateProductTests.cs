using Freddy.Application.Commands.Products;
using Freddy.Application.Commands.Products.UpdateProduct;
using Freddy.Application.UnitTests.Utilities;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Freddy.Application.UnitTests.Application.Commands.Products
{
    public class UpdateProductTests
    {
        private readonly Mock<IProducts> _MockProducts;
        private readonly UpdateProduct _updateProduct;

        public UpdateProductTests()
        {
            _MockProducts = new Mock<IProducts>();
            _updateProduct = new UpdateProduct(_MockProducts.Object);
        }

        [Fact]
        public async Task Execute_ShouldUpdateExistingProduct()
        {
            var productId = new Guid("e8e060d6-5cfc-4009-b150-c0870cc45464");
            var productInfo = new ProductInfo("change-code", "change-name", "change-size");

            _MockProducts.Setup(s => s.Get(productId)).ReturnsAsync(new Product(productId, new ProductInfo()));

            await _updateProduct.Handle(new UpdateProductCommand(productId, productInfo));

            _MockProducts.Verify(products => products.Update(It.Is(Helpers.EqualTo(productId, productInfo))), Times.Once);
        }

        [Fact]
        public async Task Execute_ShouldGetProduct()
        {
            var productId = new Guid("e8e060d6-5cfc-4009-b150-c0870cc45464");
            var productInfo = new ProductInfo("change-code", "change-name", "change-size");

            _MockProducts.Setup(s => s.Get(productId)).ReturnsAsync(new Product(productId, new ProductInfo()));

            await _updateProduct.Handle(new UpdateProductCommand(productId, productInfo));
            _MockProducts.Verify(products => products.Get(productId), Times.Once);
        }
    }
}
