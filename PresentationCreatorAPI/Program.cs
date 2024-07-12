using PresentationCreatorAPI.Application.Interfaces;
using PresentationCreatorAPI.Application.Services;
using Serilog;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Cache
builder.Services.AddMemoryCache();

//Serilog
builder.Host.UseSerilog((hostingContext, loggerConfiguration) =>
loggerConfiguration.ReadFrom.Configuration(hostingContext.Configuration));

//Redis
builder.Services.Configure<ConfigurationOptions>(
                builder.Configuration.GetSection("RedisCacheOptions"));

builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetConnectionString("RedisCacheConnectionString");
    options.InstanceName = "UsersAPI";
});

//DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("LocalDb"));
    options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
});

builder.Services.AddHttpClient();

// Unit Of Work
builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();

// Service
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

// Configure
builder.Services.ConfigureJwtAuthorize(builder.Configuration);
builder.Services.ConfigureSwaggerAuthorize(builder.Configuration);

//Validator
builder.Services.AddScoped<IValidator<User>, UserValidator>();
builder.Services.AddScoped<IValidator<Presentation>, PresentationValidator>();
builder.Services.AddScoped<IValidator<Notification>, NotificationValidator>();
builder.Services.AddScoped<IValidator<Payment>, PaymentValidator>();
builder.Services.AddScoped<IValidator<Page>, PageValidator>();

// cors policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyHeader()
                   .AllowAnyMethod();
        });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStaticFiles();

app.UseHttpsRedirection();

app.UseCors("All");

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.UseMiddleware<ExceptionHandleMiddleware>();


app.Run();