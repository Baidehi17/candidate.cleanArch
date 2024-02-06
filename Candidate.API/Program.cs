using Candidate.API.Utils;
using Candidate.Application.Implementation;
using Candidate.Application.Interface;
using Candidate.Infrastructure;
using Candidate.Infrastructure.Dbcontext;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Web;
using System.IdentityModel.Tokens.Jwt;

namespace Candidate.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                       .AddMicrosoftIdentityWebApi(options =>
                       {
                           builder.Configuration.Bind("AzureAd", options);
                           options.Events = new JwtBearerEvents();
                       }, options => { builder.Configuration.Bind("AzureAd", options); });


            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<CandidateDbContext>(options
               => options.UseSqlServer(builder.Configuration.GetConnectionString("candidateDB")));

            builder.Services.AddTransient<ICandidateRepository, CandidateRepository>();
            builder.Services.AddTransient<ICandidateService, CandidateService>();

            JwtSecurityTokenHandler.DefaultMapInboundClaims = false;

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme);

            builder.Services.Configure<JwtBearerOptions>(JwtBearerDefaults.AuthenticationScheme, options =>
            {
                options.TokenValidationParameters.RoleClaimType = "roles";
            });

            builder.Services.AddCors(p => p.AddPolicy("corsapp", builder =>
            {
                builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
            }));

            // Adding authorization policies that enforce authorization using Azure AD roles.
           builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy(AuthorizationPolicies.AssignmentToTaskUserRoleRequired, policy => policy.RequireRole(AppRole.TaskUser));
                options.AddPolicy(AuthorizationPolicies.AssignmentToTaskAdminRoleRequired, policy => policy.RequireRole(AppRole.TaskAdmin));
            });

            var app = builder.Build();
            // Configure the HTTP request pipeline.

            /* The UseDeveloperExceptionPage middleware it provide detailed error information in the browser when an unhandled 
            exception occurs in the application. It's part of the default middleware pipeline. */

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
                app.ExceptionHandling();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                /* The UseHsts(HTTP Strict Transport Security (HSTS)) middleware is used to enable HTTP Strict Transport Security(HSTS) for the application. 
                HSTS is a security feature that helps protect websites against protocol downgrade attacks and cookie hijacking 
                by instructing browsers to always use HTTPS(HTTP Secure) when communicating with the web server. */
                app.UseHsts();
            }
            app.UseCors("corsapp");

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}