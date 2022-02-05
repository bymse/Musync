using Microsoft.EntityFrameworkCore;
using Musync.DataLayer.Durable.Entity;

namespace Musync.DataLayer.Durable.Database;

public class MusyncDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Feed> Feeds { get; set; }
    public DbSet<UserFeedLink> UserFeedLinks { get; set; }
}