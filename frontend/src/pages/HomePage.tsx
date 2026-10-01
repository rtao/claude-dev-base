import { useAuth0 } from '@auth0/auth0-react';

export function HomePage() {
  const { user } = useAuth0();

  return (
    <div>
      <h2>Welcome to Poker Tracker{user?.name ? `, ${user.name}` : ''}</h2>
      <p>Track your poker sessions and analyze your performance.</p>
    </div>
  );
}
