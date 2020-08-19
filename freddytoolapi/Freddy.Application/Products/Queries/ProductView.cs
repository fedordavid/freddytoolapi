using System;

namespace Freddy.Application.Products.Queries
{
    public class ProductView
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Size { get; set; }

        protected bool Equals(ProductView other)
        {
            return Id.Equals(other.Id) && Code == other.Code && Name == other.Name && Size == other.Size;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;

            return obj.GetType() == GetType() && Equals((ProductView) obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Code, Name, Size);
        }
    }
}
