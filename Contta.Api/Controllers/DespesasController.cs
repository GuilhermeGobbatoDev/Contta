using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Contta.Services;
using System.Security.Cryptography.X509Certificates;
using Contta.Models;
using Contta.Api.DTOs;

namespace Contta.Api.Controllers
{
     
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

            var despesas = _despesaService.ListarDespesas();
          
                var resposta = ConverterParaDTOs(despesas);

                return Ok(resposta);
        }

        [HttpPost]
        public IActionResult Cadastrar([FromBody] CriarDespesaDTO despesa)
        {
            var novaDespesa = new Despesas
            {
                Descricao = despesa.Descricao,

                Valor = despesa.Valor,

                Categoria = despesa.Categoria,

                Data = despesa.Data

            };
            
            try
            {
                _despesaService.CadastrarDespesa(novaDespesa);
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
            
            var despesa = _despesaService.BuscarDespesaPorId(id);

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
            var excluida = _despesaService.ExcluirDespesa(id);

            if (!excluida)
            {
                return NotFound("Nenhuma despesa encontrada!");
            }

            return NoContent();

        }

        [HttpPut("{id}")]
        public IActionResult Editar(int id, [FromBody] AtualizarDespesaDTO novosDados)
        {
            var dadoAtualizado = new Despesas
            {
                Descricao = novosDados.Descricao,

                Valor = novosDados.Valor,

                Categoria = novosDados.Categoria,

                Data = novosDados.Data,
            };
            try 
            {
                var editado = _despesaService.EditarDespesa(id, dadoAtualizado);
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
            
            
            try
            {
                var despesa = _despesaService.PesquisarPorDescricao(descricao);
                
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
            
            try
            {
                var despesa = _despesaService.FiltrarDespesasPorCategoria(categoria);

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

            try
            {
                var despesa = _despesaService.FiltrarDespesasPorData(mes, ano);

                var resposta = ConverterParaDTOs(despesa);

                return Ok(resposta);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
