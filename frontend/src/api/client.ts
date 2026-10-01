import axios from 'axios';

const apiClient = axios.create({
  baseURL: '/',
});

export function setAuthToken(getAccessTokenSilently: () => Promise<string>) {
  apiClient.interceptors.request.use(async (config) => {
    const token = await getAccessTokenSilently();
    config.headers.Authorization = `Bearer ${token}`;
    return config;
  });
}

export default apiClient;
