import { defineConfig } from 'vite'
import react from '@vitejs/plugin-react'

// https://vite.dev/config/
export default defineConfig({
  plugins: [react()],
  
  test:{
    environment: "jsdom", // Simula un navegador
    globals: true, // Permite usar describe(), it(), expect() sin importar
  },
  
})
