import { useState } from 'react';
import type { ContractConfig, AccessControlType } from '../types';

const initialConfig: ContractConfig = {
    name: 'MyToken',
    symbol: 'MTK',
    decimals: 18,
    totalSupply: 1000000,
    premint: 100000,
    hasMinting: false,
    hasBurning: false,
    isPausable: false,
    hasTax: false,
    taxFee: 2,
    hasAccessControl: false,
    accessControlType: 0 as AccessControlType,
    roles: []
};

export const useContractConfig = () => {
    const [config, setConfig] = useState<ContractConfig>(initialConfig);

    const updateConfig = (updates: Partial<ContractConfig>) => {
        setConfig(prev => ({ ...prev, ...updates }));
    };

    return {
        config,
        updateConfig
    };
};