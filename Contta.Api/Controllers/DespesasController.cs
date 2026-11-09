using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Contta.Services;
using System.Security.Cryptography.X509Certificates;
using Contta.Models;
using Contta.Api.DTOs;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace Contta.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class DespesasController : ControllerBase
    {
        private static List<DespesaRespostaDTO> ConverterParaDTOs(IEnumerable<Despesas> despesa)
        {
            return despesa.Select(despesa => new DespesaRespostaDTO
            {
                Descricao = despesa.Descricao,
                Valor = despesa.Valor,
                Categoria = despesa.Categoria,
                Data = despesa.Data,
                Id = despesa.Id

            }).ToList();
        }

        private readonly DespesaService _despesaService;

        public DespesasController(DespesaService servico)
        {
            _despesaService = servico;

        }

        [HttpGet]
        public IActionResult Listar()
        {
            var usuarioId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrWhiteSpace(usuarioId))
            {
                return Unauthorized();
            }

            var despesas = _despesaService.ListarDespesas(usuarioId);
          
            var resposta = ConverterParaDTOs(despesas);

                return Ok(resposta);
        }

        [HttpPost]
        public IActionResult Cadastrar([FromBody] CriarDespesaDTO despesa)
        {
            var usuarioId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrWhiteSpace(usuarioId))
            {
                return Unauthorized();
            }
            var novaDespesa = new Despesas
            {
                Descricao = despesa.Descricao,

                Valor = despesa.Valor,

                Categoria = despesa.Categoria,

                Data = despesa.Data

            };
            
            try
            {
                _despesaService.CadastrarDespesa(novaDespesa, usuarioId);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            
            return CreatedAtAction(nameof(Buscar), new {id = novaDespesa.Id}, despesa);
        }

        [HttpGet("{id}")]
        public IActionResult Buscar(int id)
        {
            var usuarioId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrWhiteSpace(usuarioId))
            {
                return Unauthorized();
            }
            
            var despesa = _despesaService.BuscarDespesaPorId(id, usuarioId);

            if (despesa == null)
            {
                return NotFound("Despesa não encontrada!");
            }

            var resposta = new DespesaRespostaDTO
            {
                Descricao = despesa.Descricao,

                Valor = despesa.Valor,

                Categoria = despesa.Categoria,

                Data = despesa.Data,

                Id = despesa.Id
            };

            return Ok(resposta);
        }

        [HttpDelete("{id}")]
        public IActionResult Excluir(int id)
        {
            var usuarioId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            
            if (string.IsNullOrWhiteSpace(usuarioId))
            {
                return Unauthorized();
            }
            var excluida = _despesaService.ExcluirDespesa(id, usuarioId);

            if (!excluida)
            {
                return NotFound("Nenhuma despesa encontrada!");
            }

            return NoContent();

        }

        [HttpPut("{id}")]
        public IActionResult Editar(int id, [FromBody] AtualizarDespesaDTO novosDados)
        {
            var usuarioId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            
            if (string.IsNullOrWhiteSpace(usuarioId))
            {
                return Unauthorized();
            }

            var dadoAtualizado = new Despesas
            {
                Descricao = novosDados.Descricao,

                Valor = novosDados.Valor,

                Categoria = novosDados.Categoria,

                Data = novosDados.Data,
            };
            try 
            {
                var editado = _despesaService.EditarDespesa(id, dadoAtualizado, usuarioId);
                if (!editado)
            {
                return NotFound("Nenhuma despesa encontrada!");
            }
            
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
          
            }

            
            return NoContent();
        }

        [HttpGet("pesquisar")]
        public IActionResult PesquisarPorDescricao([FromQuery] string descricao)
        {
            var usuarioId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrWhiteSpace(usuarioId))
            {
                return Unauthorized();
            }

            try
            {
                var despesa = _despesaService.PesquisarPorDescricao(descricao, usuarioId);
                
                var resposta = ConverterParaDTOs(despesa);

                return Ok(resposta);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
          
        }

        [HttpGet("categoria")]
        public IActionResult PesquisarPorCategoria([FromQuery] string categoria)
        {
            var usuarioId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrWhiteSpace(usuarioId))
            {
                return Unauthorized();
            }

            try
            {
                var despesa = _despesaService.FiltrarDespesasPorCategoria(categoria, usuarioId);

                var resposta = ConverterParaDTOs(despesa);

                return Ok(resposta);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("data")]
        public IActionResult PesquisarPorData([FromQuery] int mes, [FromQuery] int ano)
        {
            var usuarioId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrWhiteSpace(usuarioId))
            {
                return Unauthorized();
            }

            try
            {
                var despesa = _despesaService.FiltrarDespesasPorData(mes, ano, usuarioId);

                var resposta = ConverterParaDTOs(despesa);

                return Ok(resposta);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("Total")]
        public IActionResult CalcularTotalDespesas()
        {
            var usuarioId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrWhiteSpace(usuarioId))
            {
                return Unauthorized();
            }

            var totalCalculado = _despesaService.CalcularTotal(usuarioId);
            
            return Ok(totalCalculado);   
            
        }

        [HttpGet("Despesa-Maior")]
        public IActionResult BuscarDespesasAcimaDe(decimal valor)
        {
            var usuarioId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrWhiteSpace(usuarioId))
            {
                return Unauthorized();
            }

            try 
            {

                var despesaAcimaDeEncontrada = _despesaService.PesquisarPorValor(valor, usuarioId);
                var resposta = ConverterParaDTOs(despesaAcimaDeEncontrada);

                return Ok(resposta);
              
            }
            catch (ArgumentException ex)
            {
                   return BadRequest(ex.Message);
            }
        }
    }
}
