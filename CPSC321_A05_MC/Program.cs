using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using CPSC321_A05_MC.Data;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<CPSC321_A05_MCContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("CPSC321_A05_MCContext") ?? throw new InvalidOperationException("Connection string 'CPSC321_A05_MCContext' not found.")));

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
