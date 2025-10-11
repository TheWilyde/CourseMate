import {getUser, logout} from '../../utils/auth';

const StudentDashboard = () => {
  const user = getUser();

  return (
    <div className="min-h-screen bg-gray-50">
      <div className="bg-white shadow">
        <div className="max-w-7xl mx-auto px-4 py-6 flex justify-between items-center">
          <h1 className="text-2xl font-bold text-gray-900">
            Student Dashboard
          </h1>
          <button
            onClick={logout}
            className="px-4 py-2 bg-red-500 text-white rounded hover:bg-red-600">
            Logout
          </button>
        </div>
      </div>

      <div className="max-w-7xl mx-auto px-4 py-8">
        <div className="bg-white rounded-lg shadow p-6">
          <h2 className="text-xl font-semibold mb-4">
            Welcome, {user?.FirstName}!
          </h2>
          <div className="space-y-2 text-gray-600">
            <p>
              <strong>Email:</strong> {user?.Email}
            </p>
            <p>
              <strong>Major:</strong> {user?.Major}
            </p>
            <p>
              <strong>Year:</strong> {user?.Year}
            </p>
          </div>
        </div>

        <div className="mt-6 bg-white rounded-lg shadow p-6">
          <h3 className="text-lg font-semibold mb-4">Your Courses</h3>
          <p className="text-gray-600">Coming soon...</p>
        </div>
      </div>
    </div>
  );
};

export default StudentDashboard;
