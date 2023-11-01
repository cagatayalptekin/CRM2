using Application.Contracts.Departmans;
using Application.Contracts.Personels;
using Application.Contracts.Roles;
using Application.Contracts.Tasks;
using Application.Departmans;
using Application.Personels;
using Application.Roles;
using Application.Tasks;
using AutoMapper;
using Domain;
using Domain.Departmans;
using Domain.Personels;
using Domain.Roles;
using Domain.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddMemoryCache();
builder.Services.AddDbContext<CompanyContext>(options =>
{
    options.UseSqlServer("");
});

builder.Services.AddTransient<IDepartmanRepository, EfDepartmanRepository>();
builder.Services.AddTransient<IRoleRepository, EfRoleRepository>();
builder.Services.AddTransient<ITaskRepository, EfTaskRepository>();
builder.Services.AddTransient<IPersonelRepository, EfPersonelRepository>();
builder.Services.AddTransient<IDepartmanService, DepartmanService>();
builder.Services.AddTransient<IRolesService, RoleService>();
builder.Services.AddTransient<IPersonelService, PersonelService>();
builder.Services.AddTransient<ITaskService, TaskService>();

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
