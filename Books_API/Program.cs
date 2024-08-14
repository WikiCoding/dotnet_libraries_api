using Books_API.Infra;
using Books_API.Persistence;
using Books_API.Repository;
using Books_API.Services;
using Microsoft.EntityFrameworkCore;
using Steeltoe.Common.Http.Discovery;
using Steeltoe.Discovery.Client;
using Steeltoe.Discovery.Consul;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddServiceDiscovery(o => o.UseConsul());

builder.Services.AddHttpClient<Client>(client => client.BaseAddress = new Uri("http://library-service"))
    .AddServiceDiscovery()
    .AddRoundRobinLoadBalancer();

builder.Services.AddScoped<IBookRepository, BookRepositoryImpl>();
builder.Services.AddScoped<BookService>();
builder.Services.AddScoped<BooksReservationServiceProducer>();
builder.Services.AddDbContext<BookDbContext>(options => options.UseMongoDB(builder.Configuration.GetConnectionString("books"), "books"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<BookDbContext>();
    if (app.Environment.IsDevelopment())
    {
        // auto database create-drop
        dbContext.Database.EnsureDeleted();
        dbContext.Database.EnsureCreated();
    }
}

app.Run();
