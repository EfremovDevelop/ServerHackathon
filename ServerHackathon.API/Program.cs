using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using ServerHackathon.API.Extensions;
using ServerHackathon.Application.Services;
using ServerHackathon.Core.Interfaces.Auth;
using ServerHackathon.Core.Interfaces.Repositories;
using ServerHackathon.DataAccess;
using ServerHackathon.DataAccess.Repositories;
using ServerHackathon.Infrastructure.Auth;
using System.Text.Json.Serialization;

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

var builder = WebApplication.CreateBuilder(args);

var service = builder.Services;
var config = builder.Configuration;

service.Configure<JwtOptions>(config.GetSection(nameof(JwtOptions)));

service.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy
                            .SetIsOriginAllowed(origin => true) // Разрешить все источники
                            .AllowAnyMethod()
                            .AllowAnyHeader()
                            .AllowCredentials();
                      });
});

service.AddDbContext<DataContext>(
    options =>
    {
        options.UseNpgsql(config.GetConnectionString("DefaultConnection"));
    });

service.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
service.AddEndpointsApiExplorer();
service.AddSwaggerGen();

//Services
service.AddScoped<UsersService>();

service.AddHttpContextAccessor();

// Auth
service.AddScoped<IJwtProvider, JwtProvider>();
service.AddScoped<IPasswordHash, PasswordHash>();

//Repositories
service.AddScoped<IUsersRepository, UsersRepository>();

service.AddApiAuthentication(service.BuildServiceProvider().GetRequiredService<IOptions<JwtOptions>>());

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dataContext = scope.ServiceProvider.GetRequiredService<DataContext>();
    dataContext.Database.Migrate();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseCors(MyAllowSpecificOrigins);

app.MapControllers();

app.Run();
