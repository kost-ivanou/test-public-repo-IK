using Lab5TestTask.Data;
using Lab5TestTask.Models;
using Lab5TestTask.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Lab5TestTask.Services.Implementations;

/// <summary>
/// SessionService implementation.
/// Implement methods here.
/// </summary>
public class SessionService : ISessionService
{
    private readonly ApplicationDbContext _dbContext;
    private readonly DateTime startOf2025UTC = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc);

    public SessionService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Session> GetSessionAsync()
    {
        var session = await _dbContext.Sessions.Where(s => s.DeviceType == Enums.DeviceType.Desktop)
            .OrderBy(s => s.StartedAtUTC)
            .FirstOrDefaultAsync();
        return session;
    }

    public async Task<List<Session>> GetSessionsAsync()
    {
        var sessions = await _dbContext.Sessions.Where(s => s.User.Status == Enums.UserStatus.Active && s.EndedAtUTC < startOf2025UTC)
            .ToListAsync();
        return sessions;
    }
}
