using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MSIdentity.Application.Interfaces;
using MSIdentity.Infrastructure.Interfaces;
using MSIdentity.Infrastructure.Services;
using MSIdentity.Persistence;
using MSIdentity.Persistence.Interceptors;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

var corsPolicyName = "defaultCorsPolicy";

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<AuditableEntitiesInterceptor>();

builder.Services.AddDbContext<IdentityDbContext>((serviceProvider, options) =>
{
    var interceptor = serviceProvider.GetService<AuditableEntitiesInterceptor>();
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly("MSIdentity"))
        .AddInterceptors(interceptor);
});

builder.Services.AddScoped<IIdentityDbContext>(provider => provider.GetRequiredService<IdentityDbContext>());

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetAssembly(typeof(MSIdentity.AssemblyReference))));

builder.Services.AddScoped<IJwtService, JwtService>();

builder.Services.AddSingleton<IIdentityRabbitMqProducer, IdentityRabbitMqProducer>();

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: corsPolicyName,
                      policy =>
                      {
                          policy.WithOrigins("http://localhost:4200").AllowAnyMethod().AllowCredentials().AllowAnyHeader();
                      });
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration.GetSection("AppSettings:Iss").Value,
            ValidAudience = builder.Configuration.GetSection("AppSettings:Aud").Value,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("AppSettings:Token").Value))
        };
    });

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(corsPolicyName);

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();