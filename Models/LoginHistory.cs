namespace E_Commerce.Models
{
    public class LoginHistory:SharedModel
    {

        public string UserId { get; set; }
        public virtual AppUser Appuser { get; set; }
        public string IpAddress { get; set; }
        public string ClientInfo { get; set; }
        public string Os { get; set; }
        public DateTime ValidTill { get; set; }
        public DateTime ClosedOn { get; set; }
        public string Token { get; set; } = Guid.NewGuid().ToString().Replace(".", "");

    }
}
