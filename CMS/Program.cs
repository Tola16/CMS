using CMS.AppContext;
using CMS.Repos.Implementation;
using CMS.Repos.Interface;
using Microsoft.EntityFrameworkCore;
using CMS.Models;
using CMS.IUnitOfWorks.Implementations;
using CMS.IUnitOfWorks.Interface;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("con")));

builder.Services.AddScoped(typeof(IGenericRepo<>), typeof(GenericRepo<>));

builder.Services.AddScoped<IGenreRepo, GenreRepo>();
builder.Services.AddScoped<IMovieRepo, MovieRepo>();
builder.Services.AddScoped<ICustomerRepo, CustomerRepo>();
builder.Services.AddScoped<IBookingRepo, BookingRepo>();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
