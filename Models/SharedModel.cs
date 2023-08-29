namespace E_Commerce.Models
{
    public class SharedModel
    {
        public SharedModel()
        {
            Id=Path.GetRandomFileName().Replace(".","");
            EntryTime = DateTime.UtcNow;

        }
        public string Id { get; set; } 
        public DateTime? EntryTime { get; set; }
    }
}
