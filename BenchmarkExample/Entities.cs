using Microsoft.EntityFrameworkCore;

namespace BenchmarkExample;

public class TestContext : DbContext
{
    public DbSet<Item> Items { get; set; }

    public string DbPath { get; }

    public TestContext()
    {
        var folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder);
        DbPath = Path.Join(path, "test.db");
    }

    // The following configures EF to create a Sqlite database file in the
    // special "local" folder for your platform.
    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite($"Data Source={DbPath}");
}

public class Item
{
    public int Id { get; set; }
    public DateTime DateTime { get; set; } = default!;
}
