using Microsoft.EntityFrameworkCore;
using net6webapi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDBContext>(
                o => o.UseNpgsql(builder.Configuration.GetConnectionString("AWSTestDatabase"),
                o => o.UseNodaTime())
            );

builder.Services.AddTransient<net6webapi.Repositories.IAppointmentsStatusRepository, net6webapi.Repositories.AppointmentsStatusRepository>();
builder.Services.AddTransient<net6webapi.Repositories.IAppointmentsRepository, net6webapi.Repositories.AppointmentsRepository>();
builder.Services.AddTransient<net6webapi.Repositories.IUsersRepository, net6webapi.Repositories.UsersRepository>();
builder.Services.AddTransient<net6webapi.Repositories.IPacientsRepository, net6webapi.Repositories.PacientsRepository>();

//builder.Services.AddDatabaseDeveloperPageExceptionFilter();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var context = services.GetRequiredService<AppDBContext>();
    context.Database.EnsureCreated();
    //if (app.Environment.IsDevelopment()) { DbInitializer.Initialize(context); }
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
