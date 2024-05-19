
using Quiz.Contracts.Interfaces;
using Quiz.Contracts.Services;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

using Quiz.Contracts.Helpers;
using Quiz.Infrastructure.Data;
using Quiz.Infrastructure;

namespace Wattana.Infrastructure
{
    public static class ServiceExtenstions
    {
        private static IServiceCollection AddUnitOfWork(this IServiceCollection services)
        {
            return services.AddTransient<IUnitOfWork, UnitOfWork>();
        }

        private static IServiceCollection AddDatabaseContext(this IServiceCollection services)
        {
            return services.AddDbContext<EntitiesContext>(options => options.UseSqlServer("name=ConnectionStrings:ConnectionSQLServer"));
        }

        public static IServiceCollection AddPersistence(this IServiceCollection services)
        {
            return services.AddDatabaseContext().AddUnitOfWork();
        }

        public static IServiceCollection AddDateTime(this IServiceCollection services)
        {
            return services.AddSingleton<IDateTime, DateTimeService>();
        }

        public static IServiceCollection AddDatabaseManager(this IServiceCollection services)
        {
            return services.AddScoped<IDatabaseManager, DatabaseManagerService>();
        }

        public static IServiceCollection AddHttpFormService(this IServiceCollection services)
        {
            return services.AddSingleton<IHttpFormService, HttpFormService>();
        }

        public static IServiceCollection AddEnumService(this IServiceCollection services)
        {
            return services.AddSingleton<IEnumService, EnumService>();
        }

        public static IServiceCollection AddLoggingService(this IServiceCollection services)
        {
            return services.AddTransient<LoggingHandler>();
        }
    }
}
