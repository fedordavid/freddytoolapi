using Freddy.Application.Commands.Products;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Freddy.Application.UnitTests.Application.Commands.Products
{
    public class DeleteProductTests
    {
        private readonly Mock<IProducts> _productsMock;
        private readonly DeleteProduct _deleteProduct;

        public DeleteProductTests()
        {
            _productsMock = new Mock<IProducts>();
            _deleteProduct = new DeleteProduct(_productsMock.Object);
        }

        [Fact]
        public void Execute_ShouldDeleteProduct()
        {
            var productId = new Guid("3b50451a-05d1-4e96-a2d7-7ff1e2cca09f");

            _deleteProduct.Handle(new DeleteProductCommand(productId));

            _productsMock.Verify(cmd => cmd.Delete(productId), Times.Once);
        } 
    }
}
