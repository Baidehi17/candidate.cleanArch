using Candidate.Application.Implementation;
using Candidate.Application.Interface;
using Candidate.Infrastructure;
using Candidate.Infrastructure.Dbcontext;
using Microsoft.EntityFrameworkCore;

namespace Candidate.API
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

            builder.Services.AddDbContext<CandidateDbContext>(options
               => options.UseSqlServer(builder.Configuration.GetConnectionString("candidateDB")));

            builder.Services.AddTransient<ICandidateRepository, CandidateRepository>();
            builder.Services.AddTransient<ICandidateService, CandidateService>();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.ExceptionHandling();
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