using FinalLabAppWebServ.Business;
using FinalLabAppWebServ.Context;
using FinalLabAppWebServ.Middleware;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<CustomerBusiness>();
builder.Services.AddScoped<ProductBusiness>();
builder.Services.AddScoped<OrderBusiness>();
builder.Services.AddScoped<OrderDetailBusiness>();
builder.Services.AddScoped<AuthBusiness>();
builder.Services.AddScoped<RegisterBusiness>();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer("Server=GONZALO;Database=LabAppWebServ;Trusted_Connection=True;TrustServerCertificate=True;"));

builder.Services.AddCors(options =>
{
    options.AddPolicy("PermitirTodo", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});



var app = builder.Build();

app.UseCors("PermitirTodo");

app.UseMiddleware<TokenMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
