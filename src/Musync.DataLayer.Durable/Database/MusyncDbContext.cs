using Microsoft.EntityFrameworkCore;
using Musync.DataLayer.Durable.Entity;

namespace Musync.DataLayer.Durable.Database;

public class MusyncDbContext : DbContext
{
    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Feed> Feeds { get; set; } = null!;
    public DbSet<UserFeedLink> UserFeedLinks { get; set; } = null!;
}