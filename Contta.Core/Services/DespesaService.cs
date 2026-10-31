using Contta.Models;
using Contta.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Xml.Linq;


namespace Contta.Services
{
    public class DespesaService
    {
        private readonly IDespesaRepository _repository;

        public DespesaService(IDespesaRepository repository)
        {
            _repository = repository;
        }
        
        public void CadastrarDespesa(Despesas despesa)
        {

            // Verifica se o objeto existe
            if (despesa == null)
            {
                throw new ArgumentNullException(nameof(despesa));
            }

            // Validação da descrição
            if (string.IsNullOrWhiteSpace(despesa.Descricao))
            {
                throw new ArgumentException("A descrição é obrigatória.");
            }

            // Validação do valor
            if (despesa.Valor <= 0)
            {
                throw new ArgumentException("O valor deve ser maior que zero.");
            }

            // Validação da categoria
            if (string.IsNullOrWhiteSpace(despesa.Categoria))
            {
                throw new ArgumentException("A categoria é obrigatória.");
            }

            // Validação da data
            if (despesa.Data == default(DateTime))
            {
                throw new ArgumentException("A data é obrigatória.");
            }

            //Insere a despesa no BD
            _repository.Adicionar(despesa);

        }
        public List<Despesas> ListarDespesas()
        {
           return _repository.Listar();
        }

        public decimal CalcularTotal()
        {
            //Calcula o total direto do banco
            return _repository.CalcularTotal();
        }

        public Despesas? BuscarDespesaPorId(int id)
        {
            return _repository.BuscarPorId(id);

        }

        public bool EditarDespesa(int id, Despesas novosDados)
        {
            Despesas? despesaEncontrada = BuscarDespesaPorId(id);

            if (despesaEncontrada == null)
            {
                return false;
            }

            if (novosDados.Valor <= 0)
            {
                throw new ArgumentException("Valor inválido!");
            }

            if (string.IsNullOrWhiteSpace(novosDados.Descricao))
            {
                throw new ArgumentException("A descrição é obrigatória.");
            }

            if (string.IsNullOrWhiteSpace(novosDados.Categoria))
            {
                throw new ArgumentException("A categoria é obrigatória.");
            }

            if (novosDados.Data == default(DateTime))
            {
                throw new ArgumentException("A data é obrigatória.");
            }

            despesaEncontrada.Descricao = novosDados.Descricao;

            despesaEncontrada.Categoria = novosDados.Categoria;

            despesaEncontrada.Data = novosDados.Data;

            despesaEncontrada.Valor = novosDados.Valor;

            _repository.Atualizar(despesaEncontrada);

            return true;
        }

        public bool ExcluirDespesa(int id)
        {
            Despesas? encontradaExclusao = BuscarDespesaPorId(id);

            if (encontradaExclusao == null)
            {
                return false;
            }

            _repository.Deletar(encontradaExclusao);

            return true;

        }

        public List<Despesas> PesquisarPorDescricao(string descricaoPesquisada)
        {
            if (string.IsNullOrWhiteSpace(descricaoPesquisada))
            {
                throw new ArgumentException("Por favor, digite o nome da despesa!");
            }

            return _repository.PesquisarPorDescricao(descricaoPesquisada);
        }

        public List<Despesas> FiltrarDespesasPorCategoria(string categoriaPesquisada)
        {
            if (string.IsNullOrWhiteSpace(categoriaPesquisada))
            {
                throw new ArgumentException("Por favor, digite uma categoria!");
            }

            return _repository.PesquisarPorCategoria(categoriaPesquisada);
        }

        public List<Despesas> FiltrarDespesasPorData(int mesPesquisado, int anoPesquisado)
        {
            if (mesPesquisado < 1 || mesPesquisado > 12 )
            {
                throw new ArgumentException("Digite um mês valido!");
            }

            if (anoPesquisado < 1)
            {
                throw new ArgumentException("Digite um ano válido!");
            }

            return _repository.PesquisarPorData(mesPesquisado, anoPesquisado);
        }

        public List<Despesas> PesquisarPorValor(decimal valor)
        {
            if (valor <= 0)
            {
                throw new ArgumentException("Digite um valor maior que 0");
            }

            return _repository.PesquisarPorValor(valor);
        }
    }
}