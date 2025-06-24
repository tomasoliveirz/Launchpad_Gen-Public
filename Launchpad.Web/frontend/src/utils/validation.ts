import type { ContractConfig } from '../types';

export interface ValidationErrors {
    [key: string]: string;
}

export const validateContractConfig = (config: ContractConfig): ValidationErrors => {
    const errors: ValidationErrors = {};

    if (!config.name.trim()) {
        errors.name = 'Token name is required';
    }

    if (!config.symbol.trim()) {
        errors.symbol = 'Token symbol is required';
    } else if (config.symbol.length > 10) {
        errors.symbol = 'Token symbol should not exceed 10 characters';
    }

    if (config.decimals < 0 || config.decimals > 18) {
        errors.decimals = 'Decimals must be between 0 and 18';
    }

    if (config.totalSupply <= 0) {
        errors.totalSupply = 'Total supply must be greater than 0';
    }

    if (config.premint < 0) {
        errors.premint = 'Initial mint cannot be negative';
    } else if (config.premint > config.totalSupply) {
        errors.premint = 'Initial mint cannot exceed total supply';
    }

    if (config.hasTax) {
        if (config.taxFee < 0 || config.taxFee > 100) {
            errors.taxFee = 'Tax rate must be between 0% and 100%';
        }
    }

    return errors;
};

export const isValidEthereumAddress = (address: string): boolean => {
    return /^0x[a-fA-F0-9]{40}$/.test(address);
};

export const formatError = (errors: string[]): string => {
    if (errors.length === 0) return '';

    return `Please fix the following errors:\n\n${errors.map((error: string, index: number) => `${index + 1}. ${error}`).join('\n')}`;
};