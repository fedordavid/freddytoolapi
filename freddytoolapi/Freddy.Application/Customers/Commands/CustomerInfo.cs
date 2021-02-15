namespace Freddy.Application.Customers.Commands
{
    public class CustomerInfo
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        public CustomerInfo(string name, string email, string phone)
        {
            Name = name;
            Email = email;
            Phone = phone;
        }

        public CustomerInfo() { }
    }
}
