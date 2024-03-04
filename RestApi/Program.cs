using Domain;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using RestApi.Extensions;
using System.Text;
using Persistence;
using Microsoft.EntityFrameworkCore;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;
using Hangfire;
using Hangfire.PostgreSql;

EnvironmentFetcher.Fetch();

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidators();


builder.Services.AddDbContext<InquiriesContext>(OptionsBuilder =>
{
	OptionsBuilder.UseNpgsql(EnvironmentFetcher.PostgresConnectionString);
});
builder.Services
	.AddPersistence()
	.AddDomain();

builder.Services.AddHangfire(configuration =>
	configuration.UsePostgreSqlStorage(options =>
	{
		options.UseNpgsqlConnection(EnvironmentFetcher.PostgresConnectionString);
	}));
builder.Services.AddHangfireServer(options => { options.WorkerCount = 1; });

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
	.AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
	{
		ValidateIssuer = true,
		ValidateAudience = true,
		ValidateLifetime = true,
		ValidateIssuerSigningKey = true,
		ValidIssuers = new List<string> { "https://localhost:7254/", "http://localhost:5108/" },
		ValidAudiences = new List<string> { "https://localhost:7254/", "http://localhost:5108/" },
		IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(EnvironmentFetcher.IssuerSecretKey!)),
	};
});

builder.Services.AddAuthorization();

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

app.MapControllers();

app.UseHangfireServer(() => new BackgroundJobServer());

if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development")
{
    await using var scope = app.Services.CreateAsyncScope();
    var dbContext = scope.ServiceProvider.GetRequiredService<InquiriesContext>();
    await dbContext.Database.MigrateAsync();
    await dbContext.DisposeAsync();
    await scope.DisposeAsync();
}

app.Run();