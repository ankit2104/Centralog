using AuthService.Jwt;
using DBService.MongoDB;
using KafkaConsumer;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Repository;
using Repository.Auth;
using Repository.Auth.AuthenticationModels;
using Repository.Auth.AuthorizationModels;
using System.Text;
namespace Core
{
    public class ServiceBuilder
    {
        public WebApplicationBuilder builder;
        public ServiceBuilder(WebApplicationBuilder builder)
        {
            this.builder = builder;
        }
        public WebApplicationBuilder GetWebApplicationBuilder()
        {
            var mongoDbSettings = builder.Configuration.GetSection("MongoDBSettings").Get<MongoDBSettings>();
            var JwtSettings = builder.Configuration.GetSection("JWT").Get<JwtAuth>();



            builder.Services.AddIdentity<ApplicationUser, ApplicationRoles>()
                .AddMongoDbStores<ApplicationUser, ApplicationRoles, Guid>
                (
                    mongoDbSettings.ConnectionString, mongoDbSettings.Name
                ).AddDefaultTokenProviders();
            builder.Services.AddScoped<IJwtService, JwtService>();
            builder.Services.AddSingleton<IMongoDBService, ProjectDBService>();
            builder.Services.AddHostedService<ConsumerService>();

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options =>
                {
                    options.SaveToken = true;
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidAudience = JwtSettings.ValidAudience,
                        ValidIssuer = JwtSettings.ValidIssuer,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtSettings.Secret))
                    };
                });

            builder.Services.AddControllers();

            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddSwaggerGen();

            return this.builder;
        }

    }
}
