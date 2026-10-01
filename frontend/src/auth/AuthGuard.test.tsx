import { render, screen } from '@testing-library/react';
import { describe, it, expect, vi, beforeEach } from 'vitest';
import { AuthGuard } from './AuthGuard';

const mockLoginWithRedirect = vi.fn();

vi.mock('@auth0/auth0-react', () => ({
  useAuth0: vi.fn(),
}));

import { useAuth0 } from '@auth0/auth0-react';
const mockUseAuth0 = vi.mocked(useAuth0);

describe('AuthGuard', () => {
  beforeEach(() => {
    vi.clearAllMocks();
  });

  it('should render children when authenticated', () => {
    mockUseAuth0.mockReturnValue({
      isAuthenticated: true,
      isLoading: false,
      loginWithRedirect: mockLoginWithRedirect,
    } as unknown as ReturnType<typeof useAuth0>);

    render(
      <AuthGuard>
        <div>Protected Content</div>
      </AuthGuard>
    );

    expect(screen.getByText('Protected Content')).toBeInTheDocument();
  });

  it('should call loginWithRedirect when not authenticated', () => {
    mockUseAuth0.mockReturnValue({
      isAuthenticated: false,
      isLoading: false,
      loginWithRedirect: mockLoginWithRedirect,
    } as unknown as ReturnType<typeof useAuth0>);

    render(
      <AuthGuard>
        <div>Protected Content</div>
      </AuthGuard>
    );

    expect(mockLoginWithRedirect).toHaveBeenCalled();
    expect(screen.getByText('Redirecting to login...')).toBeInTheDocument();
  });

  it('should show loading state when auth is loading', () => {
    mockUseAuth0.mockReturnValue({
      isAuthenticated: false,
      isLoading: true,
      loginWithRedirect: mockLoginWithRedirect,
    } as unknown as ReturnType<typeof useAuth0>);

    render(
      <AuthGuard>
        <div>Protected Content</div>
      </AuthGuard>
    );

    expect(screen.getByText('Loading...')).toBeInTheDocument();
  });
});
