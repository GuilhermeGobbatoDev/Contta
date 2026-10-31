
import { useCallback, useEffect, useState } from 'react'
import Header from './components/Header'
import Sidebar from './components/Sidebar'
import Dashboard from './components/Dashboard'
import ExpensesTable from './components/ExpensesTable'
import ExpenseModal from './components/ExpenseModal'
import './App.css'

const API_URL = 'https://localhost:7264/api/despesas'

export default function App() {
    const [expenses, setExpenses] = useState([])
    const [page, setPage] = useState('visao-geral')
    const [sidebarOpen, setSidebarOpen] = useState(false)
    const [showNote, setShowNote] = useState(true)
    const [modal, setModal] = useState(null)
    const [filters, setFilters] = useState({
        search: '',
        category: '',
        month: '',
        minValue: ''
    })
    const [toast, setToast] = useState('')

    const closeModal = useCallback(() => setModal(null), [])

    // Converte a data da API para o formato usado pelo React
    function formatarDespesa(despesa) {
        return {
            ...despesa,
            data: despesa.data.split('T')[0]
        }
    }

    // GET - Busca as despesas reais do PostgreSQL
    useEffect(() => {
        async function carregarDespesas() {
            try {
                let url = API_URL

                if (filters.minValue !== "")
                {
                    url = `${API_URL}/Despesa-Maior?valor=${filters.minValue}`
                }
                const resposta = await fetch(url)

                if (!resposta.ok) {
                    throw new Error('Erro ao buscar despesas')
                }

                const dados = await resposta.json()

                setExpenses(dados.map(formatarDespesa))

            } catch (erro) {
                console.error('Erro na API:', erro)
                setToast('Erro ao carregar despesas.')
            }
        }

        carregarDespesas()
    }, [filters.minValue])

    // Remove automaticamente as notificações
    useEffect(() => {
        if (!toast) return undefined

        const timeout = setTimeout(() => setToast(''), 3300)

        return () => clearTimeout(timeout)
    }, [toast])

    function navigateTo(nextPage) {
        setSidebarOpen(false)

        if (nextPage === 'relatorios' || nextPage === 'metas') {
            setToast(
                `${nextPage === 'relatorios' ? 'Relatórios' : 'Metas'} estará disponível em uma versão futura.`
            )
            return
        }

        setPage(nextPage)

        if (nextPage === 'despesas') {
            document.getElementById('despesas')?.scrollIntoView({
                behavior: 'smooth',
                block: 'start'
            })
        } else {
            window.scrollTo({ top: 0, behavior: 'smooth' })
        }
    }

    // POST e PUT - Cadastrar ou editar despesas
    async function saveExpense(expense) {

        // Recupera o ID da despesa selecionada no modal
        const id = modal?.expense?.id ?? expense.id

        // PUT - Editar uma despesa existente
        if (id != null) {
            try {
                const resposta = await fetch(`${API_URL}/${id}`, {
                    method: 'PUT',
                    headers: {
                        'Content-Type': 'application/json'
                    },
                    body: JSON.stringify(expense)
                })

                if (!resposta.ok) {
                    throw new Error('Erro ao editar despesa')
                }

                const despesaAtualizada = formatarDespesa({
                    ...expense,
                    id
                })

                setExpenses(previous =>
                    previous.map(item =>
                        item.id === id ? despesaAtualizada : item
                    )
                )

                setToast('Despesa editada com sucesso!')
                closeModal()

            } catch (erro) {
                console.error('Erro ao editar:', erro)
                setToast('Erro ao editar despesa.')
            }

            return
        }

        // POST - Cadastrar uma nova despesa
        try {
            const resposta = await fetch(API_URL, {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(expense)
            })

            if (!resposta.ok) {
                throw new Error('Erro ao cadastrar despesa')
            }

            const novaDespesa = await resposta.json()

            setExpenses(previous => [
                ...previous,
                formatarDespesa(novaDespesa)
            ])

            setToast('Despesa cadastrada com sucesso!')
            closeModal()

        } catch (erro) {
            console.error('Erro ao cadastrar:', erro)
            setToast('Erro ao cadastrar despesa.')
        }
    }

    // DELETE - Ainda vamos integrar ao banco
    async function deleteExpense(expense) {
        const confirmar = window.confirm(
            `Tem certeza que deseja excluir "${expense.descricao}"?`
        )

        if (!confirmar) return

        try {
            const resposta = await fetch(`${API_URL}/${expense.id}`, {
                method: 'DELETE'
            })

            if (!resposta.ok) {
                throw new Error('Erro ao excluir despesa')
            }

            setExpenses(previous =>
                previous.filter(item => item.id !== expense.id)
            )

            setToast('Despesa excluída com sucesso!')

        } catch (erro) {
            console.error('Erro na API:', erro)
            setToast('Erro ao excluir despesa.')
        }
    }

    return (
        <div className="app-shell" id="visao-geral">

            <Sidebar
                page={page}
                onNavigate={navigateTo}
                open={sidebarOpen}
                onClose={() => setSidebarOpen(false)}
            />

            <div className="main-area">

                <Header
                    page={page}
                    onMenuClick={() => setSidebarOpen(value => !value)}
                    sidebarOpen={sidebarOpen}
                />

                <main className="main-content">

                    {showNote && (
                        <div className="demo-note" role="note">
                            <svg viewBox="0 0 24 24" aria-hidden="true">
                                <circle cx="12" cy="12" r="9" />
                                <path d="M12 11v5m0-8h.01" />
                            </svg>

                            <span>
                                <strong>Contta conectado:</strong> listagem,
                                cadastro e edição utilizam a API e o PostgreSQL.
                                A exclusão será integrada em seguida.
                            </span>

                            <button
                                type="button"
                                aria-label="Dispensar aviso"
                                onClick={() => setShowNote(false)}
                            >
                                ×
                            </button>
                        </div>
                    )}

                    <section className="welcome">
                        <div>
                            <p className="eyebrow">SEU PAINEL FINANCEIRO</p>

                            <h1>
                                Seu dinheiro, mais claro. <span>✦</span>
                            </h1>

                            <p>
                                Veja suas despesas e acompanhe seus gastos
                                de um jeito simples.
                            </p>
                        </div>

                        <button
                            className="primary-button"
                            type="button"
                            onClick={() => setModal({ expense: null })}
                        >
                            <span className="plus-sign">+</span>
                            Nova despesa
                        </button>
                    </section>

                    <Dashboard expenses={expenses} />

                    <ExpensesTable
                        expenses={expenses}
                        filters={filters}
                        onFiltersChange={setFilters}
                        onEdit={expense => setModal({ expense })}
                        onDelete={deleteExpense}
                    />

                    <footer className="footer">
                        Contta 💜 · React + ASP.NET Core + PostgreSQL
                    </footer>

                </main>
            </div>

            {modal !== null && (
                <ExpenseModal
                    key={modal.expense?.id ?? 'new'}
                    expense={modal.expense}
                    onClose={closeModal}
                    onSave={saveExpense}
                />
            )}

            <div
                className={`toast ${toast ? 'show' : ''}`}
                role="status"
                aria-live="polite"
            >
                {toast}
            </div>

        </div>
    )
}
