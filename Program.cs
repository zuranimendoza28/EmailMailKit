using Email.Data;
using Microsoft.EntityFrameworkCore;
using Email.Models;
using Email.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddTransient<EmailRepository>();


//Add services to connect at the database
builder.Services.AddDbContext<EmailContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"), Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.20-mysql")));

    
builder.Services.Configure<EmailModel>(builder.Configuration.GetSection("EmailSettings"));



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
app.UseAuthentication();


/* Autenticación */
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Email}/{action=Index}");
app.Run();
