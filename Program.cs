
using FinalСertificationRecipeBook.Data;
using FinalСertificationRecipeBook.Repositories;
using Microsoft.EntityFrameworkCore;

namespace FinalCertificationRecipeBook
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Добавьте строку подключения в конфигурации
            string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

            // Добавьте DbContext в контейнер служб
            builder.Services.AddDbContext<RecipeBookContext>(options => options.UseNpgsql(connectionString).UseLazyLoadingProxies());

            // Регистрация репозиторий
            builder.Services.AddScoped<IRecipeRepository, RecipeRepository>();

            builder.Services.AddControllers();
            
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

          
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();
            app.UseAuthentication();

            app.MapControllers();

            app.Run();
        }
    }
}
