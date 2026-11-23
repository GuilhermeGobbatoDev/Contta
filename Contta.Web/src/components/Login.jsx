import { useState } from 'react'

export default function Login({ onLogin }) {
    const [email, setEmail] = useState('')
    const [senha, setSenha] = useState('')

    async function handleSubmit(e) {
        e.preventDefault()

        try {
            const resposta = await fetch(
                'http://localhost:5013/api/auth/login',
                {
                    method: 'POST',
                    headers: {
                        'Content-Type': 'application/json'
                    },
                    body: JSON.stringify({ email, senha })
                }
            )

            if (!resposta.ok) {
                alert('E-mail ou senha inválidos!')
                return
            }

            const dados = await resposta.json()

            if (dados.token) {
                onLogin(dados.token)
            }

        } catch (erro) {
            console.error('Erro de conexão:', erro)
            alert('Não foi possível conectar à API.')
        }
    }


    return (
        <form onSubmit={handleSubmit}>
            <input
                type="email"
                placeholder="Digite seu Email"
                value={email}
                onChange={(e) => setEmail(e.target.value)}
            />
            <input
                type="password"
                placeholder="Digite sua Senha"
                value={senha}
                onChange={(e) => setSenha(e.target.value)}
            />
            <button type="submit">
                Entrar
            </button>


        </form>
    )
}