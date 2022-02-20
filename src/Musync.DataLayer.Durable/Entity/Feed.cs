using System.ComponentModel.DataAnnotations;
using Musync.Common;
using Musync.DataLayer.Durable.Abstract;

namespace Musync.DataLayer.Durable.Entity;

public class Feed : IFeed
{
    public int FeedId { get; set; }
    
    public FeedType FeedType { get; set; }
    
    [Required]
    [StringLength(100)]
    public string ExternalFeedId { get; set; } = null!;
    
    public DateTimeOffset LastReadTime { get; set; }
    
    [StringLength(200)]
    public string? LastPostId { get; set; }
    
    public virtual ICollection<UserFeedLink> UserFeedLinks { get; set; }
}