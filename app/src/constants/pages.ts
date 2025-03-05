import { NavigationItem } from "@/components/reUIsables/NavigationItem/navigation-item";
import { FaCog, FaHome, FaNetworkWired, FaPalette, FaQuestion } from "react-icons/fa";
import { IoGitBranchOutline } from "react-icons/io5";
import { RiFilePaper2Fill } from "react-icons/ri";


export const HomeNavigationItem = {
    label:"",
    icon:FaHome,
    url:"/",
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

export const ContractCharacteristicNavigationItem = {
    label:"Contract Characteristics",
    icon:FaPalette,
    url:"/settings/contract/characteristics",
}

export const ContractFeatureNavigationItem = {
    label:"Contract Features",
    icon:FaQuestion,
    url:"/settings/contract/features",
}

export const ContractTypeNavigationItem = {
    label:"Contract Types",
    icon:RiFilePaper2Fill,
    url:"/settings/contract/types",
}

export const ContractVariantNavigationItem = {
    label:"Contract Variants",
    icon:IoGitBranchOutline,
    url:"/settings/contract/variants",
}


export const BlockchainNetworkDetailNavigationItem = {
    label:"",
    icon:FaNetworkWired,
    url:"/settings/blockchain/networks/uuid",
}

export const ContractCharacteristicDetailNavigationItem = {
    label:"",
    icon:FaPalette,
    url:"/settings/contract/characteristics/uuid",
}

export const ContractFeatureDetailNavigationItem = {
    label:"",
    icon:FaQuestion,
    url:"/settings/contract/features/uuid",
}

export const ContractTypeDetailNavigationItem = {
    label:"",
    icon:RiFilePaper2Fill,
    url:"/settings/contract/types/uuid",
}

export const ContractVariantDetailNavigationItem = {
    label:"",
    icon:IoGitBranchOutline,
    url:"/settings/contract/variants/uuid",
}

export const pages:NavigationItem[] = [
    HomeNavigationItem,
    SettingsNavigationItem,
    BlockchainNetworkNavigationItem,
    ContractCharacteristicNavigationItem,
    ContractFeatureNavigationItem,
    ContractTypeNavigationItem,
    ContractVariantNavigationItem,
    BlockchainNetworkDetailNavigationItem,
    ContractCharacteristicDetailNavigationItem,
    ContractFeatureDetailNavigationItem,
    ContractTypeDetailNavigationItem,
    ContractVariantDetailNavigationItem
]

