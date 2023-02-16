
import { useState, useEffect } from 'react';
import { getAuthenticatedUser } from './common';
import { APP_ROUTES } from '../utils/constants';
import { useNavigate } from 'react-router-dom';

export function useUser() {
  const [name, setName] = useState(null);
  const [email, setEmail] = useState(null);
  const [rights, setRights] = useState(null);
  const [authenticated, setAutenticated] = useState(false);
  const navigate = useNavigate();

  useEffect(() => {
    async function getUserDetails() {
      const { authenticated, name, email, rights } = await getAuthenticatedUser();
      console.log(await getAuthenticatedUser())
      if (!authenticated) {
        navigate(APP_ROUTES.SIGN_IN);
        return;
      }
      setName(name);
      setEmail(email);
      setRights(rights);
      setAutenticated(authenticated);
    }
    getUserDetails();
  }, []);

  return { name, email, authenticated, rights };
}