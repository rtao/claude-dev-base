import { useAuth0 } from '@auth0/auth0-react';
import { Outlet } from 'react-router-dom';

export function AppLayout() {
  const { user, logout } = useAuth0();

  return (
    <div className="app-layout">
      <header className="app-header">
        <nav className="app-nav">
          <h1 className="app-title">Poker Tracker</h1>
          <div className="user-section">
            <span className="user-name">{user?.name ?? user?.email}</span>
            <button
              className="logout-button"
              onClick={() => logout({ logoutParams: { returnTo: window.location.origin } })}
            >
              Logout
            </button>
          </div>
        </nav>
      </header>
      <main className="app-main">
        <Outlet />
      </main>
    </div>
  );
}
