using Elena.Api.Services;
using Elena.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Adiciona o contexto do banco de dados
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ElenaContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

// Adiciona a autenticação JWT
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],   // Carregar o Issuer
            ValidAudience = builder.Configuration["Jwt:Audience"], // Carregar o Audience
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SecretKey"])) // Carregar a chave secreta
        };
    });

// Registra o TokenService para gerar tokens
builder.Services.AddSingleton<TokenService>();

// Adiciona serviços para controllers
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configuração do Swagger UI
app.UseSwagger();
app.UseSwaggerUI();

// Habilita HTTPS e autenticação
app.UseHttpsRedirection();
app.UseAuthentication();  // Aqui habilitamos a autenticação
app.UseAuthorization();

app.MapControllers();  // Mapeia os controllers

app.Run();
