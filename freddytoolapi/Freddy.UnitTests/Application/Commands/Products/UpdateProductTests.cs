using Freddy.Application.Commands.Products;
using Freddy.Application.Commands.Products.UpdateProduct;
using Freddy.Application.UnitTests.Utilities;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
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
        public void Execute_ShouldUpdateExistingProduct()
        {
            var productId = new Guid("e8e060d6-5cfc-4009-b150-c0870cc45464");
            var productInfo = new ProductInfo("change-code", "change-name", "change-size");

            _updateProduct.Handle(new UpdateProductCommand(productId, productInfo));
            _MockProducts.Verify(cmd => cmd.Update(It.Is(Helpers.EqualTo(productId, productInfo))), Times.Once);
        }
    }
}
