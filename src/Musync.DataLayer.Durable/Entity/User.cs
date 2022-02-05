namespace Musync.DataLayer.Durable.Entity;

public class User
{
    public int UserId { get; set; }
    
    public virtual ICollection<UserFeedLink> UserFeedLinks { get; set; }
}