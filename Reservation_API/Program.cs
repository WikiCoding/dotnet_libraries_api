using Microsoft.EntityFrameworkCore;
using Reservation_API.Repository;
using Reservation_API.Services;
using Steeltoe.Discovery.Client;
using Steeltoe.Discovery.Consul;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ReservationDbContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("reservations")));

builder.Services.AddScoped<IReservationRepository, ReservationRepositoryImpl>();
builder.Services.AddScoped<GetReservationsService>();

builder.Services.AddHostedService<CreateReservationService>();

builder.Services.AddServiceDiscovery(o => o.UseConsul());

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
    var dbContext = scope.ServiceProvider.GetRequiredService<ReservationDbContext>();
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
