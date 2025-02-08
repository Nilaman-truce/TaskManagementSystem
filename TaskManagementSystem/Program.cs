
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Data.SqlClient;
using System.Data;
using Dapper;
using TaskManagementSystem.Repositories;
using QuestPDF.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

QuestPDF.Settings.License = LicenseType.Community;
// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<ITaskRepository, TaskRepository>();

builder.Services.AddScoped<IDbConnection>(provider =>
    new SqlConnection(builder.Configuration.GetConnectionString("DefaultConnection")));


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
    pattern: "{controller=Task}/{action=Index}/{id?}");

app.Run();
