using Microsoft.EntityFrameworkCore;
using CSClub.Data;
using CSClub.ADT;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// DB
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// Authentication
builder.Services.AddAuthentication(Constants.ADMIN_COOKIE_NAME)
    .AddCookie(Constants.ADMIN_COOKIE_NAME, options =>
    {
        options.Cookie.Name = Constants.ADMIN_COOKIE_NAME;
    })
    .AddCookie(Constants.TEACHER_COOKIE_NAME, options =>
    {
        options.Cookie.Name = Constants.TEACHER_COOKIE_NAME;
    });

builder.Services.AddRazorPages();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.Run();
