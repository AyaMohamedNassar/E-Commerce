using Core.Entities;
using Core.Interfaces;
using E_CommerceAPI.Errors;
using E_CommerceAPI.Helpers;
using E_CommerceAPI.Middleware;
using Infrastructure.Data;
using Infrastructure.Repository;
using Infrastructure.UnitOfWork;
using Infrastructure.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Builder;
using static System.Net.Mime.MediaTypeNames;

namespace E_CommerceAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string txt = "";

            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddCors(options => options.AddPolicy(txt, builder =>
            {
                builder.AllowAnyOrigin();
                builder.AllowAnyMethod();
                builder.AllowAnyHeader();
            }));
            // Add services to the container.

            builder.Services.AddControllers();

            builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<StoreContext>();

            //Configer Error message from our api 
            builder.Services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = ActionContext =>
                {
                    var errors = ActionContext.ModelState.Where(error => error.Value.Errors.Count > 0)
                    .SelectMany(value => value.Value.Errors)
                    .Select(error => error.ErrorMessage).ToArray();

                    var errorResponse = new ApiValidation
                    {
                        Errors = errors
                    };

                    return new BadRequestObjectResult(errorResponse);
                };
            });

            builder.Services.AddDbContext<StoreContext>(options => {
                options.UseLazyLoadingProxies()
                .UseSqlServer(builder.Configuration.GetConnectionString("StoreConnection"));

                options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            }) ;
        

            builder.Services.AddScoped< IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<ItokenService, TokenService>();

            builder.Services.AddAutoMapper(typeof(MappingProfiles));

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = "StoreSchema";
            }).AddJwtBearer("StoreSchema",
                opt =>
                {
                    string key = "Look forward, take a step, keep going untill you make it and after.";

                    SymmetricSecurityKey secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));

                    opt.TokenValidationParameters = new TokenValidationParameters
                    {
                        IssuerSigningKey = secretKey,
                        ValidateLifetime = true,
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                }
            );

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseMiddleware<ExceptionMeddleware>();

            app.UseStatusCodePagesWithReExecute("/errors/{0}");

            //app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseCors(txt);

            app.MapControllers();

            app.Run();
        }
    }
}
