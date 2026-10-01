import { render, screen } from '@testing-library/react';
import { describe, it, expect, vi } from 'vitest';

vi.mock('@auth0/auth0-react', () => ({
  Auth0Provider: ({ children }: { children: React.ReactNode }) => <>{children}</>,
  useAuth0: () => ({
    isAuthenticated: true,
    isLoading: false,
    user: { name: 'Test User', email: 'test@example.com' },
    loginWithRedirect: vi.fn(),
    logout: vi.fn(),
    getAccessTokenSilently: vi.fn().mockResolvedValue('fake-token'),
  }),
}));

import { App } from './App';

describe('App', () => {
  it('should render without crashing when authenticated', () => {
    render(<App />);

    expect(screen.getByText('Poker Tracker')).toBeInTheDocument();
    expect(screen.getByText(/Welcome to Poker Tracker/)).toBeInTheDocument();
  });

  it('should display user name in the header', () => {
    render(<App />);

    expect(screen.getByText('Test User')).toBeInTheDocument();
  });

  it('should show logout button', () => {
    render(<App />);

    expect(screen.getByText('Logout')).toBeInTheDocument();
  });
});
