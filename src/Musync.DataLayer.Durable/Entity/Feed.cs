using System.ComponentModel.DataAnnotations;

namespace Musync.DataLayer.Durable.Entity;

public class Feed
{
    public int FeedId { get; set; }
    
    public FeedType FeedType { get; set; }
    
    [Required]
    [StringLength(100)]
    public string ExternalFeedId { get; set; } = null!;
    
    public DateTimeOffset LastReadTime { get; set; }
    
    public virtual ICollection<UserFeedLink> UserFeedLinks { get; set; }
}