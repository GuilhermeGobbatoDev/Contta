using Contta.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contta.Core.Interfaces
{
    public interface IDespesaRepository
    {
        void Adicionar(Despesas despesa);

        List<Despesas> Listar(string usuarioId);

        Despesas? BuscarPorId(int id, string usuarioId);

        void Atualizar(Despesas despesa);

        void Deletar(Despesas despesas);

        List<Despesas> PesquisarPorDescricao(string descricao, string usuarioId);

        List<Despesas> PesquisarPorCategoria(string categoria, string usuarioId);

        List<Despesas> PesquisarPorData(int mes, int ano, string usuarioId);

        List<Despesas> PesquisarPorValor(decimal valor, string usuarioId);

        decimal CalcularTotal(string usuarioId);

        

    }
}
