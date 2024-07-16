using FluentValidation;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using PresentationCreatorAPI.Application.Common.Validators;
using PresentationCreatorAPI.Application.Interfaces;
using PresentationCreatorAPI.Application.Services;
using PresentationCreatorAPI.Data;
using PresentationCreatorAPI.Data.Interfaces;
using PresentationCreatorAPI.Domain.Entites;
using PresentationCreatorAPI.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(100000);
});

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("LocalDB")));

//builder.Services.AddAuthentication(options =>
//{
//    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
//})
//.AddJwtBearer(options =>
//{
//    options.TokenValidationParameters = new TokenValidationParameters
//    {
//        ValidateIssuer = true,
//        ValidateAudience = true,
//        ValidateLifetime = true,
//        ValidateIssuerSigningKey = true,
//        ValidIssuer = "https://PresentationCreator.API", // issuer
//        ValidAudience = "Presentation", // audience
//        IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("www.google.com/search?q=guid+generator&oq=guid+&gs_lcrp=EgZjaHJvbWUqBggBEEUYOzIGCAAQRRg5MgYIARBFGDsyBwgCEAAYjwIyBwgDEAAYjwIyBwgEEAAYjwIyBggFEEUYPNIBCDIxMDVqMGo0qAIAsAIB&sourceid=chrome&ie=UTF-8"))
//    };
//});

// Add Authentication and configure the cookie scheme
builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = "Admin";
})
.AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
{
    options.LoginPath = "/account/login";
    options.ExpireTimeSpan = TimeSpan.FromMinutes(5);
})
.AddCookie("Admin", options =>
{
    options.LoginPath = "/admin/account/login";
    options.ExpireTimeSpan = TimeSpan.FromMinutes(5);
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminPolicy", policy =>
        policy.RequireClaim("Admin", "true"));
});

builder.Services.AddHttpClient();

// Unit Of Work
builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();

builder.Services.AddTransient<IAccountService, AccountService>();
builder.Services.AddTransient<IAuthManager, AuthManager>();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IEmailService, EmailService>();
builder.Services.AddTransient<IAdminService, AdminService>();
builder.Services.AddTransient<INotificationService, NotificationService>();
builder.Services.AddTransient<IPaymentService, PaymentServicce>();
builder.Services.AddTransient<IPresentationServise, PresentationServise>();
builder.Services.AddTransient<IPageService, PageService>();
builder.Services.AddTransient<IRedisService, RedisService>();

//Validator
builder.Services.AddScoped<IValidator<User>, UserValidator>();
builder.Services.AddScoped<IValidator<PresentationCreatorAPI.Domain.Entites.Presentation>, PresentationValidator>();
builder.Services.AddScoped<IValidator<Notification>, NotificationValidator>();
builder.Services.AddScoped<IValidator<Payment>, PaymentValidator>();
builder.Services.AddScoped<IValidator<Page>, PageValidator>();

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseSession();

app.UseAuthentication(); // Ensure this is added before UseAuthorization
app.UseAuthorization();

// Define the routes
app.MapControllerRoute(
    name: "admin",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
