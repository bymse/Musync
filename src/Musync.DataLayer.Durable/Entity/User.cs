namespace Musync.DataLayer.Durable.Entity;

public class User
{
    public User()
    {
        UserFeedLinks = new HashSet<UserFeedLink>();
    }

    public int UserId { get; set; }
    
    public virtual ICollection<UserFeedLink> UserFeedLinks { get; set; }
}