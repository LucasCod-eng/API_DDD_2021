using Aplicacao.Interfaces;
using Entidades.Entidades;
using Entidades.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using System.Text;
using WebAPI.Models;
using WebAPI.Token;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {

        private readonly IAplicacaoUsuario _IAplicacaoUsuario;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        public UsuarioController(IAplicacaoUsuario iAplicacaoUsuario)
        {
            _IAplicacaoUsuario = iAplicacaoUsuario;
        }

        [AllowAnonymous]
        [Produces("application/json")]
        [HttpPost("/api/CriarToken")]

        public async Task<IActionResult> CriarToken([FromBody] Login login)
        {
            if (string.IsNullOrWhiteSpace(login.Email) || string.IsNullOrWhiteSpace(login.Senha))
                return Unauthorized();


            var resultado = await _IAplicacaoUsuario.ExisteUsuario(login.Email, login.Senha);
            if (resultado)
            {
                var token = new TokenJWTBuilder()
                   .AddSecurityKey(JwtSecurityKey.Create("Secret_Key-12345678"))
               .AddSubject("Empresa - Canal Dev Net Core")
               .AddIssuer("Teste.Securiry.Bearer")
               .AddAudience("Teste.Securiry.Bearer")
               .AddClaim("UsuarioAPINumero", "1")
               .AddExpiry(5)
               .Builder();

                return Ok(token.value);
            }
            else
            {
                return Unauthorized();
            }

        }

        [AllowAnonymous]
        [Produces("application/json")]
        [HttpPost("/api/AdicionarUsuario")]
        public async Task<IActionResult> AdicionarUsuario([FromBody] Login login)
        {

            if (string.IsNullOrWhiteSpace(login.Email) || string.IsNullOrWhiteSpace(login.Senha))
                return Ok("Falta alguns dados");
            var resultado = await _IAplicacaoUsuario.AdicionaUsuario(login.Email, login.Senha, login.Idade, login.Celular);
            if (resultado)

                return Ok("Usuário criado com sucesso");
            else
                return Ok("Erro ao adicionar o usuário");

        }


        [AllowAnonymous]
        [Produces("application/json")]
        [HttpPost("/api/CriarTokenIdentity")]
        public async Task<IActionResult> CriarTokenIdentity([FromBody] Login login)
        {
            if (string.IsNullOrWhiteSpace(login.Email) || string.IsNullOrWhiteSpace(login.Senha))
                return Unauthorized();

            var resultado = await _signInManager.PasswordSignInAsync(login.Email, login.Senha, false, lockoutOnFailure: false);

            if (resultado.Succeeded)
            {
                var token = new TokenJWTBuilder()
                     .AddSecurityKey(JwtSecurityKey.Create("Secret_Key-12345678"))
                 .AddSubject("Empresa - Canal Dev Net Core")
                 .AddIssuer("Teste.Securiry.Bearer")
                 .AddAudience("Teste.Securiry.Bearer")
                 .AddClaim("UsuarioAPINumero", "1")
                 .AddExpiry(5)
                 .Builder();

                return Ok(token.value);
            }
            else
            {
                return Unauthorized();
            }

        }

        [AllowAnonymous]
        [Produces("application/json")]
        [HttpPost("/api/AdicionaUsuarioIdentity")]
        public async Task<IActionResult> AdicionaUsuarioIdentity([FromBody] Login login)
        {
            if (string.IsNullOrWhiteSpace(login.Email) || string.IsNullOrWhiteSpace(login.Senha))
                return Ok("Falta alguns dados");

            var user = new ApplicationUser
            {
                UserName = login.Email,
                Email = login.Email,
                Celular = login.Celular,
                Tipo = TipoUsuario.Comum,
            };
            var resultado = await _userManager.CreateAsync(user, login.Senha);

            if (resultado.Errors.Any())
            {
                return Ok(resultado.Errors);
            }

            // Geração de Confirmação caso precise
            var userId = await _userManager.GetUserIdAsync(user);
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

            // retorno email 
            code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
            var resultado2 = await _userManager.ConfirmEmailAsync(user, code);

            if (resultado2.Succeeded)
                return Ok("Usuário Adicionado com Sucesso");
            else
                return Ok("Erro ao confirmar usuários");
        }

    }
}

