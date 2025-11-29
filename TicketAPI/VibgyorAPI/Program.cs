using DataCore;
using DataServices;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddScoped<TicketService>();
builder.Services.AddDbContext<TicketContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
                         b => b.MigrationsAssembly("TicketAPI")));
builder.Services.AddControllers();

// CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

// Swagger generator (no SerializeAsV2 here)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Middleware pipeline
if (app.Environment.IsDevelopment())
{
    // Force Swagger 2.0 output here
    app.UseSwagger(c =>
    {
        c.SerializeAsV2 = true; // ✅ Only works here, NOT in AddSwaggerGen
    });

    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Ticket API V1");
    });
}

app.UseHttpsRedirection();

// CORS must come BEFORE UseAuthorization and MapControllers
app.UseCors("AllowAll");

app.UseAuthorization();
app.MapControllers();

app.Run();
