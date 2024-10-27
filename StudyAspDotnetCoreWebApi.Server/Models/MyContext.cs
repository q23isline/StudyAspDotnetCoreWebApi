using Microsoft.EntityFrameworkCore;

namespace StudyAspDotnetCoreWebApi.Models;

public class MyContext : DbContext
{
    public MyContext(DbContextOptions<MyContext> options) : base(options) { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.AddInterceptors(new TimestampInterceptor());
    }
}
