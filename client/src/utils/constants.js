const API_URL = 'http://localhost:51406'
export const API_ROUTES = {
  SIGN_UP: `${API_URL}/auth/signup`,
  SIGN_IN: `${API_URL}/api/Login/Login`,
  GET_USER: `${API_URL}/api/Login/GetUser`,
  LOAD_ADMINS: `${API_URL}/api/Values/LoadAdmins`,
}

export const APP_ROUTES = {
  SIGN_UP: '/signup',
  SIGN_IN: '/signin',
  DASHBOARD: '/dashboard',
}