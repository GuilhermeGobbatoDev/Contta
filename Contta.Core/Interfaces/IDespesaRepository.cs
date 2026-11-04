using Contta.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contta.Core.Interfaces
{
    public interface IDespesaRepository
    {
        void Adicionar(Despesas despesa);

        List<Despesas> Listar();

        Despesas? BuscarPorId(int id);

        void Atualizar(Despesas despesa);

        void Deletar(Despesas despesas);

        List<Despesas> PesquisarPorDescricao(string descricao);

        List<Despesas> PesquisarPorCategoria(string categoria);

        List<Despesas> PesquisarPorData(int mes, int ano);

        List<Despesas> PesquisarPorValor(decimal valor);

        decimal CalcularTotal();
    }
}
