using System.Text;
using Bogus;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using OnlineLibrarySystem.Data;
using OnlineLibrarySystem.Data.Models;
using OnlineLibrarySystem.Data.Repositories;
using OnlineLibrarySystem.Data.Services;
using OnlineLibrarySystem.Jwt;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

const string jwtSecret = "C9080BAE-4DA1-4037-AC23-ECE68845B381";

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<OnlineLibrarySystemDbContext>(opt
    => opt.UseSqlite("Data Source=../OLS.db"));

builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddScoped<IBookService, BookService>();

builder.Services.AddSingleton<IJwtTokenHandler>(new JwtTokenHandler(jwtSecret));

builder.Services.AddAuthorization(auth =>
{
    auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
        .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
        .RequireAuthenticatedUser().Build());
});

var key = Encoding.ASCII.GetBytes(jwtSecret);
builder.Services.AddAuthentication(x =>
    {
        x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(x =>
    {
        x.Events = new JwtBearerEvents
        {
            OnTokenValidated = tokenValidatedContext =>
            {
                var userName = tokenValidatedContext.Principal.Identity.Name;
               
                if (string.IsNullOrEmpty(userName))
                {
                    // return unauthorized if user no longer exists
                    tokenValidatedContext.Fail("Unauthorized");
                }
                return Task.CompletedTask;
            }
        };
        x.RequireHttpsMetadata = false;
        x.SaveToken = true;
        x.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = false,
            ValidateAudience = false
        };
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();


// MIGRATIONS ON STARTUP
await using var scope = app.Services.CreateAsyncScope();
await using var db = scope.ServiceProvider.GetService<OnlineLibrarySystemDbContext>();

await db.Database.MigrateAsync();

// SEED DB 

// Clear books
var books = await db.Books.ToListAsync();
foreach (var book in books)
{
    db.Books.Remove(book);
}

await db.SaveChangesAsync();

var faker = new Faker();
// Add 
for (var i = 0; i < 100; i++)
{
    db.Books.Add(new Book
    {
        BookName = faker.Lorem.Text(),
        Author = new Faker().Person.FullName,
        Publisher = faker.Company.CompanyName()
    });
}

await db.SaveChangesAsync();


app.Run();