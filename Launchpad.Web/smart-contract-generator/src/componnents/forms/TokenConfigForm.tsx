import React from 'react';
import { FormSectionProps } from '@/types';

const TokenConfigForm: React.FC<FormSectionProps> = ({ config, updateConfig }) => {
    return (
        <div className="bg-slate-800 rounded-lg p-6 mb-6 border border-slate-700">
            <h2 className="text-xl font-semibold mb-4 pb-2 border-b border-slate-700">
                Token Configuration
            </h2>

            <div className="grid grid-cols-1 md:grid-cols-2 gap-4 mb-4">
                <div>
                    <label htmlFor="name" className="block text-sm font-medium mb-2">
                        Token Name
                    </label>
                    <input
                        id="name"
                        type="text"
                        value={config.name}
                        onChange={(e) => updateConfig({ name: e.target.value })}
                        className="w-full bg-slate-700 border border-slate-600 rounded px-3 py-2 focus:ring-2 focus:ring-blue-500 focus:border-transparent"
                        placeholder="MyToken"
                    />
                </div>

                <div>
                    <label htmlFor="symbol" className="block text-sm font-medium mb-2">
                        Symbol
                    </label>
                    <input
                        id="symbol"
                        type="text"
                        value={config.symbol}
                        onChange={(e) => updateConfig({ symbol: e.target.value.toUpperCase() })}
                        className="w-full bg-slate-700 border border-slate-600 rounded px-3 py-2 focus:ring-2 focus:ring-blue-500 focus:border-transparent"
                        placeholder="MTK"
                        maxLength={10}
                    />
                </div>
            </div>

            <div className="grid grid-cols-1 md:grid-cols-3 gap-4">
                <div>
                    <label htmlFor="decimals" className="block text-sm font-medium mb-2">
                        Decimals
                    </label>
                    <input
                        id="decimals"
                        type="number"
                        min="0"
                        max="18"
                        value={config.decimals}
                        onChange={(e) => updateConfig({ decimals: parseInt(e.target.value) || 0 })}
                        className="w-full bg-slate-700 border border-slate-600 rounded px-3 py-2 focus:ring-2 focus:ring-blue-500 focus:border-transparent"
                    />
                </div>

                <div>
                    <label htmlFor="totalSupply" className="block text-sm font-medium mb-2">
                        Total Supply
                    </label>
                    <input
                        id="totalSupply"
                        type="number"
                        min="0"
                        value={config.totalSupply}
                        onChange={(e) => updateConfig({ totalSupply: parseInt(e.target.value) || 0 })}
                        className="w-full bg-slate-700 border border-slate-600 rounded px-3 py-2 focus:ring-2 focus:ring-blue-500 focus:border-transparent"
                    />
                </div>

                <div>
                    <label htmlFor="premint" className="block text-sm font-medium mb-2">
                        Initial Mint
                    </label>
                    <input
                        id="premint"
                        type="number"
                        min="0"
                        value={config.premint}
                        onChange={(e) => updateConfig({ premint: parseInt(e.target.value) || 0 })}
                        className="w-full bg-slate-700 border border-slate-600 rounded px-3 py-2 focus:ring-2 focus:ring-blue-500 focus:border-transparent"
                    />
                </div>
            </div>
        </div>
    );
};

export default TokenConfigForm;