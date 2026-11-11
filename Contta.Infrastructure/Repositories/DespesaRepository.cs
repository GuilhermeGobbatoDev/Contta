using Contta.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.Marshalling;
using System.Text;
using Contta.Models;
using Contta.Infrastructure.Data;
using System.Runtime.CompilerServices;
using System.Globalization;

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

        public List<Despesas> Listar(string usuarioId)
        {
            return _context.Despesas.Where(despesas => despesas.UsuarioId == usuarioId).ToList();
        }

        public Despesas? BuscarPorId(int id, string usuarioId)
        {
            return _context.Despesas.FirstOrDefault(despesa => despesa.Id == id && despesa.UsuarioId == usuarioId);
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
        public List<Despesas> PesquisarPorDescricao(string descricao, string usuarioId)
        {
            return _context.Despesas.Where(despesa => despesa.Descricao.ToLower().Trim() == descricao.ToLower().Trim() && despesa.UsuarioId == usuarioId).ToList();

        }

        public List<Despesas> PesquisarPorCategoria(string categoria, string usuarioId)
        {
            return _context.Despesas.Where(despesa => despesa.Categoria.ToLower().Trim().Contains(categoria.ToLower().Trim()) && despesa.UsuarioId == usuarioId).ToList();
        }

        public List<Despesas> PesquisarPorData(int mes, int ano, string usuarioId)
        {
            return _context.Despesas.Where(data => data.Data.Month == mes && data.Data.Year == ano && data.UsuarioId == usuarioId).ToList();
        }

        public decimal CalcularTotal(string usuarioId)
        {
            return _context.Despesas.Where(despesa => despesa.UsuarioId == usuarioId).Sum(despesa => despesa.Valor);
        }

        public List<Despesas> PesquisarPorValor(decimal valor, string usuarioId)
        {
            return _context.Despesas.Where(despesa => despesa.Valor > valor && despesa.UsuarioId == usuarioId).ToList();
        }
    }
}
