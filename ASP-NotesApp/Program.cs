using ASP_NotesApp.DAL;
using ASP_NotesApp.DAL.Repository;
using ASP_NotesApp.Models.Domain;
using ASP_NotesApp.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
string connection = builder.Configuration.GetConnectionString("NoteAppDBContext");
builder.Services.AddDbContext<NoteAppDBContext>(options => options.UseSqlServer(connection));
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme,
        options =>
        {
            options.LoginPath = new PathString("/User/Login");
            options.LogoutPath = new PathString("/User/Logout");
            options.AccessDeniedPath = new PathString("/Error");

        });
builder.Services.AddScoped<IGenericRepository<Note>, NotesRepository>();
builder.Services.AddScoped<IGenericRepository<User>, UserRepository>();
builder.Services.AddTransient<ClaimsPrincipal>(s =>s.GetService<IHttpContextAccessor>().HttpContext.User);
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddRazorPages().AddMvcOptions(option =>
{
    option.ModelBindingMessageProvider.SetValueMustNotBeNullAccessor(
        _ => "Ïîëå äîëæíî áûòü çàïîëíåíî");
});
builder.Services.AddScoped<UserManagerService>();
builder.Services.AddScoped<NoteManagerService>();


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
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Note}/{action=Index}/{id?}");

app.Run();
