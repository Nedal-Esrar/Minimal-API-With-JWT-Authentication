using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using MinimalApiWithJwtAuthentication.API.Configurations;
using MinimalApiWithJwtAuthentication.API.Interfaces;
using MinimalApiWithJwtAuthentication.API.Repositories;
using MinimalApiWithJwtAuthentication.API.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services
  .Configure<JwtAuthenticationConfig>(builder.Configuration.GetSection(nameof(JwtAuthenticationConfig)))
  .AddScoped<IUserRepository, InMemoryUserRepository>()
  .AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();

builder.Services.AddControllers();

builder.Services.AddAuthentication(options =>
{
  options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
  options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
  options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
  var config = builder.Configuration.GetSection(nameof(JwtAuthenticationConfig)).Get<JwtAuthenticationConfig>()!;

  var key = Encoding.UTF8.GetBytes(config.Key);

  options.TokenValidationParameters = new TokenValidationParameters
  {
    ValidateIssuer = true,
    ValidateAudience = true,
    ValidateLifetime = true,
    ValidateIssuerSigningKey = true,
    ValidIssuer = config.Issuer,
    ValidAudience = config.Audience,
    IssuerSigningKey = new SymmetricSecurityKey(key),
    ClockSkew = TimeSpan.Zero
  };
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();