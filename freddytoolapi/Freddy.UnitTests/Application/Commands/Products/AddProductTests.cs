using System;
using System.Linq.Expressions;
using Freddy.Application.Commands.Products;
using Freddy.Application.UnitTests.Utilities;
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
            
            _productsMock.Verify(products => products.Add(It.Is(A.Product.With(productId, productInfo))), Times.Once);
        }
    }
}