using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ocelot.Values;
using PayosferIdentity.Controllers;
using PayosferIdentity.Data;
using PayosferIdentity.Services;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddScoped<ProjectRequestService>();
builder.Services.AddScoped<PasswordResetService>();
builder.Services.AddScoped<EmailService>();


builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseNpgsql("Host=localhost;Port=5432;Database=pmsystem1;Username=postgres;Password=5284;Pooling=true;");
});

builder.Services.AddControllers();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseStaticFiles();
app.UseHttpsRedirection();
app.UseForwardedHeaders();

app.UseCors(builder => builder.WithOrigins("*").AllowAnyHeader().AllowAnyMethod());

app.UseRouting();

app.MapGet("/auth", () => "This endpoint requires authorization")
   .RequireAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
