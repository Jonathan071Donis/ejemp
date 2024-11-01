using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MVCFirstDatabase.Models.ProyectoControlDeParqueos.Models;
using ProyectoControlDeParqueos.Models;

var builder = WebApplication.CreateBuilder(args);


// Configurar el contexto de la base de datos
builder.Services.AddDbContext<LoginDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("loginString")));

// Configurar Identity
builder.Services.AddDefaultIdentity<ApplicationUsers>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<LoginDbContext>()
    .AddDefaultTokenProviders();


builder.Services.AddRazorPages();

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

//configurar el contexto de la base de datos 



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


app.MapRazorPages();

app.Run();
