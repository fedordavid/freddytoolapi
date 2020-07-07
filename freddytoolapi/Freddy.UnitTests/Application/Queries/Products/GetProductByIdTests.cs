using System;
using System.Threading.Tasks;
using Freddy.Application.Queries.Products;
using Moq;
using Xunit;

namespace Freddy.Application.UnitTests.Application.Queries.Products
{
    public class GetProductByIdTests
    {
        private readonly GetProductById _getProductById;
        private readonly Mock<IProductViews> _productViewsMock;

        public GetProductByIdTests()
        {
            _productViewsMock = new Mock<IProductViews>();
            _getProductById = new GetProductById(_productViewsMock.Object);
        }

        [Fact]
        public async Task Execute_ShouldCallProductViews()
        {
            _productViewsMock.SetupGet(p => p.Products).Returns(new ViewCollectionMock<ProductView>());
            
            await _getProductById.Execute(new GetProductByIdQuery(Guid.Empty));
                
            _productViewsMock.VerifyGet(v => v.Products, Times.Once);
        }
        
        [Fact]
        public async Task Execute_ShouldReturnViewWithCorrectId()
        {
            var product =  new ProductView {Id = new Guid("5412D947-05DB-45AE-AA20-6ADA289FBA0E")};
            
            var products = new[]
            {
                product,
                new ProductView {Id = new Guid("A017EFE7-D230-42BD-84F8-65574DD5AF6E")}
            };
            
            _productViewsMock.SetupGet(p => p.Products).Returns(new ViewCollectionMock<ProductView>(products));

            var result = await _getProductById.Execute(new GetProductByIdQuery(product.Id));
            Assert.Equal(product, result);
        }
    }
}