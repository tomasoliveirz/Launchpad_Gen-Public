import { Box, HStack } from "@chakra-ui/react"
import { BrowserRouter, Route, Routes } from "react-router-dom"
import SideMenu from "./components/launchpad/side-menu/side-menu"
import ContractGeneratorPage from "./pages/ContractGeneratorPage/ContractGeneratorPage"
import AppGeneratorPage from "./pages/AppGeneratorPage/AppGeneratorPage"
import ContractManagerPage from "./pages/ContractManagerPage/ContractManagerPage"
import ContractPublisherPage from "./pages/ContractPublisherPage/ContractPublisherPage"
import HomePage from "./pages/HomePage/HomePage"
import ContractCharacteristicsPage from "./pages/SettingsPages/ContractCharacteristicsPage/ContractCharacteristicsPage"
import ContractTypesPage from "./pages/SettingsPages/ContractTypesPage/ContractTypesPage"
import SettingsMenuPage from "./pages/SettingsPages/SettingsMenuPage/SettingsMenuPage"
import ContractVariantsPage from "./pages/SettingsPages/ContractVariantsPage/ContractVariantsPage"
import BlockchainNetworksPage from "./pages/SettingsPages/BlockchainNetworksPage/BlockchainNetworksPage"
import ContractFeaturesPage from "./pages/SettingsPages/ContractFeaturesPage/ContractFeaturesPage"
import ContractTypeDetailsPage from "./pages/SettingsPages/DetailsPages/ContractTypeDetailsPage"
import ContractCharacteristicDetailsPage from "./pages/SettingsPages/DetailsPages/ContractCharacteristicDetailsPage"
import ContractVariantDetailsPage from "./pages/SettingsPages/DetailsPages/ContractVariantDetailsPage"
import BlockchainNetworkDetailsPage from "./pages/SettingsPages/DetailsPages/BlockchainNetworkDetailsPage"
import { Toaster } from "./components/ui/toaster"

function App() {

  return (
    <Box w="100vw" h="100vh" bg="#353941">
      <BrowserRouter>
        <HStack h="100%" w="100%">
          <SideMenu/>
          <Box h="100vh" w="100%" >
            <Routes>
              <Route element={<ContractGeneratorPage/>} path="/contract/generate"/>
              <Route element={<ContractPublisherPage/>} path="/contract/publish"/>
              <Route element={<AppGeneratorPage/>} path="/app/generate"/>
              <Route element={<ContractManagerPage/>} path="/contract/manage"/>
              <Route element={<SettingsMenuPage/>} path="/settings" />
              <Route element={<ContractTypesPage/>} path="/settings/contract/types"/>
              <Route element={<ContractCharacteristicsPage/>} path="/settings/contract/characteristics"/>
              <Route element={<ContractVariantsPage/>} path="/settings/contract/variants"/>
              <Route element={<ContractFeaturesPage/>} path="/settings/contract/features"/>
              <Route element={<BlockchainNetworksPage/>} path="/settings/blockchain/networks"/>

              <Route element={<ContractTypeDetailsPage/>} path="/settings/contract/types/:uuid"/>
              <Route element={<ContractCharacteristicDetailsPage/>} path="/settings/contract/characteristics/:uuid"/>
              <Route element={<ContractVariantDetailsPage/>} path="/settings/contract/variants/:uuid"/>
              <Route element={<BlockchainNetworkDetailsPage/>} path="/settings/blockchain/networks/:uuid"/>

              <Route path="/" element={<HomePage/>}/>
            </Routes>
          </Box>
        </HStack>
        
      </BrowserRouter>
      <Toaster/>
    </Box>
  )
}

export default App
