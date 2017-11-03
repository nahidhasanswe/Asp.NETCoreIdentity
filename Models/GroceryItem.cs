namespace AuthApp.Models
{
    public class GroceryItem
    {
        public long Id { get; set; }
        public string Description { get; set; }
    }

    public class JWTSettings
    {
        public string SecretKey { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
    }
}