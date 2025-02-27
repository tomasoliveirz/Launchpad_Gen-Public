import { StrictMode } from 'react'
import { createRoot } from 'react-dom/client'
import './index.css'
import App from './App.tsx'
import { Provider } from './components/ui/provider.tsx'
import { Provider as ReduxProvider } from 'react-redux'
import theme from "./theme/theme.ts"
import { store } from './store/store.ts'
import { ChakraProvider } from '@chakra-ui/react'

createRoot(document.getElementById('root')!).render(
  <StrictMode>
    <Provider>
      <ReduxProvider store={store}>
      <ChakraProvider value={theme} >
        <App />
        </ChakraProvider>
      </ReduxProvider>
    </Provider>
  </StrictMode>,
)
