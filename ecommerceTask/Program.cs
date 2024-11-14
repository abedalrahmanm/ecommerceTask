using ecommerceTask.Models;
using ecommerceTask.Models.Repositories;
using ecommerceTask.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IRepository<User>,UserRepository>();
builder.Services.AddScoped<IRepository<Product>,ProductRepository>();
builder.Services.AddScoped<IRepository<Category>,CategoryRepository>();
builder.Services.AddScoped<IRepository<Cart>,CartRepository>();
builder.Services.AddScoped<IRepository<CartItem>,CartItemRepository>();
builder.Services.AddScoped<IRepository<Review>,ReviewRepository>();
builder.Services.AddScoped<IRepository<Order>,OrderRepository>();

builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionString")));


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
