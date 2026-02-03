using Application.Behaviors;
using Application.Features.Users.Commands.Register;
using Domain.Entities.System;
using Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        sql => sql.MigrationsAssembly("Infrastructure")
    )
);

builder.AddApplicationServices();



builder.Services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
{
    options.Password.RequireUppercase = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireDigit = false;
    options.Password.RequireNonAlphanumeric = false;

})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();
// Add services to the container.

builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(RegisterUserCommand).Assembly);
});
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
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

//app.Use(async (context, next) =>
//{
//    try
//    {
//        await next();
//    }
//    catch (FluentValidation.ValidationException ex)
//    {
//        context.Response.StatusCode = 400;
//        context.Response.ContentType = "application/json";

//        var errors = ex.Errors
//            .GroupBy(e => e.PropertyName)
//            .ToDictionary(
//                g => g.Key,
//                g => g.Select(e => e.ErrorMessage).ToArray()
//            );

//        var response = new
//        {
//            status = 400,
//            message = "Validation failed",
//            errors = errors
//        };

//        await context.Response.WriteAsJsonAsync(response);
//    }
//});


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/openapi/v1.json", "api");
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
