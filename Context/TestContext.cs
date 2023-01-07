namespace FirstDemoCS.Context;

using Microsoft.EntityFrameworkCore;
using FirstDemoCS.Models;

public class TestContext : DbContext
{
    public TestContext(DbContextOptions<TestContext> options)
        : base(options)
    {
    }

    public DbSet<Test>? Tests { get; set; }
}