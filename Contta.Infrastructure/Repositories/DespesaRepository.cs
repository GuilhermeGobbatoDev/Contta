using Contta.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.Marshalling;
using System.Text;
using Contta.Models;
using Contta.Infrastructure.Data;
using System.Runtime.CompilerServices;

namespace Contta.Infrastructure.Repositories
{
    public class DespesaRepository : IDespesaRepository
    {
      private readonly ConttaDbContext _context;

        public DespesaRepository(ConttaDbContext context)
        {
            _context = context;
        }
        public void Adicionar(Despesas despesas)
        {
            _context.Despesas.Add(despesas);
           
            _context.SaveChanges();
        }

        public List<Despesas> Listar()
        {
            return _context.Despesas.ToList();
        }

        public Despesas? BuscarPorId(int id)
        {
            return _context.Despesas.FirstOrDefault(despesa => despesa.Id == id);
        }

        public void Atualizar(Despesas despesas)
        {
            _context.SaveChanges();
        }

        public void Deletar(Despesas despesa)
        {
            _context.Despesas.Remove(despesa);

            _context.SaveChanges();
        }
        public List<Despesas> PesquisarPorDescricao(string descricao)
        {
            return _context.Despesas.Where(despesa => despesa.Descricao.ToLower().Trim() == descricao.ToLower().Trim()).ToList();

        }

        public List<Despesas> PesquisarPorCategoria(string categoria)
        {
            return _context.Despesas.Where(despesa => despesa.Categoria.ToLower().Trim().Contains(categoria.ToLower().Trim())).ToList();
        }

        public List<Despesas> PesquisarPorData(int mes, int ano)
        {
            return _context.Despesas.Where(data => data.Data.Month == mes && data.Data.Year == ano).ToList();
        }

        public decimal CalcularTotal()
        {
            return _context.Despesas.Sum(despesa => despesa.Valor);
        }
    }
}
