using Identity.Application.Interfaces;
using Identity.Infrastructure.Services;
using Identity.Persistence;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Product.Application.Interfaces;
using Product.Persistence;
using System.Reflection;
using System.Text;
using Upsell.Interceptors;
using User.Application.Interfaces;
using User.Infrastructure.Services;
using User.Persistence;

var builder = WebApplication.CreateBuilder(args);

var corsPolicyName = "defaultCorsPolicy";

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<AuditableEntitiesInterceptor>();

builder.Services.AddDbContext<IdentityDbContext>((serviceProvider, options) =>
{
    var interceptor = serviceProvider.GetService<AuditableEntitiesInterceptor>();
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly("Identity"))
        .AddInterceptors(interceptor);
});

builder.Services.AddDbContext<UserDbContext>((serviceProvider, options) =>
{
    var interceptor = serviceProvider.GetService<AuditableEntitiesInterceptor>();
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly("User"))
        .AddInterceptors(interceptor);
});

builder.Services.AddScoped<IProductDbContext>(provider => provider.GetRequiredService<ProductDbContext>());
builder.Services.AddScoped<IIdentityDbContext>(provider => provider.GetRequiredService<IdentityDbContext>());
builder.Services.AddScoped<IUserDbContext>(provider => provider.GetRequiredService<UserDbContext>());

builder.Services.AddDbContext<ProductDbContext>((serviceProvider, options) =>
{
    var interceptor = serviceProvider.GetService<AuditableEntitiesInterceptor>();
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly("Product"))
        .AddInterceptors(interceptor);
});

builder.Services.AddAutoMapper(typeof(Identity.AssemblyReference));

builder.Services.AddAutoMapper(typeof(User.AssemblyReference));

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetAssembly(typeof(Identity.AssemblyReference))));

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetAssembly(typeof(User.AssemblyReference))));

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetAssembly(typeof(Product.AssemblyReference))));

builder.Services.AddScoped<IJwtService, JwtService>();

builder.Services.AddHostedService<UserRabbitMqConsumer>();

builder.Services.AddSingleton<IIdentityRabbitMqProducer, IdentityRabbitMqProducer>();

builder.Services.AddSingleton<IUserEventProcessor, UserEventProcessor>();

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

// Configure the HTTP request pipeline.
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