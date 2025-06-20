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

        const totalShares = config.taxRecipients.reduce((sum: number, r: any) => sum + r.share, 0);

        if (config.taxRecipients.length === 0) {
            errors.taxRecipients = 'At least one tax recipient is required when tax is enabled';
        } else if (totalShares > 100) {
            errors.taxRecipients = 'Total tax recipient shares cannot exceed 100%';
        }

        config.taxRecipients.forEach((recipient: any, index: number) => {
            if (!recipient.address.trim()) {
                errors[`taxRecipient${index}Address`] = 'Address is required';
            } else if (!isValidEthereumAddress(recipient.address)) {
                errors[`taxRecipient${index}Address`] = 'Invalid Ethereum address';
            }

            if (recipient.share <= 0) {
                errors[`taxRecipient${index}Share`] = 'Share must be greater than 0%';
            }
        });
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