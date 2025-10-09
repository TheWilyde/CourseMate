import {defineConfig} from 'vite';
import react from '@vitejs/plugin-react';
import tailwindcss from '@tailwindcss/vite';

export default defineConfig({
  plugins: [react(), tailwindcss()],
  server: {
    proxy: {
      '/api': {
        target: 'http://localhost:5219/',  // HTTP backend (use https://localhost:7281/ for HTTPS)
        changeOrigin: true,
      },
    },
  },
});
