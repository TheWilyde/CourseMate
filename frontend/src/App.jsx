import {BrowserRouter, Routes, Route, Navigate} from 'react-router-dom';
import Login from './pages/Login.jsx';
import StudentDashboard from './pages/Dashboards/StudentDashboard.jsx';
import InstructorDashboard from './pages/Dashboards/InstructorDashboard.jsx';
import AdminDashboard from './pages/Dashboards/AdminDashboard.jsx';
import ProtectedRoute from './components/ProtectedRoute.jsx';
import {isAuthenticated, getUserRole} from './utils/auth.js';

function App() {
  // Redirect to dashboard if already logged in
  const RedirectIfAuthenticated = ({children}) => {
    if (isAuthenticated()) {
      const role = getUserRole();
      switch (role) {
        case 'Student':
          return <Navigate to="/student/dashboard" replace />;
        case 'Instructor':
          return <Navigate to="/instructor/dashboard" replace />;
        case 'Admin':
          return <Navigate to="/admin/dashboard" replace />;
        default:
          return children;
      }
    }
    return children;
  };

  return (
    <BrowserRouter>
      <Routes>
        {/* Login Route */}
        <Route
          path="/"
          element={
            <RedirectIfAuthenticated>
              <Login />
            </RedirectIfAuthenticated>
          }
        />

        {/* Student Routes */}
        <Route
          path="/student/dashboard"
          element={
            <ProtectedRoute allowedRoles={['Student']}>
              <StudentDashboard />
            </ProtectedRoute>
          }
        />

        {/* Instructor Routes */}
        <Route
          path="/instructor/dashboard"
          element={
            <ProtectedRoute allowedRoles={['Instructor']}>
              <InstructorDashboard />
            </ProtectedRoute>
          }
        />

        {/* Admin Routes */}
        <Route
          path="/admin/dashboard"
          element={
            <ProtectedRoute allowedRoles={['Admin']}>
              <AdminDashboard />
            </ProtectedRoute>
          }
        />

        {/* Catch all - redirect to home */}
        <Route path="*" element={<Navigate to="/" replace />} />
      </Routes>
    </BrowserRouter>
  );
}

export default App;
