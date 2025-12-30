using Microsoft.EntityFrameworkCore;
using PastebinApi.Models;

namespace PastebinApi.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Paste> Pastes { get; set; }
}
