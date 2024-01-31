namespace EcommerceBackend.Models
{
    public class User
    {
        public int Id { get; init; }
        public string Auth0Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Gender { get; set; }
        public string Password { get; set; }
        //public List<Product>? Basket { get; set; } = new();
        public Basket Basket { get; set; } = new();
        public List<Order>? Orders { get; set; } = new();

        //public List<Review>? Reviews { get; set; }
        public bool IsLoggedIn { get; set; } = false;
    }
}
