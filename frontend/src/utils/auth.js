// Authentication utility functions for managing JWT tokens and user sessions

/**
 * Get the stored authentication token
 * Checks both localStorage and sessionStorage
 */
export const getAuthToken = () => {
  return (
    localStorage.getItem('authToken') || sessionStorage.getItem('authToken')
  );
};

// Get the stored user role
export const getUserRole = () => {
  return localStorage.getItem('userRole') || sessionStorage.getItem('userRole');
};

// Get the stored user data
export const getUser = () => {
  const userStr =
    localStorage.getItem('user') || sessionStorage.getItem('user');
  return userStr ? JSON.parse(userStr) : null;
};

// Get the stored user ID
export const getUserId = () => {
  return localStorage.getItem('userId') || sessionStorage.getItem('userId');
};

// Get the stored user email

export const getUserEmail = () => {
  return (
    localStorage.getItem('userEmail') || sessionStorage.getItem('userEmail')
  );
};

// Get the stored user name

export const getUserName = () => {
  return localStorage.getItem('userName') || sessionStorage.getItem('userName');
};

// Check if user is authenticated (has valid token)

export const isAuthenticated = () => {
  const token = getAuthToken();
  if (!token) return false;

  // Optional: Check if token is expired
  try {
    const payload = JSON.parse(atob(token.split('.')[1]));
    const expiry = payload.exp * 1000; // Convert to milliseconds
    return Date.now() < expiry;
  } catch (e) {
    return false;
  }
};

// Clear all authentication data (logout)

export const logout = () => {
  // Clear from both storages
  localStorage.removeItem('authToken');
  localStorage.removeItem('userRole');
  localStorage.removeItem('userEmail');
  localStorage.removeItem('userId');
  localStorage.removeItem('userName');
  localStorage.removeItem('user');

  sessionStorage.removeItem('authToken');
  sessionStorage.removeItem('userRole');
  sessionStorage.removeItem('userEmail');
  sessionStorage.removeItem('userId');
  sessionStorage.removeItem('userName');
  sessionStorage.removeItem('user');

  // Redirect to login
  window.location.href = '/';
};

// Make authenticated API request with token in header

export const fetchWithAuth = async (url, options = {}) => {
  const token = getAuthToken();

  const headers = {
    'Content-Type': 'application/json',
    ...options.headers,
  };

  if (token) {
    headers['Authorization'] = `Bearer ${token}`;
  }

  const response = await fetch(url, {
    ...options,
    headers,
  });

  // If unauthorized, logout and redirect
  if (response.status === 401) {
    logout();
  }

  return response;
};

// Decode JWT token payload
export const decodeToken = (token) => {
  try {
    const base64Url = token.split('.')[1];
    const base64 = base64Url.replace(/-/g, '+').replace(/_/g, '/');
    const jsonPayload = decodeURIComponent(
      atob(base64)
        .split('')
        .map((c) => '%' + ('00' + c.charCodeAt(0).toString(16)).slice(-2))
        .join('')
    );
    return JSON.parse(jsonPayload);
  } catch (e) {
    console.error('Error decoding token:', e);
    return null;
  }
};

// Get token expiry date
export const getTokenExpiry = () => {
  const token = getAuthToken();
  if (!token) return null;

  const decoded = decodeToken(token);
  if (!decoded || !decoded.exp) return null;

  return new Date(decoded.exp * 1000);
};

// Check if token is about to expire (within 5 minutes)

export const isTokenExpiringSoon = () => {
  const expiry = getTokenExpiry();
  if (!expiry) return true;

  const fiveMinutes = 5 * 60 * 1000;
  return expiry.getTime() - Date.now() < fiveMinutes;
};
