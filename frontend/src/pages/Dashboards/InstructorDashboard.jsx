import {getUser, logout} from '../../utils/auth';

const InstructorDashboard = () => {
  const user = getUser();

  return (
    <div className="min-h-screen bg-gray-50">
      <div className="bg-white shadow">
        <div className="max-w-7xl mx-auto px-4 py-6 flex justify-between items-center">
          <h1 className="text-2xl font-bold text-gray-900">
            Instructor Dashboard
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
            Welcome, Prof. {user?.LastName}!
          </h2>
          <div className="space-y-2 text-gray-600">
            <p>
              <strong>Email:</strong> {user?.Email}
            </p>
            <p>
              <strong>Name:</strong> {user?.FirstName} {user?.LastName}
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

export default InstructorDashboard;
