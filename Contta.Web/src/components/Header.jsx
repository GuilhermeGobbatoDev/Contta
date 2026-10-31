export default function Header({ page, onMenuClick, sidebarOpen }) {
  return (
    <header className="topbar">
      <div className="topbar-left">
        <button className="icon-button mobile-menu" type="button" aria-label="Abrir menu" aria-expanded={sidebarOpen} onClick={onMenuClick}>
          <svg viewBox="0 0 24 24" aria-hidden="true"><path d="M4 7h16M4 12h16M4 17h16"/></svg>
        </button>
        <span className="breadcrumb">Painel <span>/</span> <strong>{page === 'despesas' ? 'Despesas' : 'Visão geral'}</strong></span>
      </div>
      <div className="topbar-right"><span className="header-pill"><span className="status-dot"/> Prévia visual</span><div className="avatar top-avatar">C</div></div>
    </header>
  )
}
