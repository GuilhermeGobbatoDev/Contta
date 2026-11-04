// Dados fictícios usados apenas para montar e testar a interface.
// Nada aqui é enviado ao PostgreSQL ou à API ASP.NET Core.
export const initialExpenses = [
  { id: 1, descricao: 'Supermercado', valor: 286.9, categoria: 'Alimentação', data: '2026-10-25' },
  { id: 2, descricao: 'Internet residencial', valor: 109.9, categoria: 'Moradia', data: '2026-10-23' },
  { id: 3, descricao: 'Combustível', valor: 180, categoria: 'Transporte', data: '2026-10-18' },
  { id: 4, descricao: 'Cinema no fim de semana', valor: 64, categoria: 'Lazer', data: '2026-10-13' },
  { id: 5, descricao: 'Farmácia', valor: 56.8, categoria: 'Saúde', data: '2026-10-08' },
  { id: 6, descricao: 'Mensalidade do curso', valor: 140, categoria: 'Educação', data: '2026-09-28' },
  { id: 7, descricao: 'Mercado do mês', valor: 340.5, categoria: 'Alimentação', data: '2026-09-12' },
  { id: 8, descricao: 'Aluguel', valor: 850, categoria: 'Moradia', data: '2026-08-07' },
  { id: 9, descricao: 'Transporte urbano', valor: 95, categoria: 'Transporte', data: '2026-07-15' },
  { id: 10, descricao: 'Almoço', valor: 38.9, categoria: 'Alimentação', data: '2026-06-18' },
]

export const categories = ['Alimentação', 'Moradia', 'Transporte', 'Saúde', 'Lazer', 'Educação', 'Outros']
export const categoryColors = {
  Alimentação: '#7556e8', Moradia: '#ad97f4', Transporte: '#b6a3ec',
  Saúde: '#f2a57f', Lazer: '#f6c79b', Educação: '#79c7b0', Outros: '#a5abc6',
}
export const categorySymbols = {
  Alimentação: '◈', Moradia: '⌂', Transporte: '↗',
  Saúde: '✚', Lazer: '✦', Educação: '▤', Outros: '◉',
}
export const monthNames = ['Jan', 'Fev', 'Mar', 'Abr', 'Mai', 'Jun', 'Jul', 'Ago', 'Set', 'Out', 'Nov', 'Dez']
export const money = value => new Intl.NumberFormat('pt-BR', { style: 'currency', currency: 'BRL' }).format(value)
export const shortMoney = value => value >= 1000 ? `R$ ${(value / 1000).toFixed(1).replace('.', ',')} mil` : money(value)
export const dateLabel = value => new Intl.DateTimeFormat('pt-BR', { timeZone: 'UTC' }).format(new Date(`${value}T12:00:00Z`))
