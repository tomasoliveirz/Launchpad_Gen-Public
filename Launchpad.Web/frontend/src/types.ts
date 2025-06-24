export interface ContractConfig {
    name: string;
    symbol: string;
    decimals: number;
    totalSupply: number;
    premint: number;
    hasMinting: boolean;
    hasBurning: boolean;
    isPausable: boolean;
    hasTax: boolean;
    taxFee: number;
    taxRecipients: TaxRecipient[];
    hasAccessControl: boolean;
    accessControlType: AccessControlType;
    roles?: string[];
}

export interface TaxRecipient {
    id: number;
    address: string;
    share: number;
}

export enum AccessControlType {
    Ownable = 0,
}

export interface ApiTaxRecipient {
    address: string;
    share: number;
}

export interface GenerateRequest {
    name: string;
    symbol: string;
    decimals: number;
    premint: number;
    supply: number;
    hasMinting: boolean;
    hasBurning: boolean;
    isPausable: boolean;
    hasTax: boolean;
    taxFee: number;
    taxRecipients?: ApiTaxRecipient[];
    hasAccessControl: boolean;
    accessControlType: AccessControlType;
    roles?: string[];
}

export interface ApiResponse {
    success: boolean;
    code?: string;
    error?: string;
}

export type Page = 'home' | 'generate' | 'about';

export interface NavigationProps {
    onNavigate: (page: Page) => void;
}