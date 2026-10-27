using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Contta.Services;
using System.Security.Cryptography.X509Certificates;
using Contta.Models;

namespace Contta.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DespesasController : ControllerBase
    {
        private readonly DespesaService _despesaService;

        public DespesasController(DespesaService servico)
        {
            _despesaService = servico;
            
        }

        [HttpGet]
        public IActionResult Listar()
        {

            var despesas = _despesaService.ListarDespesas();

            return Ok(despesas);

        }

        [HttpPost]
        public IActionResult Cadastrar([FromBody] Despesas despesa)
        {
           
            _despesaService.CadastrarDespesa(despesa);

            return Ok(despesa);
        }

        [HttpGet("{id}")]
        public IActionResult Buscar(int id)
        {
            var despesa = _despesaService.BuscarDespesaPorId(id);

            if (despesa == null)
            {
                return NotFound("Despesa não encontrada!");
            }

            return Ok(despesa);
        }
    }
}
