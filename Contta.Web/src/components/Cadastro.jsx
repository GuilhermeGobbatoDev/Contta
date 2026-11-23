import { useState } from 'react'

export default function Cadastro() {
    const [nomeCompleto, setNomeCompleto] = useState('')
    const [email, setEmail] = useState('')
    const [senha, setSenha] = useState('')

    async function handleSubmit(e) {
        e.preventDefault()

        const dadosUsuario = {
            nomeCompleto,
            email,
            senha
        }

        try {
            const resposta = await fetch(
                'http://localhost:5013/api/auth/registrar',
                {
                    method: 'POST',
                    headers: {
                        'Content-Type': 'application/json'
                    },
                    body: JSON.stringify(dadosUsuario)
                }
            )

            if (resposta.ok) {
                alert('Conta criada com sucesso!')
            } else {
                alert('Não foi possível criar a conta.')
            }
        } catch (erro) {
            console.error('Erro de conexão:', erro)
            alert('Não foi possível conectar à API.')
        }
    }

    return (
        <form onSubmit={handleSubmit}>
            <h1> Criar conta no Contta</h1>
            <input type="text"
                placeholder="Nome Completo"
                value={nomeCompleto}
                onChange={(e) => setNomeCompleto(e.target.value)}
                required
                minLength={3}
            />

            <input type="email"
                placeholder="Digite seu melhor e-mail!"
                value={email}
                onChange={(e) => setEmail(e.target.value)}
                required
                minLength={3}
            />

            <input type="password"
                placeholder="Escolha uma senha"
                value={senha}
                onChange={(e) => setSenha(e.target.value)}
                required
                minLength={8}
            />

            <button type="submit">
                Fazer cadastro
            </button>
                

        </form>
    )
   
}