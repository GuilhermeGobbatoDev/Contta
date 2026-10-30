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
       
    }
}
