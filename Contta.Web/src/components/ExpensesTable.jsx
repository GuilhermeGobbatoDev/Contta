import { categories, categorySymbols, dateLabel, money, monthNames } from '../data/demoExpenses'

export default function ExpensesTable({ expenses, filters, onFiltersChange, onEdit, onDelete }) {
  const search = filters.search.trim().toLocaleLowerCase('pt-BR')
  const filtered = expenses
    .filter(expense => (!search || expense.descricao.toLocaleLowerCase('pt-BR').includes(search)) &&
      (!filters.category || expense.categoria === filters.category) &&
      (!filters.month || expense.data.slice(5, 7) === filters.month))
    .sort((a, b) => b.data.localeCompare(a.data) || b.id - a.id)

  const setFilter = (key, value) => onFiltersChange(previous => ({ ...previous, [key]: value }))

  return (
    <section className="panel transactions-panel" id="despesas">
      <div className="panel-heading transactions-heading"><div><h2>Minhas despesas</h2><p>Consulte, organize e gerencie seus lançamentos</p></div><span className="table-count">{filtered.length} {filtered.length === 1 ? 'despesa' : 'despesas'}</span></div>
      <div className="filters">
        <label className="search-field"><input type="number" placeholder="Valor mínimo (R$)" value={filters.minValue} onChange={event => setFilter('minValue', event.target.value)}/></label>
        <label className="search-field"><svg viewBox="0 0 24 24" aria-hidden="true"><circle cx="10.8" cy="10.8" r="6.8"/><path d="m16 16 5 5"/></svg><input value={filters.search} onChange={event => setFilter('search', event.target.value)} type="search" placeholder="Buscar por descrição..." aria-label="Buscar por descrição"/></label>
        <label className="select-field"><span className="sr-only">Filtrar por categoria</span><select value={filters.category} onChange={event => setFilter('category', event.target.value)}><option value="">Todas as categorias</option>{categories.map(category => <option key={category} value={category}>{category}</option>)}</select></label>
        <label className="select-field"><span className="sr-only">Filtrar por mês</span><select value={filters.month} onChange={event => setFilter('month', event.target.value)}><option value="">Todos os meses</option>{monthNames.map((month, index) => <option key={month} value={String(index + 1).padStart(2, '0')}>{month}</option>)}</select></label>
        <button className="secondary-button" type="button" onClick={() => onFiltersChange({ search: '', category: '', month: '', minValue: '' })}>Limpar filtros</button>
      </div>
      <div className="table-scroll">
        <table><thead><tr><th>DESCRIÇÃO</th><th>CATEGORIA</th><th>DATA</th><th>VALOR</th><th className="actions-col">AÇÕES</th></tr></thead>
          <tbody>
            {filtered.map(expense => (
              <tr key={expense.id}>
                <td><div className="tx-desc"><span className="tx-icon">{categorySymbols[expense.categoria] ?? '◉'}</span><span className="tx-title">{expense.descricao}</span></div></td>
                <td><span className="category-pill">{expense.categoria}</span></td>
                <td>{dateLabel(expense.data)}</td>
                <td className="tx-amount">{money(expense.valor)}</td>
                <td className="actions-col"><div className="action-buttons"><button className="table-action" type="button" aria-label={`Editar ${expense.descricao}`} onClick={() => onEdit(expense)}>Editar</button><button className="table-action delete" type="button" aria-label={`Excluir ${expense.descricao}`} onClick={() => onDelete(expense)}>Excluir</button></div></td>
              </tr>
            ))}
          </tbody>
        </table>
      </div>
      {!filtered.length && <p className="empty-state">Nenhuma despesa encontrada para esses filtros.</p>}
    </section>
  )
}
