
using Asp.Versioning;
using DemoBookAPI.Configuration;
using DemoBookAPI.Core.Interfaces;
using DemoBookAPI.Domain.JWT;
using DemoBookAPI.EF;
using DemoBookAPI.EF.Repositories;
using DemoBookAPI.Filters;
using DemoBookAPI.Middlewares;
using DemoBookAPI.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Configuration;
using System.Net.Mail;
using System.Text;

namespace DemoBookAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            // Add services to the container.
            //builder.Configuration.AddJsonFile("Config.json");


            //IOptions pattern
            //way 3 this is best for use.
            //IOptions<AttachmentOptions> registered as singleton
            //IOptionsSnapshot<AttachmentOptions> registered as scopped and it reads with the first access of the configuration.
            //It refresh reading the configuration with each request.
            //you cannot inject singleton inside socopped but scopped can inject singleton.
            //IOptionsMonitor<AttachmentOptions> registered as singleton
            //It refresh reading the configuration with each change in the configuration and it can be in the same request.
            builder.Services.Configure<AttachmentOptions>(builder.Configuration.GetSection("Attachments"));

            //way 1
            //var attchementOptions = builder.Configuration.GetSection("Attachments").Get<AttachmentOptions>();
            //builder.Services.AddSingleton(attchementOptions);
            //way 2
            //var attchementOptions = new AttachmentOptions();
            //builder.Configuration.GetSection("Attachments").Bind(attchementOptions);
            //builder.Services.AddSingleton(attchementOptions);



            builder.Services.AddControllers(Option => {
                Option.Filters.Add<LogActivityFilter>();
            });
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            //EF
            builder.Services.AddDbContext<DemoBookAPIContext>(options =>
                options.UseSqlServer(
                    builder.Configuration.GetConnectionString("DefaultConnection"),
                        b => b.MigrationsAssembly(typeof(DemoBookAPIContext).Assembly.FullName)));
            //builder.Services.AddDbContext<DemoBookAPIContext>(options =>
            //    options.UseInMemoryDatabase("dbData"));

            //builder.Services.AddTransient(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<IAuthService, AuthService>();
            builder.Services.Configure<JWT>(builder.Configuration.GetSection("JWT"));
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<DemoBookAPIContext>();
            
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(o =>
                {
                    o.RequireHttpsMetadata = false;
                    o.SaveToken = false;
                    o.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidIssuer = builder.Configuration["JWT:Issuer"],
                        ValidAudience = builder.Configuration["JWT:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"]))
                    };
                });

            //api versioning
            builder.Services.AddApiVersioning(options =>
            {
                options.DefaultApiVersion = new ApiVersion(1);
                options.ReportApiVersions = true;
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ApiVersionReader = ApiVersionReader.Combine(
                    new UrlSegmentApiVersionReader(),
                    new HeaderApiVersionReader("api-version"));
            }).AddApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'V";
                options.SubstituteApiVersionInUrl = true;
            });
    
            var app = builder.Build();
            
            var apiVersionSet = app.NewApiVersionSet()
.HasDeprecatedApiVersion(new ApiVersion(1, 0))
.HasApiVersion(new ApiVersion(2, 0))
.HasApiVersion(new ApiVersion(3, 0))
.ReportApiVersions()
.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseMiddleware<ProfilingMiddleware>();
            app.UseMiddleware<RateLimitingMiddleware>();

            app.MapControllers();

            app.Run();
        }
    }
}
