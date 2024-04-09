using Microsoft.EntityFrameworkCore;

namespace IntexGroup210.Models;

public class IntexContext : DbContext
{
    public IntexContext(DbContextOptions<IntexContext> options) : base(options)
    {
        
    }
    
    public DbSet<Customer> Customers { get; set; }
}