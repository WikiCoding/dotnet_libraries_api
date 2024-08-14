using Library_Rest_API.Domain;
using Library_Rest_API.Domain.Repo;
using Library_Rest_API.Domain.UseCases;
using Library_Rest_API.Persistence;
using Library_Rest_API.Persistence.Repository;
using Library_Rest_API.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ILibraryFactory, LibraryFactoryImpl>();
builder.Services.AddScoped<ILibraryRepository, LibraryRepositoryImpl>();
builder.Services.AddScoped<ICreateLibrary, CreateLibraryService>();
builder.Services.AddScoped<IGetAllLibraries, GetAllLibrariesService>();
builder.Services.AddScoped<IGetLibraryById, GetLibraryByIdService>();
builder.Services.AddScoped<IUpdateLibrary, UpdateLibraryService>();
builder.Services.AddScoped<IDeleteLibrary, DeleteLibraryService>();

builder.Services.AddDbContext<LibraryDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("LibraryDb"));
});

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
    var dbContext = scope.ServiceProvider.GetRequiredService<LibraryDbContext>();
    if (app.Environment.IsDevelopment())
    {
        // auto database create-drop
        dbContext.Database.EnsureDeleted();
        dbContext.Database.EnsureCreated();
    }
    else
    {
        dbContext.Database.Migrate();
    }
}

app.Run();

// necessary for integration Tests
public partial class Program { }
