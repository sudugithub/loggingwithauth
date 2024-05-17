using Data.Repositories;
using Data.Repositories.BlogRepository;
using Data.Repositories.LogEventRepository;
using Data.Repositories.UserRepository;
using Data.Utils.UserContext;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Converters;
using Service.AuthService;
using Service.BlogService;
using Service.LogEventService;
using Service.UserService;
using SpanTechnologyTask.Filters;

namespace SpanTechnologyTask
{
    public class ComponentRegistry
    {
        public static void Register(IServiceCollection serviceCollection, IConfiguration Configuration)
        {
            serviceCollection.AddControllers().AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.Converters.Add(new StringEnumConverter());
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            });

            serviceCollection.AddControllers(options =>
            {
                options.Filters.Add(typeof(ExceptionFilter));
            });

            var connectionString = Configuration.GetConnectionString("Repository");
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new Exception("Database not found");
            }
            
            serviceCollection.AddDbContext<Repository>(opt => opt.UseMySQL(connectionString));

            serviceCollection.AddHttpContextAccessor();
            serviceCollection.AddScoped<IUserContext, UserContext>();

            serviceCollection.AddScoped<IAuthService, AuthService>();

            serviceCollection.AddScoped<IUserRepository, UserRepository>();
            serviceCollection.AddScoped<IUserService, UserService>();

            serviceCollection.AddScoped<IBlogRepository, BlogRepository>();
            serviceCollection.AddScoped<IBlogService, BlogService>();

            serviceCollection.AddScoped<ILogEventRepository, LogEventRepository>();
            serviceCollection.AddScoped<ILogEventService, LogEventService>();
        }
    }
}
