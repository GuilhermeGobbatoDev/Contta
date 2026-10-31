import { useEffect, useRef, useState } from 'react'
import { categories } from '../data/demoExpenses'

function currentLocalDate() {
  const now = new Date()
  const offset = now.getTimezoneOffset() * 60_000
  return new Date(now.getTime() - offset).toISOString().slice(0, 10)
}

export default function ExpenseModal({ expense, onClose, onSave }) {
  const [description, setDescription] = useState(expense?.descricao ?? '')
  const [amount, setAmount] = useState(expense ? String(expense.valor) : '')
  const [date, setDate] = useState(expense?.data ?? currentLocalDate())
  const [category, setCategory] = useState(expense?.categoria ?? '')
  const descriptionRef = useRef(null)

  useEffect(() => {
    descriptionRef.current?.focus()
    const oldOverflow = document.body.style.overflow
    document.body.style.overflow = 'hidden'
    const onKeyDown = event => { if (event.key === 'Escape') onClose() }
    document.addEventListener('keydown', onKeyDown)
    return () => {
      document.body.style.overflow = oldOverflow
      document.removeEventListener('keydown', onKeyDown)
    }
  }, [onClose])

  function handleSubmit(event) {
    event.preventDefault()
    const valor = Number(amount)
    if (!description.trim() || !Number.isFinite(valor) || valor <= 0 || !category || !date) return
    onSave({ id: expense?.id, descricao: description.trim(), valor, categoria: category, data: date })
  }

  return (
    <div className="modal-backdrop" onMouseDown={event => { if (event.target === event.currentTarget) onClose() }}>
      <div className="modal" role="dialog" aria-modal="true" aria-labelledby="modalTitle">
        <div className="modal-header"><div><p className="eyebrow">CONTROLE FINANCEIRO</p><h2 id="modalTitle">{expense ? 'Editar despesa' : 'Nova despesa'}</h2></div><button className="icon-button" onClick={onClose} aria-label="Fechar janela" type="button">×</button></div>
        <form onSubmit={handleSubmit}>
          <label>Descrição<input ref={descriptionRef} value={description} onChange={event => setDescription(event.target.value)} type="text" placeholder="Ex.: Supermercado" maxLength={80} required/></label>
          <div className="form-row">
            <label>Valor (R$)<input value={amount} onChange={event => setAmount(event.target.value)} type="number" step="0.01" min="0.01" placeholder="0,00" required/></label>
            <label>Data<input value={date} onChange={event => setDate(event.target.value)} type="date" required/></label>
          </div>
          <label>Categoria<select value={category} onChange={event => setCategory(event.target.value)} required><option value="" disabled>Selecione uma categoria</option>{categories.map(item => <option key={item}>{item}</option>)}</select></label>
          <div className="modal-actions"><button className="secondary-button" onClick={onClose} type="button">Cancelar</button><button className="primary-button" type="submit">{expense ? 'Salvar alterações' : 'Adicionar despesa'}</button></div>
        </form>
      </div>
    </div>
  )
}
