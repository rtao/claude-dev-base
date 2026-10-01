import { Auth0Provider, useAuth0 } from '@auth0/auth0-react';
import { useEffect, type ReactNode } from 'react';
import { setAuthToken } from '../api/client';

function AuthTokenSetup({ children }: { children: ReactNode }) {
  const { getAccessTokenSilently } = useAuth0();
  useEffect(() => {
    setAuthToken(async () => {
      const token = await getAccessTokenSilently();
      return token ?? '';
    });
  }, [getAccessTokenSilently]);
  return <>{children}</>;
}

export function AuthProvider({ children }: { children: ReactNode }) {
  return (
    <Auth0Provider
      domain={import.meta.env.VITE_AUTH0_DOMAIN}
      clientId={import.meta.env.VITE_AUTH0_CLIENT_ID}
      authorizationParams={{
        redirect_uri: window.location.origin,
        audience: import.meta.env.VITE_AUTH0_AUDIENCE,
      }}
    >
      <AuthTokenSetup>{children}</AuthTokenSetup>
    </Auth0Provider>
  );
}
