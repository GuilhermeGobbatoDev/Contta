using Contta.Models;
using Contta.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;

namespace Contta.Services
{
    public class DespesaService
    {
        private readonly IDespesaRepository _repository;

        public DespesaService(IDespesaRepository repository)
        {
            _repository = repository;
        }
        
        private readonly List<Despesas> _despesas = new();

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

        public decimal CalcularTotalDespesas()
        {
            //Percorre a lista e soma os valores
            return _despesas.Sum(d => d.Valor);
        }

        public Despesas? BuscarDespesaPorId(int id)
        {
           return _despesas.FirstOrDefault(despesa => despesa.Id == id);

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

            return true;
        }

        public bool ExcluirDespesa(int id)
        {
            Despesas? encontradaExclusao = BuscarDespesaPorId(id);

            if (encontradaExclusao == null)
            {
                return false;
            }

            return _despesas.Remove(encontradaExclusao);

        }

        public List<Despesas> PesquisarPorDescricao(string descricaoPesquisada)
        {
            if (string.IsNullOrWhiteSpace(descricaoPesquisada))
            {
                throw new ArgumentException("Por favor, digite o nome da despesa!");
            }

            return _despesas.Where(despesa => despesa.Descricao.ToLower().Trim() == descricaoPesquisada.Trim().ToLower()).ToList();
        }

        public List<Despesas> FiltrarDespesasPorCategoria(string categoriaPesquisada)
        {
            if (string.IsNullOrWhiteSpace(categoriaPesquisada))
            {
                throw new ArgumentException("Por favor, digite uma categoria!");
            }

            return _despesas.Where(despesa => despesa.Categoria.ToLower().Trim().Contains(categoriaPesquisada.ToLower().Trim())).ToList();
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

            return _despesas.Where(data => data.Data.Month == mesPesquisado && data.Data.Year == anoPesquisado).ToList();
        }
    }
}