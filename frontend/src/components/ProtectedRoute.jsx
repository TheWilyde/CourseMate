import {Navigate} from 'react-router-dom';
import {isAuthenticated, getUserRole} from '../utils/auth';

const ProtectedRoute = ({children, allowedRoles = []}) => {
  const authenticated = isAuthenticated();
  const userRole = getUserRole();

  // Not authenticated - redirect to login
  if (!authenticated) {
    return <Navigate to="/" replace />;
  }

  // Check if user has required role
  if (allowedRoles.length > 0 && !allowedRoles.includes(userRole)) {
    // User doesn't have required role - redirect to their dashboard
    switch (userRole) {
      case 'Student':
        return <Navigate to="/student/dashboard" replace />;
      case 'Instructor':
        return <Navigate to="/instructor/dashboard" replace />;
      case 'Admin':
        return <Navigate to="/admin/dashboard" replace />;
      default:
        return <Navigate to="/" replace />;
    }
  }

  // User is authenticated and has correct role
  return children;
};

export default ProtectedRoute;
