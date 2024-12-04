using Microsoft.EntityFrameworkCore;
using MyWebApp.Models;

namespace MyWebApp.Data
{
    public class MyWebAppDbContext : DbContext
    {
        public MyWebAppDbContext(DbContextOptions<MyWebAppDbContext> options)
            :base(options){}
        public DbSet<Expense> Expenses { get; set;}
    }
}