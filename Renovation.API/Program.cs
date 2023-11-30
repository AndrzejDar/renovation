using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Renovation.API.Data;
using Renovation.API.Mappings;
using Renovation.API.Repositories;
using System.Text;
using Microsoft.OpenApi.Models;
using Microsoft.Extensions.Logging.AzureAppServices;

//  Define cors policy name
var AllowLocalhost3000 = "_AllowLocalhost3000";

// Create Builder
var builder = WebApplication.CreateBuilder(args);

//Add Azure loger and its config
builder.Logging.AddAzureWebAppDiagnostics();
builder.Services.Configure<AzureFileLoggerOptions>(options =>
{
    options.FileName = "logs-";
    options.FileSizeLimit = 50 * 1024;
    options.RetainedFileCountLimit = 3;
});


// Add services to the container.
builder.Services.AddControllers();

// Add Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>{ 
    options.SwaggerDoc("v1", new OpenApiInfo { Title= "Renovations API", Version= "v1" });
    options.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
    {
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = JwtBearerDefaults.AuthenticationScheme
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = JwtBearerDefaults.AuthenticationScheme
                },
                Scheme = "Oauth2",
                Name = JwtBearerDefaults.AuthenticationScheme,
                In = ParameterLocation.Header
            },
            new List<string>()
        }
    }
    );
});


//add connection strings from azure appconfig or fallback to appsettings.json
builder.Services.AddDbContext<RenovationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("RenovationConnectionString")));

builder.Services.AddDbContext<RenovationAuthDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("RenovationAuthConnectionString")));

//add Repositories
builder.Services.AddScoped<IRegionRepository, SQLRegionRepository>();
builder.Services.AddScoped<IProjectRepository, SQLProjectRepository>();
builder.Services.AddScoped<ITokenRepository, TokenRepository>();

//add Automapper
builder.Services.AddAutoMapper(typeof(AutoMapperProfiles));

//add identities
builder.Services.AddIdentityCore<IdentityUser>().AddRoles<IdentityRole>()
    .AddTokenProvider<DataProtectorTokenProvider<IdentityUser>>("Renovation")
    .AddEntityFrameworkStores<RenovationAuthDbContext>()
    .AddDefaultTokenProviders();

builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 1;
});

//add Authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience=true,
        ValidateLifetime=true,
        ValidateIssuerSigningKey=true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    });


//Add Cors setup for localhost
builder.Services.AddCors((options) => {
    options.AddPolicy(name: AllowLocalhost3000, policy => policy.WithOrigins("http://localhost:3000", "https://localhost:3000").AllowAnyHeader().AllowAnyMethod());
});

var app = builder.Build();

//Configure logger
var loggerF = app.Services.GetRequiredService<ILoggerFactory>();
var logger = loggerF.CreateLogger("Startup");

// Configure Swagger to run on develop
if (app.Environment.IsDevelopment())
{
    logger.LogInformation("Application is running in Devleopment mode");
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    logger.LogInformation("Application is running in Production mode");
}


app.UseHttpsRedirection();

app.UseCors(AllowLocalhost3000);

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
