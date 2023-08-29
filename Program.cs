using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using E_Commerce.Data;


var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddHttpContextAccessor();
builder.Services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
string connectionstring = builder.Configuration.GetConnectionString("AppDbContext");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionstring ?? throw new InvalidOperationException("Connection string 'AppDbContext' not found.")));
builder.Services.AddSession(m => m.IdleTimeout = TimeSpan.FromMinutes(20));
// Add services to the container.


// Add services to the container.
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();
builder.Services.AddDistributedSqlServerCache(m =>
{
    m.ConnectionString = connectionstring;
    m.SchemaName = "dbo";
    m.TableName = "SessionData";
});
//scope
//singleton
//transient
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

using var scope = app.Services.CreateScope();
var _context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    _context.Database.Migrate();
app.UseSession();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Client}/{id?}");

app.Run();
