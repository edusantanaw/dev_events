using Persistence;
using Microsoft.EntityFrameworkCore;
namespace Rest;


public class Program
{
    public static void Main(string[] args)
    { 
        string connection = "Host=localhost;Database=dev;Username=postgres;Password=edusantanaw";
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddControllers();
        // builder.Services.AddDbContext<DevEventDbContext>(o => o.UseInMemoryDatabase("DevEventsDb"));
        builder.Services.AddDbContext<DevEventDbContext>(o => o.UseNpgsql(connection));

        builder.Services.AddEndpointsApiExplorer();
        var app = builder.Build();
        app.MapControllers();
        app.Run();
    }
}
