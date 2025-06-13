import { useState } from 'react';
import type { ContractConfig, TaxRecipient } from '../types';

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
    taxFee: 2.5,
    taxRecipients: [],
    hasAccessControl: true,
    accessControlType: 0
};

export const useContractConfig = () => {
    const [config, setConfig] = useState<ContractConfig>(initialConfig);
    const [nextRecipientId, setNextRecipientId] = useState(1);

    const updateConfig = (updates: Partial<ContractConfig>) => {
        setConfig(prev => ({ ...prev, ...updates }));
    };

    const addTaxRecipient = () => {
        const newRecipient: TaxRecipient = {
            id: nextRecipientId,
            address: '',
            share: 0
        };
        setConfig(prev => ({
            ...prev,
            taxRecipients: [...prev.taxRecipients, newRecipient]
        }));
        setNextRecipientId(prev => prev + 1);
    };

    const removeTaxRecipient = (id: number) => {
        setConfig(prev => ({
            ...prev,
            taxRecipients: prev.taxRecipients.filter(r => r.id !== id)
        }));
    };

    const updateTaxRecipient = (id: number, field: 'address' | 'share', value: string | number) => {
        setConfig(prev => ({
            ...prev,
            taxRecipients: prev.taxRecipients.map(r =>
                r.id === id ? { ...r, [field]: value } : r
            )
        }));
    };

    return {
        config,
        updateConfig,
        addTaxRecipient,
        removeTaxRecipient,
        updateTaxRecipient
    };
};