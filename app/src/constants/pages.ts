import { NavigationItem } from "@/components/reUIsables/NavigationItem/navigation-item";
import { FaCog, FaHome, FaNetworkWired } from "react-icons/fa";


export const HomeNavigationItem = {
    label:"",
    icon:FaHome,
    url:"",
}

export const SettingsNavigationItem = {
    label:"Settings",
    icon:FaCog,
    url:"/settings",
}

export const BlockchainNetworkNavigationItem = {
    label:"Ethereum Networks",
    icon:FaNetworkWired,
    url:"/settings/blockchain/networks",
}

export const BlockchainNetworkDetailNavigationItem = {
    label:"",
    icon:FaNetworkWired,
    url:"/settings/blockchain/networks/uuid",
}

export const pages:NavigationItem[] = [
    HomeNavigationItem,
    SettingsNavigationItem,
    BlockchainNetworkNavigationItem,
    BlockchainNetworkDetailNavigationItem
]

