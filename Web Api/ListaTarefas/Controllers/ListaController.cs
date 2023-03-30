using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiListaTarefas.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ListaTarefas.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ListaController : ControllerBase
    {
        private readonly TarefaContext _context;
        public ListaController(TarefaContext context)
        {
            _context = context;
        }



        [HttpPost]
        public IActionResult InserirTarefa ([FromBody]) tabTarefas request)
        {
        var tarefaService = new TarefaService(_context);
        var sucesso = tarefaService.InserirTarefa(request);
        I
            return sucesso;
        }
    }
}
