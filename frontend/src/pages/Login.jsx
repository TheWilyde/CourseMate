import {useState} from 'react';
import Logo from '../assets/CourseMateLogo.svg';
import Input from '../components/Input.jsx';

const Login = () => {
  return (
    <div className="h-screen flex justify-center items-center bg-gunmetal">
      <div className="w-[25rem] h-[31rem] rounded-2xl bg-white flex justify-center py-8 shadow-[0_8px_20px_rgba(14,47,63,0.25)]">
        <div className="w-[22.5rem] h-full flex gap-12 flex-col items-center">
          <div className="w-full h-12 flex items-center justify-center">
            <img src={Logo} alt="CourseMate Logo" width="250" />
          </div>
          <div className="w-full h-full">
            <div className="w-full flex flex-col gap-6 items-center">
              <p className="font-medium text-[18px]">Sign in to your account</p>
              <LoginForm />
            </div>
          </div>
        </div>
      </div>
    </div>
  );
};

export default Login;

function LoginForm() {
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');

  const handleSubmit = async (e) => {
    e.preventDefault(); // prevent default browser POST

    const res = await fetch('/api/Login/auth', {
      method: 'POST',
      headers: {'Content-Type': 'application/json'},
      body: JSON.stringify({email, password}),
    });

    if (res.ok) {
      console.log('Login successful');
      // handle redirect, token storage, etc.
    } else if (res.status === 400) {
      console.error('Invalid credentials');
    } else {
      console.error('Login failed');
    }
  };
  return (
    <form onSubmit={handleSubmit} className="w-full h-full flex flex-col gap-4">
      <Input
        label="Email"
        type="email"
        placeholder="Enter your email"
        onChange={(e) => setEmail(e.target.value)}
        required={true}
      />
      <Input
        label="Password"
        type="password"
        placeholder="Enter your password"
        onChange={(e) => setPassword(e.target.value)}
        required={true}
      />
      <div className="flex items-center gap-2">
        <label className="relative inline-block w-10 h-6 ">
          <input type="checkbox" className="absolute opacity-0 w-0 h-0 peer" />
          <span
            className="border-none absolute inset-0 cursor-pointer rounded-full bg-gray-300 transition-colors duration-300
           peer-checked:bg-teal 
           before:content-[''] before:absolute before:left-1 before:top-1 before:bottom-1
           before:aspect-square before:bg-white before:rounded-full before:transition-transform before:duration-300
           peer-checked:before:translate-x-4"></span>
        </label>
        <span className="text-sm">Remember me</span>
      </div>
      <button className="appearance-none w-[22.5rem] h-12 bg-teal rounded text-white font-medium mt-4">
        Sign in
      </button>
    </form>
  );
}
