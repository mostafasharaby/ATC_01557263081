namespace EventBooking.Domain.Entities
{
    public class EventTag
    {
        public int EventId { get; set; }
        public Event? Event { get; set; }
        public int TagId { get; set; }
        public Tag? Tag { get; set; }
    }

    public class Tag : BaseEntity
    {
        public string? Name { get; set; }
        public ICollection<EventTag>? EventTags { get; set; } = new List<EventTag>();
    }
}
