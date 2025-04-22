using Microsoft.EntityFrameworkCore;
using ToDoApplication.Common.Models.Database;

namespace ToDoApplication.Infrastructure.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            Database.SetCommandTimeout(TimeSpan.FromSeconds(60));
        }
        public DbSet<TodoItem> TodoItem { get; set; }
    }
}
