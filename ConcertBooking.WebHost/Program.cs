using ConcertBooking.Repositories.DataAccess;
using ConcertBooking.Repositories.Implementions;
using ConcertBooking.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using ConcertBooking.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews()
    .AddJsonOptions(options => options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
//DI - Tightly coulpled -> Loosely Coupled
// ICity cityRepo = new CityRepo();
builder.Services.AddScoped<IdbInitial, DbInitial>();
builder.Services.AddScoped<IVenue, VenueRepo>();
builder.Services.AddScoped<IConcert, ConcertRepo>();
builder.Services.AddScoped<IArtist, ArtistRepo>();
builder.Services.AddScoped<ITicket, TicketRepo>();
builder.Services.AddScoped<IBooking, BookingRepo>();
builder.Services.AddScoped<IUtility, UtilityRepo>();
builder.Services.AddScoped<IEmailSender, EmailSender>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies()); // Auto mapper configuration
builder.Services.AddDbContext<ApplicationDBContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly("ConcertBooking.WebHost"));
    options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
});

//AddDefaultIdentity
//AddIdentity
//AddIdentityCore

builder.Services.AddIdentity<ApplicationUser, IdentityRole>(/*options => options.SignIn.RequireConfirmedAccount = true*/)
    .AddDefaultTokenProviders()
    .AddEntityFrameworkStores<ApplicationDBContext>();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Identity/Account/Login";
    options.LogoutPath = "/Identity/Account/Logout";
    options.AccessDeniedPath = "/Identity/Account/AccessDenied";
});


builder.Services.AddRazorPages();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

DataSeeding();

void DataSeeding()
{
    using (var scope = app.Services.CreateScope())
    {
        var _dbRepo = scope.ServiceProvider.GetRequiredService<IdbInitial>();
        _dbRepo.Seed();
    }
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();;

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=CountSummary}/{id?}");

app.MapRazorPages();
app.Run();
