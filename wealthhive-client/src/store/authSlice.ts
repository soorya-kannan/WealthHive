import { createSlice, createAsyncThunk } from '@reduxjs/toolkit';
import axios from 'axios';

interface AuthState {
  user: {
    userId: string;
    email: string;
    username: string;
    role: string;
  } | null;
  token: string | null;
  loading: boolean;
  error: string | null;
}

const initialState: AuthState = {
  user: null,
  token: localStorage.getItem('token'),
  loading: false,
  error: null,
};

const API_URL = 'http://localhost:5266/api/auth';

export const signUp = createAsyncThunk(
  'auth/signUp',
  async (userData: { email: string; username: string; name: string; familyName: string }) => {
    const response = await axios.post(`${API_URL}/signup`, userData);
    return response.data;
  }
);

export const verifyOTP = createAsyncThunk(
  'auth/verifyOTP',
  async (otpData: { email: string; otp: string; password: string }) => {
    const response = await axios.post(`${API_URL}/verify-otp`, otpData);
    return response.data;
  }
);

export const login = createAsyncThunk(
  'auth/login',
  async (credentials: { email: string; password: string }) => {
    const response = await axios.post(`${API_URL}/login`, credentials);
    return response.data;
  }
);

export const getCurrentUser = createAsyncThunk(
  'auth/getCurrentUser',
  async (token: string) => {
    const response = await axios.get(`${API_URL}/me`, {
      headers: { Authorization: `Bearer ${token}` }
    });
    return response.data;
  }
);

const authSlice = createSlice({
  name: 'auth',
  initialState,
  reducers: {
    logout: (state) => {
      state.user = null;
      state.token = null;
      localStorage.removeItem('token');
    },
    clearError: (state) => {
      state.error = null;
    },
  },
  extraReducers: (builder) => {
    builder
      // SignUp
      .addCase(signUp.pending, (state) => {
        state.loading = true;
        state.error = null;
      })
      .addCase(signUp.fulfilled, (state) => {
        state.loading = false;
      })
      .addCase(signUp.rejected, (state, action) => {
        state.loading = false;
        state.error = action.error.message || 'Signup failed';
      })
      // Verify OTP
      .addCase(verifyOTP.pending, (state) => {
        state.loading = true;
        state.error = null;
      })
      .addCase(verifyOTP.fulfilled, (state, action) => {
        state.loading = false;
        state.token = action.payload.token;
        localStorage.setItem('token', action.payload.token);
      })
      .addCase(verifyOTP.rejected, (state, action) => {
        state.loading = false;
        state.error = action.error.message || 'OTP verification failed';
      })
      // Login
      .addCase(login.pending, (state) => {
        state.loading = true;
        state.error = null;
      })
      .addCase(login.fulfilled, (state, action) => {
        state.loading = false;
        state.token = action.payload.token;
        localStorage.setItem('token', action.payload.token);
      })
      .addCase(login.rejected, (state, action) => {
        state.loading = false;
        state.error = action.error.message || 'Login failed';
      })
      // Get Current User
      .addCase(getCurrentUser.pending, (state) => {
        state.loading = true;
        state.error = null;
      })
      .addCase(getCurrentUser.fulfilled, (state, action) => {
        state.loading = false;
        state.user = action.payload;
      })
      .addCase(getCurrentUser.rejected, (state, action) => {
        state.loading = false;
        state.error = action.error.message || 'Failed to get user data';
      });
  },
});

export const { logout, clearError } = authSlice.actions;
export default authSlice.reducer; 