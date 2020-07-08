using System;
using System.Linq.Expressions;
using Freddy.Application.Commands.Products;
using Moq;
using Xunit;

namespace Freddy.Application.UnitTests.Application.Commands.Products
{
    public class AddProductTests
    {
        private readonly AddProduct _addProduct;
        private readonly Mock<IProducts> _productsMock;

        public AddProductTests()
        {
            _productsMock = new Mock<IProducts>();
            _addProduct = new AddProduct(_productsMock.Object);
        }

        [Fact]
        public void Execute_ShouldAddProductsBasedOnCommandParameters()
        {
            var productId = new Guid("5412D947-05DB-45AE-AA20-6ADA289FBA0E");
            var productInfo = new ProductInfo("code", "name", "size");

            _addProduct.Handle(new AddProductCommand(productId, productInfo));
            
            _productsMock.Verify(cmd => cmd.Add(It.Is(EqualTo(productId, productInfo))), Times.Once);
        }

        private static Expression<Func<Product, bool>> EqualTo(Guid id, ProductInfo info)
        {
            return p => p.Id == id
                && p.Info.Code == info.Code
                && p.Info.Name == info.Name
                && p.Info.Size == info.Size;
        }
    }
}