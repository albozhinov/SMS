namespace SMS
{
    using BasicWebServer.Server;
    using BasicWebServer.Server.Routing;
    using SMS.Data;
    using SMS.Data.Common;
    using SMS.Services.Contracts;
    using SMS.Services.Services;
    using System.Threading.Tasks;

    public class StartUp
    {
        public static async Task Main()
        {
            var server = new HttpServer(routes => routes
               .MapControllers()
               .MapStaticFiles());

            server.ServiceCollection
                .Add<IUserService, UserService>()
                .Add<SMSDbContext>()
                .Add<IRepository, Repository>();

            await server.Start();
        }
    }
}