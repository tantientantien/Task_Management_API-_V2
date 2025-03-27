using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using Serilog;
using TaskManagement.Api.Endpoints;
using TaskManagement.Api.Extensions;
using TaskManagement.Api.Mappings;
using TaskManagement.Api.Service;

var builder = WebApplication.CreateBuilder(args);

// Configure Serilog
Log.Logger = new LoggerConfiguration()
    .WriteTo.File("Logs/log.txt", rollingInterval: RollingInterval.Day)
    .MinimumLevel.Information()
    .CreateLogger();

builder.Host.UseSerilog();

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddSingleton(TimeProvider.System);
builder.Services.AddOpenApi();
builder.Services.AddAuthentication();
builder.Services.AddAuthorization();

builder.Services.AddDbContext<TaskManagementContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnectionString")
    )
);

builder.Services.AddIdentityApiEndpoints<User>()
    .AddRoles<IdentityRole<int>>()
    .AddEntityFrameworkStores<TaskManagementContext>()
    .AddDefaultTokenProviders();

// builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<GetCurrentUser>();
builder.Services.AddScoped<UserPermission>();

var app = builder.Build();

app.UseGlobalExceptionHandler();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapScalarApiReference();
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();



var userGroup = app.MapGroup("/api/users").WithTags("User");
userGroup.MapUserEndpoints().RequireAuthorization();
userGroup.MapIdentityApi<User>().AllowAnonymous();



app.MapGroup("/api/tasks").MapTaskItemEndpoints().RequireAuthorization().WithTags("Task");
app.MapGroup("/api/categories").MapCategoryEndpoints().RequireAuthorization().WithTags("Category");
app.MapGroup("/api/labels").MapLabelEndpoints().RequireAuthorization().WithTags("Label");
app.MapGroup("/api/tasklabels").MapTaskLabelEndpoints().RequireAuthorization().WithTags("Task Label");
app.MapGroup("/api/comments").MapTaskCommentEndpoints().RequireAuthorization().WithTags("Task Comment");




app.Run();