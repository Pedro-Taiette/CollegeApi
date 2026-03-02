using College.Application.Configuration;
using College.Infrastructure.Configuration;

var builder = WebApplication.CreateBuilder(args);

// --- 1. Registros dos layers ---
builder.Services.AddApplication();
builder.Services.AddInfrastructure();

// --- 2. Configurações da api ---
builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

// --- 3. Pipeline ---
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "College API v1");
        options.RoutePrefix = string.Empty;
    });
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();