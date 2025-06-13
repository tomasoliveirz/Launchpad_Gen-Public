export interface TaxRecipient {
    id: number;
    address: string;
    share: number;
}

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
    accessControlType: number;
}

export type Page = 'home' | 'generate' | 'about';

export interface NavigationProps {
    onNavigate: (page: Page) => void;
}