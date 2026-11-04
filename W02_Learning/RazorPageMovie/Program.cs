using Microsoft.EntityFrameworkCore;
using RazorPagesMovie.Models;
var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("RazorPagesMovieContext") ?? throw new InvalidOperationException("Connection string 'RazorPagesMovieContext' not found.");

builder.Services.AddDbContext<RazorPagesMovieContext>(options => options.UseSqlite(connectionString));

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddValidation();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    SeedData.Initialize(services);
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseRouting();

// app.UseAuthorization(); // Not used for this app

app.MapStaticAssets();
app.MapRazorPages()
   .WithStaticAssets(); //Tag Helpers can be used for <hash> cache-busting

app.Run();
