import { useState } from 'react'
import Auth from './components/Auth.jsx'
import App from './App.jsx'

export default function Root() {
    const [token, setToken] = useState(null)

    return token
        ? <App token={token} />
        : <Auth onLogin={setToken} />
}
