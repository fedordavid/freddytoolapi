using System;
using Freddy.Application.Queries.Products;

namespace Freddy.Application.UnitTests.API.Controllers
{
    public class ProductViewBuilder
    {
        private Guid _id = new Guid("F9EDC8C1-00BF-454A-BF5C-58CF0E335653");
        private string _code = "code";
        private string _size = "size";
        private string _name = "name";

        public ProductViewBuilder(ProductViewBuilder product)
        {
            _code = product._code;
            _id = product._id;
            _name = product._name;
            _size = product._size;
        }

        public ProductViewBuilder()
        {
        }

        public ProductViewBuilder WithId(string id)
        {
            return new ProductViewBuilder(this)
            {
                _id = new Guid(id)
            };
        }

        public ProductView Build()
        {
            return new ProductView()
            {
                Code = _code,
                Id = _id,
                Name = _name,
                Size = _size
            };
        }

        public static implicit operator ProductView(ProductViewBuilder builder) => builder.Build();
    }
}