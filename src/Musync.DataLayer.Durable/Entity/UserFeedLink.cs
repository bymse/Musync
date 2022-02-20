namespace Musync.DataLayer.Durable.Entity;

public class UserFeedLink
{
    public int UserFeedLinkId { get; set; }
    public int UserId { get; set; }
    public int FeedId { get; set; }
    
    public virtual User User { get; set; } = null!;
    public virtual Feed Feed { get; set; } = null!;
}