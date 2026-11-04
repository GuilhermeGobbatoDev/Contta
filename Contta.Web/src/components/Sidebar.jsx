export default function Sidebar({ page, onNavigate, open, onClose }) {
  const items = [
    { key: 'visao-geral', title: 'Visão geral', icon: <><rect x="3" y="3" width="7" height="7" rx="1.5"/><rect x="14" y="3" width="7" height="7" rx="1.5"/><rect x="3" y="14" width="7" height="7" rx="1.5"/><rect x="14" y="14" width="7" height="7" rx="1.5"/></> },
    { key: 'despesas', title: 'Despesas', icon: <><rect x="3" y="5" width="18" height="14" rx="3"/><path d="M3 10h18M7 15h4"/></> },
    { key: 'relatorios', title: 'Relatórios', soon: true, icon: <><path d="M4 20V9m5 11V4m5 16v-7m5 7V8"/></> },
    { key: 'metas', title: 'Metas', soon: true, icon: <><circle cx="12" cy="12" r="9"/><circle cx="12" cy="12" r="5"/><circle cx="12" cy="12" r="1"/></> },
  ]

  return (
    <>
      <aside className={`sidebar ${open ? 'open' : ''}`} aria-label="Menu principal">
        <a className="brand" href="#visao-geral" onClick={(e) => { e.preventDefault(); onNavigate('visao-geral') }} aria-label="Contta - início">
          <span className="brand-mark"><svg viewBox="0 0 32 32" fill="none" aria-hidden="true"><path d="M22.7 10.5a9 9 0 1 0 0 11" stroke="currentColor" strokeWidth="5" strokeLinecap="round"/><path d="M27.5 12.3a13 13 0 0 0-6.4-8.4" stroke="currentColor" strokeWidth="4" strokeLinecap="round"/></svg></span>
          <span>contta<span className="brand-dot">.</span></span>
        </a>
        <div className="side-section-label">MENU PRINCIPAL</div>
        <nav className="sidebar-nav" aria-label="Navegação">
          {items.map(item => (
            <button key={item.key} className={`nav-item ${page === item.key ? 'active' : ''}`} type="button" onClick={() => onNavigate(item.key)}>
              <svg viewBox="0 0 24 24" aria-hidden="true">{item.icon}</svg><span>{item.title}</span>
              {item.soon && <span className="nav-soon">Em breve</span>}
            </button>
          ))}
        </nav>
        <div className="sidebar-bottom">
          <div className="help-card"><div className="help-icon">✦</div><strong>Seu dinheiro.<br/>Seu controle.</strong><p>Um lugar mais simples para cuidar das suas finanças.</p></div>
          <div className="sidebar-account"><div className="avatar">C</div><div><strong>Minha conta</strong><small>Ambiente de demonstração</small></div></div>
        </div>
      </aside>
      {open && <div className="overlay" onClick={onClose} aria-hidden="true"/>}
    </>
  )
}
