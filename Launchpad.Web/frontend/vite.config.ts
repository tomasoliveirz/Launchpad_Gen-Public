import { defineConfig } from 'vite'
import react from '@vitejs/plugin-react'

// https://vite.dev/config/
export default defineConfig({
  plugins: [react()],
  server: {
    proxy: {
      '/api': {
        target: 'http://localhost:5000', // Porta do seu backend
        changeOrigin: true,
        secure: false
      }
    }
  },
  build: {
    outDir: '../wwwroot', // Build diretamente para wwwroot do backend
    emptyOutDir: true,    // Limpa a pasta antes do build
    rollupOptions: {
      output: {
        manualChunks: undefined,
      },
    },
  }
})