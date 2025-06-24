import type { ContractConfig, GenerateRequest, ApiResponse } from '../types';

const mapConfigToRequest = (config: ContractConfig): GenerateRequest => {
    return {
        name: config.name,
        symbol: config.symbol,
        decimals: config.decimals,
        premint: config.premint,
        supply: config.totalSupply,
        hasMinting: config.hasMinting,
        hasBurning: config.hasBurning,
        isPausable: config.isPausable,
        hasTax: config.hasTax,
        taxFee: config.taxFee,
        hasAccessControl: config.hasAccessControl,
        accessControlType: config.accessControlType,
        roles: config.roles
    };
};

export const generateSolidityContract = async (config: ContractConfig): Promise<string> => {
    try {
        const request = mapConfigToRequest(config);

        const response = await fetch('/api/generate', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(request)
        });

        if (!response.ok) {
            const errorData = await response.json().catch(() => null);
            throw new Error(errorData?.error || `HTTP error! status: ${response.status}`);
        }

        const data: ApiResponse = await response.json();

        if (!data.success || !data.code) {
            throw new Error(data.error || 'Failed to generate contract');
        }

        return data.code;
    } catch (error) {
        console.error('Error generating contract:', error);
        if (error instanceof Error) {
            throw error;
        }
        throw new Error('Failed to generate contract. Please try again.');
    }
};

export const downloadContract = (code: string, filename: string) => {
    const blob = new Blob([code], { type: 'text/plain' });
    const url = URL.createObjectURL(blob);

    const link = document.createElement('a');
    link.href = url;
    link.download = `${filename}.sol`;
    document.body.appendChild(link);
    link.click();
    document.body.removeChild(link);

    URL.revokeObjectURL(url);
};