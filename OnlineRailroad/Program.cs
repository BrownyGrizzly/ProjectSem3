using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using OnlineRailroad.Data;
using OnlineRailroad.Models;
using OnlineRailroad.Services;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Add MySQL Database Service
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),
    new MySqlServerVersion(new Version(8, 0, 23))));

// JWT configuration
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});

builder.Services.AddAuthorization(); // Optional: Configure more advanced policies if needed

// Register User services
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();

// Register Train services
builder.Services.AddScoped<ITrainService, TrainService>();

// Register Station services
builder.Services.AddScoped<IStationService, StationService>();

// Register Schedule services
builder.Services.AddScoped<IScheduleService, ScheduleService>();

// Register PassengerDetail services
builder.Services.AddScoped<IPassengerDetailService, PassengerDetailService>();

// Register Route services
builder.Services.AddScoped<IRouteService, RouteService>();

// Register FareRule services
builder.Services.AddScoped<IFareRuleService, FareRuleService>();

// Register Distance services
builder.Services.AddScoped<IDistanceService, DistanceService>();

// Register Search services
builder.Services.AddScoped<ISearchService, SearchService>();


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Enable CORS if needed
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder => builder.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.UseCors("AllowAllOrigins"); // Enable CORS policy

app.MapControllers();

app.Run();