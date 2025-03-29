import React from 'react';
import { BrowserRouter as Router, Routes, Route, Navigate } from 'react-router-dom';
import { Provider } from 'react-redux';
import { store } from './store/store';
import SignUp from './components/auth/SignUp';
import Login from './components/auth/Login';
import ProtectedRoute from './components/auth/ProtectedRoute';

const App: React.FC = () => {
  return (
    <Provider store={store}>
      <Router>
        <Routes>
          <Route path="/signup" element={<SignUp />} />
          <Route path="/login" element={<Login />} />
          <Route
            path="/dashboard"
            element={
              <ProtectedRoute>
                <div>Dashboard (Protected Route)</div>
              </ProtectedRoute>
            }
          />
          <Route
            path="/admin"
            element={
              <ProtectedRoute allowedRoles={['Admin']}>
                <div>Admin Dashboard (Admin Only)</div>
              </ProtectedRoute>
            }
          />
          <Route path="/unauthorized" element={<div>Unauthorized Access</div>} />
          <Route path="/" element={<Navigate to="/dashboard" replace />} />
        </Routes>
      </Router>
    </Provider>
  );
};

export default App;
