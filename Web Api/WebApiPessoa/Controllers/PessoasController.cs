using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebApiPessoa.Repository;
using WebApiPessoaApplication.Pessoa;
using WebApiPessoaApplication.Usuario;

namespace WebApiPessoa.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PessoasController : ControllerBase
    {
        private readonly PessoaContext _context;
        public PessoasController(PessoaContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Authorize]
        public PessoaResponse ProcessarInformacoesPessoa([FromBody] PessoaRequest request)
        {
            var identidade = (ClaimsIdentity)HttpContext.User.Identity;
            var usuarioId = identidade.FindFirst("usuarioId").Value;

            var pessoaService = new PessoaService(_context);
            var pessoaResponse = pessoaService.ProcessarInformacoesPessoa(request, Convert.ToInt32(usuarioId));

            return pessoaResponse;
        }

        [HttpGet]
        [Authorize]
        public List<PessoaHistoricoResponse> ObterHistoricoPessoas()
        {
            var pessoaService = new PessoaService(_context);
            var pessoas = pessoaService.ObterHistoricoPessoas();

            return pessoas;
        }

        [HttpGet]
        [Authorize]
        [Route("{id}")]
        public PessoaHistoricoResponse ObterHistoricoPessoa ([FromRoute] int id)
        {
            var pessoaService = new PessoaService(_context);
            var pessoa = pessoaService.ObterHistoricoPessoa(id);

            return pessoa;

        }

        [HttpDelete]
        [Route("{id}")]

        public IActionResult RemoverId([FromRoute] int id)
        {
                var pessoaService = new PessoaService(_context);
                var sucesso = pessoaService.RemoverId(id);

                if (sucesso == true)
                {
                    return NoContent();
                }
                else
                {
                    return BadRequest();
                }     
        }
    }
}
