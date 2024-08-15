using Barbearia.APi;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Adiciona os controladores
builder.Services.AddControllers();

// Configura o Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configura o banco de dados
string sqlServer = builder.Configuration.GetConnectionString("ApiConnection");
builder.Services.AddDbContext<AplicationDbContext>(options =>
    options.UseSqlServer(sqlServer)
);

var app = builder.Build();

// Configura o pipeline de solicitação HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Configura o roteamento
app.UseRouting();

// Mapeia os controladores
app.MapControllers();

app.Run();
