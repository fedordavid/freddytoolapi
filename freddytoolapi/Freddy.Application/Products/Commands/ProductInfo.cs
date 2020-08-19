namespace Freddy.Application.Products.Commands
{
    public class ProductInfo
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Size { get; set; }

        public ProductInfo(string code, string name, string size)
        {
            Code = code;
            Name = name;
            Size = size;
        }

        public ProductInfo() {}
    }
}