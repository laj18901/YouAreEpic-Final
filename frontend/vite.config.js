import { defineConfig, loadEnv } from 'vite'
import vue from '@vitejs/plugin-vue'

// https://vitejs.dev/config/
export default defineConfig(({command, mode}) => {
    const env = loadEnv(mode,process.cwd());
    const apiUri = env.VITE_API;
 return {
    plugins: [vue()],
    build:{
      outDir: "../backend/YouAreEpic.Backend/wwwroot"
    },
    server: {
      port: 3000,
      proxy: {
          '/api': {
              secure: false,
              target: apiUri
          }
      }
  }
 };
})
