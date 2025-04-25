using Elena.Application.DTOs;
using Elena.Api.Services;  // Usando o TokenService
using Elena.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Elena.Infrastructure.Data;

namespace Elena.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ElenaContext _context;
        private readonly TokenService _tokenService;

        // Injeção de dependências para o AuthController
        public AuthController(IConfiguration configuration, ElenaContext context, TokenService tokenService)
        {
            _configuration = configuration;
            _context = context;
            _tokenService = tokenService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDto login)
        {
            if (string.IsNullOrEmpty(login.Username) || string.IsNullOrEmpty(login.Password))
            {
                return BadRequest("Username ou senha não podem ser vazios.");
            }

            var usuario = await _context.Usuarios
                .Include(u => u.Pessoa)  // Certifique-se de carregar a Pessoa
                .FirstOrDefaultAsync(u => u.Username == login.Username);

            if (usuario == null)
            {
                return Unauthorized("Credenciais inválidas");
            }

            // Aqui você pode validar a senha com uma comparação segura
            if (usuario.Password != login.Password)
            {
                return Unauthorized("Senha incorreta");
            }

            // Gerar o token JWT
            var token = _tokenService.GenerateToken(usuario.Username, usuario.Pessoa.Email, usuario.Tipo.ToString());

            return Ok(new { Token = token });
        }




    }
}
