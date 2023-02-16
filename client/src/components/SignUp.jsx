import React from 'react';
import axios from 'axios';
import { useState } from 'react';
import { API_ROUTES, APP_ROUTES } from '../utils/constants';
import { Link, useNavigate } from 'react-router-dom';
import { Field, Formik, Form } from "formik";
import * as Yup from "yup";
import Button from '@mui/material/Button';
import CssBaseline from '@mui/material/CssBaseline';
import TextField from '@mui/material/TextField';
import Grid from '@mui/material/Grid';
import Box from '@mui/material/Box';
import Typography from '@mui/material/Typography';
import Container from '@mui/material/Container';
import { createTheme, ThemeProvider } from '@mui/material/styles';
import { MuiFileInput } from 'mui-file-input'
import { Upload } from '@mui/icons-material';
const SignUp = () => {
  const navigate = useNavigate()

  function Copyright(props) {
    return (
      <Typography variant="body2" color="text.secondary" align="center" {...props}>
        {'Copyright © '}
        {new Date().getFullYear()}
        {'.'}
      </Typography>
    );
  }
  const theme = createTheme();
  const [file, setFile] = React.useState(null)

  const handleChange = (newFile) => {
    setFile(newFile)
  }
  const uploadFile = async (values) => {
    try {
        const data = new FormData();
        data.append('file', file);
        await axios({
            method: 'post',
            url: API_ROUTES.LOAD_ADMINS,
            data: {data}
            ,
            headers: {'content-type': 'multipart/form-data'}   });

        navigate(APP_ROUTES.SIGN_IN);
      }
      catch (err) {
        console.log(err.response);
      }
  
    console.log(file);
  }
  return (
   <ThemeProvider theme={theme}>
      <Container component="main" maxWidth="xs">
        <CssBaseline />
        <Box
          sx={{
            marginTop: 8,
            display: 'flex',
            flexDirection: 'column',
            alignItems: 'center',
          }}
        >
          <Typography component="h1" variant="h5">
          Upload txt admins list
          </Typography>
         
    <MuiFileInput value={file} onChange={handleChange} />
    <Grid item xs={12}>
              <Button onClick={uploadFile} type="submit"
              fullWidth
              variant="contained"
              sx={{ mt: 3, mb: 2 }}>
                Upload File
              </Button>
            </Grid>
    <Grid container>
              <Grid item xs>
              </Grid>
              <Grid item>
              <Link to="/signin">
                  {"Already a User? Sign Up"}
                </Link>
              </Grid>
            </Grid>
            </Box>

        <Copyright sx={{ mt: 8, mb: 4 }} />
      </Container>
    </ThemeProvider>
   );
}

export default SignUp;