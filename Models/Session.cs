namespace EcommerceBackend.Models
{
    public class Session
    {
        public int Id { get; init; }
        public int userId { get; init; }
        public DateTime logonTime { get; set; }
        public DateTime lastInteraction { get; set; }
    }
}
