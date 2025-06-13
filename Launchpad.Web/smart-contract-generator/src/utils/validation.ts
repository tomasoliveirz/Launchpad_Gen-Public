import type { ContractConfig } from '../types';

export const validateContractConfig = (config: ContractConfig): string[] => {
    const errors: string[] = [];

    if (!config.name.trim()) {
        errors.push('Token name is required');
    }

    if (!config.symbol.trim()) {
        errors.push('Token symbol is required');
    }

    if (config.symbol.length > 10) {
        errors.push('Token symbol should not exceed 10 characters');
    }

    if (config.decimals < 0 || config.decimals > 18) {
        errors.push('Decimals must be between 0 and 18');
    }

    if (config.totalSupply <= 0) {
        errors.push('Total supply must be greater than 0');
    }

    if (config.premint < 0) {
        errors.push('Initial mint cannot be negative');
    }

    if (config.premint > config.totalSupply) {
        errors.push('Initial mint cannot exceed total supply');
    }

    if (config.hasTax) {
        if (config.taxFee < 0 || config.taxFee > 100) {
            errors.push('Tax rate must be between 0% and 100%');
        }

        const totalShares = config.taxRecipients.reduce((sum: number, r: any) => sum + r.share, 0);

        if (config.taxRecipients.length === 0) {
            errors.push('At least one tax recipient is required when tax is enabled');
        } else if (totalShares > 100) {
            errors.push('Total tax recipient shares cannot exceed 100%');
        }

        config.taxRecipients.forEach((recipient: any, index: number) => {
            if (!recipient.address.trim()) {
                errors.push(`Tax recipient ${index + 1}: Address is required`);
            } else if (!isValidEthereumAddress(recipient.address)) {
                errors.push(`Tax recipient ${index + 1}: Invalid Ethereum address`);
            }

            if (recipient.share <= 0) {
                errors.push(`Tax recipient ${index + 1}: Share must be greater than 0%`);
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