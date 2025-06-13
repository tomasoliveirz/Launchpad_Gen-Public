import React from 'react';
import { FormSectionProps } from '@/types';

const TokenFeaturesForm: React.FC<FormSectionProps> = ({ config, updateConfig }) => {
    const features = [
        {
            key: 'hasMinting' as const,
            label: 'Mintable',
            description: 'Allow minting new tokens'
        },
        {
            key: 'hasBurning' as const,
            label: 'Burnable',
            description: 'Allow burning tokens'
        },
        {
            key: 'isPausable' as const,
            label: 'Pausable',
            description: 'Emergency stop functionality'
        }
    ];

    return (
        <div className="bg-slate-800 rounded-lg p-6 mb-6 border border-slate-700">
            <h2 className="text-xl font-semibold mb-4 pb-2 border-b border-slate-700">
                Token Features
            </h2>

            <div className="space-y-3">
                {features.map(({ key, label, description }) => (
                    <label
                        key={key}
                        className="flex items-center gap-3 p-3 bg-slate-700/50 rounded border border-slate-600 hover:bg-slate-700 transition-colors cursor-pointer"
                    >
                        <input
                            type="checkbox"
                            checked={config[key]}
                            onChange={(e) => updateConfig({ [key]: e.target.checked })}
                            className="w-4 h-4 text-blue-600 bg-slate-700 border-slate-600 rounded focus:ring-blue-500 focus:ring-2"
                        />
                        <div className="flex-1">
                            <div className="font-medium">{label}</div>
                            <div className="text-sm text-slate-400">{description}</div>
                        </div>
                    </label>
                ))}
            </div>
        </div>
    );
};

export default TokenFeaturesForm;