import { categories, categoryColors, money, shortMoney, monthNames } from '../data/demoExpenses'

export default function Dashboard({ expenses }) {
  const total = expenses.reduce((sum, expense) => sum + expense.valor, 0)
  const totalsByCategory = Object.fromEntries(categories.map(category => [category, 0]))
  for (const expense of expenses) totalsByCategory[expense.categoria] = (totalsByCategory[expense.categoria] ?? 0) + expense.valor
  const ranking = Object.entries(totalsByCategory).filter(([, value]) => value > 0).sort((a, b) => b[1] - a[1])
  const year = 2026 // Período fixo do protótipo; depois integraremos os filtros reais da API.
  const monthlyTotals = monthNames.map((_, index) => expenses
    .filter(expense => expense.data.startsWith(`${year}-${String(index + 1).padStart(2, '0')}`))
    .reduce((sum, expense) => sum + expense.valor, 0))
  const maxMonthly = Math.max(1, ...monthlyTotals)

  let offset = 0
  const slices = ranking.map(([category, value]) => {
    const next = offset + (value / total) * 100
    const result = `${categoryColors[category] ?? categoryColors.Outros} ${offset.toFixed(2)}% ${next.toFixed(2)}%`
    offset = next
    return result
  })
  const donutBackground = slices.length ? `conic-gradient(${slices.join(',')})` : '#e8e5f0'

  return (
    <>
      <section className="summary-grid" aria-label="Resumo de despesas">
        <article className="summary-card highlighted">
          <div className="metric-icon light-icon"><svg viewBox="0 0 24 24" aria-hidden="true"><path d="M4 16.5 9 11l3 3 7-7M15 7h4v4"/></svg></div>
          <div className="summary-label">Total de despesas</div><div className="summary-value">{money(total)}</div>
          <div className="metric-foot"><span className="metric-tag">Na demonstração</span><span>Despesas cadastradas</span></div><div className="card-decoration"/>
        </article>
        <article className="summary-card">
          <div className="metric-icon lavender-icon"><svg viewBox="0 0 24 24" aria-hidden="true"><rect x="4" y="4" width="16" height="16" rx="3"/><path d="M8 9h8m-8 4h8m-8 4h5"/></svg></div>
          <div className="summary-label">Despesas registradas</div><div className="summary-value">{expenses.length}</div>
          <div className="metric-foot"><span>Registros de exemplo</span></div>
        </article>
        <article className="summary-card">
          <div className="metric-icon peach-icon"><svg viewBox="0 0 24 24" aria-hidden="true"><path d="m12 3 2.8 5.7 6.2.9-4.5 4.4 1.1 6.2L12 17.3l-5.6 2.9 1.1-6.2L3 9.6l6.2-.9L12 3Z"/></svg></div>
          <div className="summary-label">Maior categoria</div><div className="summary-value small-value">{ranking[0]?.[0] ?? '—'}</div>
          <div className="metric-foot"><span>{ranking.length ? `${money(ranking[0][1])} em lançamentos` : 'Sem lançamentos'}</span></div>
        </article>
      </section>

      <section className="analytics-grid" aria-label="Análise financeira">
        <article className="panel month-panel">
          <div className="panel-heading"><div><h2>Gastos ao longo dos meses</h2><p>Evolução dos seus lançamentos</p></div><span className="panel-chip">{year}</span></div>
          <div className="bars" aria-label="Gráfico de gastos mensais">
            {monthlyTotals.map((amount, index) => (
              <div className={`bar-col ${index === 9 ? 'featured' : ''}`} key={monthNames[index]}>
                <div className="bar-fill" style={{ height: `${Math.max((amount / maxMonthly) * 97, amount ? 3 : 1)}%` }} data-tip={money(amount)} tabIndex={0} role="img" aria-label={`${monthNames[index]}: ${money(amount)}`}/>
                <span className="bar-label">{monthNames[index]}</span>
              </div>
            ))}
          </div>
        </article>
        <article className="panel category-panel">
          <div className="panel-heading"><div><h2>Por categoria</h2><p>Onde seu dinheiro foi gasto</p></div></div>
          <div className="donut-content">
            <div className="donut" style={{ background: donutBackground }}><div className="donut-center"><small>Total</small><strong>{shortMoney(total)}</strong></div></div>
            <div className="category-legend">
              {ranking.length ? ranking.slice(0, 6).map(([category, value]) => (
                <div className="legend-row" key={category}>
                  <span className="legend-name"><span className="legend-dot" style={{ background: categoryColors[category] ?? categoryColors.Outros }}/>{category}</span>
                  <strong>{Math.round((value / total) * 100)}%</strong>
                </div>
              )) : <span className="legend-empty">Sem despesas cadastradas.</span>}
            </div>
          </div>
        </article>
      </section>
    </>
  )
}
