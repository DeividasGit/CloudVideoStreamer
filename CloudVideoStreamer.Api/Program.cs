using CloudVideoStreamer.Repository;
using CloudVideoStreamer.Repository.Interfaces;
using CloudVideoStreamer.Repository.Models;
using CloudVideoStreamer.Repository.Repositories;
using CloudVideoStreamer.Repository.Settings;
using CloudVideoStreamer.Service.Interfaces;
using CloudVideoStreamer.Service.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddNewtonsoftJson();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
  options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
  {
    Description = "JWT Authorization header using the bearer scheme. Enter: Bearer [token].",
    Name = "Authorization",
    In = ParameterLocation.Header,
    Scheme = "Bearer"
  });
  options.AddSecurityRequirement(new OpenApiSecurityRequirement()
  {
    {
      new OpenApiSecurityScheme()
      {
        Reference = new OpenApiReference()
        {
          Id = "Bearer",
          Type = ReferenceType.SecurityScheme
        },
        Scheme = "oauth2",
        Name = "Bearer",
        In = ParameterLocation.Header
      },
      new List<string>()
    }
  });
});

builder.Configuration.AddEnvironmentVariables();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
var environmentConnectionString = Environment.GetEnvironmentVariable(connectionString) ?? connectionString;

builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(environmentConnectionString));

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IMovieService, MovieService>();
builder.Services.AddScoped<IMediaContentService, MediaContentService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IRoleService, RoleService>();

builder.Services.AddCors(options =>
  options.AddDefaultPolicy(policy =>
  //policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()
    {
        policy
            .WithOrigins("http://localhost:60301", "http://192.168.37.237:19006", "http://10.0.2.2:19006") // your React app origin
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials(); // <- IMPORTANT
    }
  )
);

builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));

var jwtSecret = builder.Configuration.GetValue<string>("InternalAuthKey");
var environmentJwtSecret = Environment.GetEnvironmentVariable(jwtSecret) ?? jwtSecret;

builder.Services.AddAuthentication(options =>
{
  options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
  options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
  options.SaveToken = true;
  options.TokenValidationParameters = new TokenValidationParameters()
  {
    ValidateIssuerSigningKey = true,
    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(environmentJwtSecret)),
    ValidateIssuer = false,
    ValidateAudience = false,
    ValidateLifetime = true,
    ClockSkew = TimeSpan.Zero
  };
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();