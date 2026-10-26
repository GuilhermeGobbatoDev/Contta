using Contta.Models;
using System;
using System.Collections.Generic;

namespace Contta.Services
{
    public class DespesaService
    {
        private readonly List<Despesas> _despesas = new();
        private int _proximoId = 1;

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

            //Adiciona 1 ao id
            despesa.Id = _proximoId++;

            // Adiciona a despesa à lista
            _despesas.Add(despesa);
        }
        public List<Despesas> ListarDespesas()
        {
            return new List <Despesas>(_despesas);
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

    }
}