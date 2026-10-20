using Microsoft.EntityFrameworkCore;
using Midterms.Models;

namespace Midterms.Data;

public class CasinoDbContext : DbContext
{
    public CasinoDbContext(DbContextOptions<CasinoDbContext> options) : base(options) { }

    public DbSet<User> Users => Set<User>();
}