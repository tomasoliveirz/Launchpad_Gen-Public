import { Box, HStack } from "@chakra-ui/react"
import { BrowserRouter, Route, Routes } from "react-router-dom"
import SideMenu from "./components/launchpad/side-menu/side-menu"
import ContractGeneratorPage from "./pages/ContractGeneratorPage/ContractGeneratorPage"
import AppGeneratorPage from "./pages/AppGeneratorPage/AppGeneratorPage"
import ContractManagerPage from "./pages/ContractManagerPage/ContractManagerPage"
import ContractPublisherPage from "./pages/ContractPublisherPage/ContractPublisherPage"

function App() {

  return (
    <Box w="100vw" h="100vh">
      <BrowserRouter>
        <HStack h="100%" w="100%">
          <SideMenu/>
          <Box h="100vh" w="100%" >
            <Routes>
              <Route element={<ContractGeneratorPage/>} path="/contract/generate"/>
              <Route element={<ContractPublisherPage/>} path="/contract/publish"/>
              <Route element={<AppGeneratorPage/>} path="/app/generate"/>
              <Route element={<ContractManagerPage/>} path="/contract/manage"/>
              <Route path="/" element={<HomePage/>}/>
            </Routes>
          </Box>
        </HStack>
        
      </BrowserRouter>
      
    </Box>
  )
}

export default App
