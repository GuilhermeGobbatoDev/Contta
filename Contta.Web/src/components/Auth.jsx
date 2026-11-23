import { useState } from 'react'
import './Auth.css'

const API_AUTH = 'http://localhost:5013/api/auth'

export default function Auth({ onLogin }) {
    const [modo, setModo] = useState('login')
    const [nomeCompleto, setNomeCompleto] = useState('')
    const [email, setEmail] = useState('')
    const [senha, setSenha] = useState('')
    const [mostrarSenha, setMostrarSenha] = useState(false)
    const [carregando, setCarregando] = useState(false)
    const [erro, setErro] = useState('')
    const [sucesso, setSucesso] = useState('')

    const cadastrando = modo === 'cadastro'

    function mudarModo(proximoModo) {
        if (carregando) return
        setModo(proximoModo)
        setSenha('')
        setMostrarSenha(false)
        setErro('')
        setSucesso('')
    }

    async function handleSubmit(e) {
        e.preventDefault()
        if (carregando) return

        setErro('')
        setSucesso('')
        setCarregando(true)

        try {
            const endereco = cadastrando ? `${API_AUTH}/registrar` : `${API_AUTH}/login`
            const dadosUsuario = cadastrando
                ? { nomeCompleto: nomeCompleto.trim(), email: email.trim(), senha }
                : { email: email.trim(), senha }

            const resposta = await fetch(endereco, {
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify(dadosUsuario)
            })

            const texto = await resposta.text()
            let dados = null
            try {
                dados = texto ? JSON.parse(texto) : null
            } catch {
                // A API também pode retornar uma resposta sem JSON.
            }

            if (!resposta.ok) {
                if (!cadastrando && resposta.status === 401) {
                    setErro('E-mail ou senha inválidos. Confira os dados e tente novamente.')
                } else if (cadastrando && Array.isArray(dados)) {
                    const mensagens = dados.map(item => item.description).filter(Boolean)
                    setErro(mensagens.join(' ') || 'Não foi possível criar sua conta.')
                } else if (cadastrando && dados?.errors) {
                    setErro(Object.values(dados.errors).flat().join(' '))
                } else {
                    setErro('Não foi possível concluir a operação. Tente novamente.')
                }
                return
            }

            if (cadastrando) {
                setModo('login')
                setSenha('')
                setMostrarSenha(false)
                setSucesso('Conta criada com sucesso! Entre com seus dados para continuar.')
                return
            }

            if (typeof dados?.token !== 'string' || !dados.token) {
                setErro('A API não retornou o token de acesso. Tente novamente.')
                return
            }

            onLogin(dados.token)
        } catch {
            setErro('Não foi possível conectar à API. Confira se o Contta.Api está ligado.')
        } finally {
            setCarregando(false)
        }
    }

    return (
        <main className="auth-screen">
            <div className="auth-layout">
                <section className="auth-showcase" aria-label="Apresentação do Contta">
                    <div className="auth-brand auth-brand-light">
                        <span className="auth-brand-mark">C</span>
                        <span>contta<span className="auth-brand-dot">.</span></span>
                    </div>

                    <div className="auth-showcase-content">
                        <span className="auth-eyebrow">SUAS FINANÇAS, MAIS SIMPLES</span>
                        <h1>Seu dinheiro.<br />Seu controle<span>.</span></h1>
                        <p>Organize suas despesas, acompanhe seus gastos e tome decisões com mais tranquilidade.</p>

                        <div className="auth-preview" aria-hidden="true">
                            <div className="auth-preview-top">
                                <span className="auth-preview-icon">✦</span>
                                <span>Visão geral das suas finanças</span>
                                <span className="auth-preview-dots">•••</span>
                            </div>
                            <div className="auth-preview-main">
                                <span className="auth-preview-label">Um lugar para acompanhar tudo</span>
                                <strong>Mais clareza, todo dia.</strong>
                            </div>
                            <div className="auth-preview-chart">
                                <i style={{ height: '35%' }} />
                                <i style={{ height: '58%' }} />
                                <i style={{ height: '44%' }} />
                                <i style={{ height: '78%' }} />
                                <i style={{ height: '64%' }} />
                                <i style={{ height: '90%' }} />
                                <i style={{ height: '70%' }} />
                            </div>
                        </div>
                    </div>

                    <span className="auth-showcase-footer">Controle de verdade. Sem complicação. ✦</span>
                    <div className="auth-glow auth-glow-one" aria-hidden="true" />
                    <div className="auth-glow auth-glow-two" aria-hidden="true" />
                </section>

                <section className="auth-panel" aria-label={cadastrando ? 'Criar conta' : 'Entrar na conta'}>
                    <div className="auth-panel-inner">
                        <div className="auth-mobile-brand auth-brand">
                            <span className="auth-brand-mark">C</span>
                            <span>contta<span className="auth-brand-dot">.</span></span>
                        </div>

                        <div className="auth-switch" role="group" aria-label="Escolha como acessar">
                            <button
                                type="button"
                                className={!cadastrando ? 'auth-switch-active' : ''}
                                onClick={() => mudarModo('login')}
                                aria-pressed={!cadastrando}
                                disabled={carregando}
                            >Entrar</button>
                            <button
                                type="button"
                                className={cadastrando ? 'auth-switch-active' : ''}
                                onClick={() => mudarModo('cadastro')}
                                aria-pressed={cadastrando}
                                disabled={carregando}
                            >Criar conta</button>
                        </div>

                        <div className="auth-heading">
                            <span className="auth-heading-icon">{cadastrando ? '✦' : '👋'}</span>
                            <h2>{cadastrando ? 'Comece com o Contta' : 'Bem-vindo de volta!'}</h2>
                            <p>{cadastrando
                                ? 'Crie sua conta e dê o primeiro passo para organizar suas finanças.'
                                : 'Entre na sua conta para continuar de onde parou.'}</p>
                        </div>

                        <form className="auth-form" onSubmit={handleSubmit}>
                            {cadastrando && (
                                <div className="auth-field">
                                    <label htmlFor="auth-nome">Nome completo</label>
                                    <input
                                        id="auth-nome"
                                        type="text"
                                        placeholder="Como podemos chamar você?"
                                        autoComplete="name"
                                        value={nomeCompleto}
                                        onChange={e => setNomeCompleto(e.target.value)}
                                        minLength={3}
                                        required
                                    />
                                </div>
                            )}

                            <div className="auth-field">
                                <label htmlFor="auth-email">E-mail</label>
                                <input
                                    id="auth-email"
                                    type="email"
                                    placeholder="seu@email.com"
                                    autoComplete="email"
                                    value={email}
                                    onChange={e => setEmail(e.target.value)}
                                    required
                                />
                            </div>

                            <div className="auth-field">
                                <label htmlFor="auth-senha">Senha</label>
                                <div className="auth-password-wrap">
                                    <input
                                        id="auth-senha"
                                        type={mostrarSenha ? 'text' : 'password'}
                                        placeholder={cadastrando ? 'Crie uma senha segura' : 'Digite sua senha'}
                                        autoComplete={cadastrando ? 'new-password' : 'current-password'}
                                        value={senha}
                                        onChange={e => setSenha(e.target.value)}
                                        minLength={cadastrando ? 8 : undefined}
                                        required
                                    />
                                    <button
                                        className="auth-password-toggle"
                                        type="button"
                                        onClick={() => setMostrarSenha(visivel => !visivel)}
                                        aria-label={mostrarSenha ? 'Ocultar senha' : 'Mostrar senha'}
                                        aria-pressed={mostrarSenha}
                                    >{mostrarSenha ? 'Ocultar' : 'Mostrar'}</button>
                                </div>
                                {cadastrando && (
                                    <p className="auth-hint">Use pelo menos 8 caracteres, incluindo letras, número e símbolo.</p>
                                )}
                            </div>

                            {erro && <div className="auth-feedback auth-feedback-error" role="alert">{erro}</div>}
                            {sucesso && <div className="auth-feedback auth-feedback-success" role="status">{sucesso}</div>}

                            <button className="auth-submit" type="submit" disabled={carregando}>
                                {carregando ? 'Aguarde...' : cadastrando ? 'Criar minha conta' : 'Entrar no Contta'}
                                {!carregando && <span aria-hidden="true">→</span>}
                            </button>
                        </form>

                        <p className="auth-bottom-link">
                            {cadastrando ? 'Já tem uma conta?' : 'Ainda não tem uma conta?'}{' '}
                            <button type="button" onClick={() => mudarModo(cadastrando ? 'login' : 'cadastro')} disabled={carregando}>
                                {cadastrando ? 'Fazer login' : 'Cadastre-se grátis'}
                            </button>
                        </p>
                        <p className="auth-panel-footer">Feito para dar mais clareza ao seu dinheiro. 💜</p>
                    </div>
                </section>
            </div>
        </main>
    )
}
