
using Final�ertificationRecipeBook.Data;
using Microsoft.EntityFrameworkCore;

namespace FinalCertificationRecipeBook
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // �������� ������ ����������� � ������������
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

            // �������� DbContext � ��������� �����
            builder.Services.AddDbContext<RecipeBookContext>(options => options.UseNpgsql(connectionString).UseLazyLoadingProxies());

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
