using Microsoft.EntityFrameworkCore;
using Midterms.Data;
using Midterms.Models;

namespace Midterms.Services;

public class UserStateService
{
    private readonly IDbContextFactory<CasinoDbContext> _dbFactory;

    public User? CurrentUser { get; private set; }
    public event Action? OnChange;

    public UserStateService(IDbContextFactory<CasinoDbContext> dbFactory)
    {
        _dbFactory = dbFactory;
    }

    public async Task<bool> LoginAsync(string username)
    {
        using var db = _dbFactory.CreateDbContext();
        var user = await db.Users.FirstOrDefaultAsync(u => u.Username.ToLower() == username.Trim().ToLower());
        if (user != null)
        {
            CurrentUser = user;
            NotifyStateChanged();
            return true;
        }
        return false;
    }

    public async Task<bool> CreateUserAsync(string username)
    {
        using var db = _dbFactory.CreateDbContext();
        bool exists = await db.Users.AnyAsync(u => u.Username.ToLower() == username.Trim().ToLower());
        if (exists) return false;

        var newUser = new User { Username = username.Trim(), Balance = 1000m };
        db.Users.Add(newUser);
        await db.SaveChangesAsync();

        CurrentUser = newUser;
        NotifyStateChanged();
        return true;
    }

    public async Task UpdateBalanceAsync(decimal delta)
    {
        if (CurrentUser == null) return;

        using var db = _dbFactory.CreateDbContext();
        var user = await db.Users.FindAsync(CurrentUser.Id);
        if (user != null)
        {
            user.Balance += delta;
            await db.SaveChangesAsync();
            CurrentUser.Balance = user.Balance;
            NotifyStateChanged();
        }
    }

    public void Logout()
    {
        CurrentUser = null;
        NotifyStateChanged();
    }

    private void NotifyStateChanged() => OnChange?.Invoke();
}