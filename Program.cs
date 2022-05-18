using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SD_125_BugTracker.DAL;
using SD_125_BugTracker.Data;
using SD_125_BugTracker.Models;

var builder = WebApplication.CreateBuilder(args);

// Add repository singleton
builder.Services.AddScoped<IRepository<Project>, ProjectRepository>();
builder.Services.AddScoped<IRepository<ProjectUser>, ProjectUserRepository>();
builder.Services.AddScoped<IRepository<Ticket>, TicketRepository>();
builder.Services.AddScoped<IRepository<TicketAttachment>, TicketAttachmentRepository>();
builder.Services.AddScoped<IRepository<TicketComment>, TicketCommentRepository>();
builder.Services.AddScoped<IRepository<TicketHistory>, TicketHistoryRepository>();
builder.Services.AddScoped<IRepository<TicketNotification>, TicketNotificationRepository>();
builder.Services.AddScoped<IUserRepository<ApplicationUser>, UserRepository>();

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();

var app = builder.Build();

using ( var scope = app.Services.CreateScope() )
{
    var services = scope.ServiceProvider;
    await SeedData.Initialize(services);
}
// Configure the HTTP request pipeline.
if ( app.Environment.IsDevelopment() )
{
    app.UseMigrationsEndPoint();
}
else
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
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
