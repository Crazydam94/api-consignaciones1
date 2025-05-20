var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<ConsignacionesApi.Services.ConexionBD>();

var app = builder.Build();

app.UseAuthorization();
app.MapControllers();
app.Run();