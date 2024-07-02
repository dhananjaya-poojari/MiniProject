
using Microsoft.EntityFrameworkCore;

namespace TinyUrl.Data
{
    public class DeleteExpiredData : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        public DeleteExpiredData(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();
            using var scope = _scopeFactory.CreateScope();
            var _dbContext = scope.ServiceProvider.GetRequiredService<AppDBContext>();
            var expired = await _dbContext.URLs.Where(x => x.ExpiryDate < DateTime.UtcNow).ToListAsync();
            if (expired.Any())
            {
                foreach (var item in expired)
                {
                    _dbContext.URLs.Remove(item);
                }
                await _dbContext.SaveChangesAsync();
            }
            Thread.Sleep(TimeSpan.FromMinutes(10));
            ExecuteAsync(stoppingToken);
        }
    }
}
