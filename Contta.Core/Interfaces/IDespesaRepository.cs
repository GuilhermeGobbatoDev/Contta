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
    }
}
