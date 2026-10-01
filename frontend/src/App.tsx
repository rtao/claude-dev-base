import { BrowserRouter, Routes, Route } from 'react-router-dom';
import { QueryClient, QueryClientProvider } from '@tanstack/react-query';
import { AuthProvider } from './auth/AuthProvider';
import { AuthGuard } from './auth/AuthGuard';
import { AppLayout } from './layouts/AppLayout';
import { HomePage } from './pages/HomePage';
import { SessionsPage } from './pages/SessionsPage';
import { NotFoundPage } from './pages/NotFoundPage';

const queryClient = new QueryClient();

export function App() {
  return (
    <AuthProvider>
      <QueryClientProvider client={queryClient}>
        <BrowserRouter>
          <Routes>
            <Route element={<AuthGuard><AppLayout /></AuthGuard>}>
              <Route index element={<HomePage />} />
              <Route path="sessions" element={<SessionsPage />} />
              <Route path="*" element={<NotFoundPage />} />
            </Route>
          </Routes>
        </BrowserRouter>
      </QueryClientProvider>
    </AuthProvider>
  );
}
