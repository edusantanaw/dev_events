using Persistence;
namespace c_;


public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddControllers();
        builder.Services.AddSingleton<DevEventDbContext>();
        builder.Services.AddEndpointsApiExplorer();
        var app = builder.Build();
        app.MapControllers();
        app.Run();
    }
}
