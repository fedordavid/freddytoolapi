using System;
using System.Threading.Tasks;
using Freddy.Application.Products.Queries;
using Freddy.Application.Products.Queries.GetAllProducts;
using Moq;
using Xunit;

namespace Freddy.Application.UnitTests.Application.Queries.Products
{
    public class GetAllProductsTests
    {
        private readonly GetAllProducts _getAllProducts;
        private readonly Mock<IProductViews> _productViewsMock;

        public GetAllProductsTests()
        {
            _productViewsMock = new Mock<IProductViews>();
            _getAllProducts = new GetAllProducts(_productViewsMock.Object);
        }

        [Fact]
        public async Task Execute_ShouldCallProductViews()
        {
            _productViewsMock.SetupGet(p => p.Products).Returns(new ViewCollectionMock<ProductView>());
            
            await _getAllProducts.Execute(new GetAllProductsQuery());
                
            _productViewsMock.VerifyGet(v => v.Products, Times.Once);
        }
        
        [Fact]
        public async Task Execute_ShouldReturnAllViews()
        {
            var products = new[]
            {
                new ProductView {Id = new Guid("5412D947-05DB-45AE-AA20-6ADA289FBA0E")},
                new ProductView {Id = new Guid("A017EFE7-D230-42BD-84F8-65574DD5AF6E")}
            };
            
            _productViewsMock.SetupGet(p => p.Products).Returns(new ViewCollectionMock<ProductView>(products));

            var result = await _getAllProducts.Execute(new GetAllProductsQuery());
            Assert.Equal(products, result);
        }
    }
}