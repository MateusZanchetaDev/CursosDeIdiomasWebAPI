using CursosDeIdiomasWebAPI.DataAccess;
using CursosDeIdiomasWebAPI.Repository;
using CursosDeIdiomasWebAPI.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CursosDeIdiomasWebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddEntityFrameworkSqlServer().AddDbContext<CursoDeIdiomasDbContext>
                (
                options => options.UseSqlServer(builder.Configuration.GetConnectionString("DataBase"))
                );

            builder.Services.AddScoped<IAlunoRepository, AlunoRepository>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}