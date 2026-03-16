using Application.Behaviors;
using Application.Features.Users.Commands.Register;
using Application.Interfaces;
using Application.Services.Impelementation;
using Infrastructure;
using Infrastructure.Helper;
using MediatR;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        sql => sql.MigrationsAssembly("Infrastructure")
    )
);

builder.AddApplicationServices();
builder.Services.AddInfrastructure().AddServiceRegisteration(builder.Configuration);




// Add services to the container.

builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(RegisterUserCommand).Assembly);
});


builder.Services.AddTransient(
    typeof(IPipelineBehavior<,>),
    typeof(ValidationBehavior<,>));
builder.Services.AddControllers().ConfigureApiBehaviorOptions(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});



builder.Services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
//builder.Services.AddOpenApi();
//builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("XChange",
        policy =>
        {
            policy.WithOrigins("http://localhost:4200")
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<ILocalStorageService, LocalStorageService>();

// ✅ تسجيل IPhotoService
builder.Services.AddScoped<IPhotoService, PhotoService>();

// ✅ تسجيل HttpClient (للـ Cloudinary)
builder.Services.AddHttpClient();
var app = builder.Build();




// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "X-Change API v1");
        options.RoutePrefix = string.Empty;
    });
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseCors("XChange");
app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
