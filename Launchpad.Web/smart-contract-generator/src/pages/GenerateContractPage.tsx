import React, { useState } from 'react';
import { Code, Copy, Download, Check, Plus, Trash2 } from 'lucide-react';
import type { NavigationProps } from '../types';
import { useContractConfig } from '../hooks/useContractConfig';
import { validateContractConfig, formatError } from '../utils/validation';
import { generateSolidityContract, downloadContract } from '../utils/contractGenerator';
import { copyToClipboard } from '../utils/clipboard';

const GenerateContractPage: React.FC<NavigationProps> = ({ onNavigate }) => {
    const {
        config,
        updateConfig,
        addTaxRecipient,
        removeTaxRecipient,
        updateTaxRecipient
    } = useContractConfig();

    const [isGenerating, setIsGenerating] = useState(false);
    const [generatedCode, setGeneratedCode] = useState('');
    const [copySuccess, setCopySuccess] = useState(false);

    const showToast = (message: string) => {
        alert(message);
    };

    const handleGenerate = async () => {
        const errors = validateContractConfig(config);

        if (errors.length > 0) {
            showToast(formatError(errors));
            return;
        }

        setIsGenerating(true);

        setTimeout(() => {
            try {
                const code = generateSolidityContract(config);
                setGeneratedCode(code);
                showToast('Smart contract generated successfully!');

                setTimeout(() => {
                    document.getElementById('result-section')?.scrollIntoView({
                        behavior: 'smooth'
                    });
                }, 100);
            } catch (error) {
                showToast('Failed to generate contract. Please try again.');
            } finally {
                setIsGenerating(false);
            }
        }, 2000);
    };

    const handleCopy = async () => {
        const success = await copyToClipboard(generatedCode);
        if (success) {
            setCopySuccess(true);
            showToast('Contract copied to clipboard!');
            setTimeout(() => setCopySuccess(false), 2000);
        } else {
            showToast('Failed to copy to clipboard');
        }
    };

    const handleDownload = () => {
        try {
            const filename = config.name.replace(/\s+/g, '') || 'Contract';
            downloadContract(generatedCode, filename);
            showToast('Contract downloaded successfully!');
        } catch (error) {
            showToast('Failed to download contract');
        }
    };

    const handleTaxToggle = (enabled: boolean) => {
        updateConfig({ hasTax: enabled });
        if (enabled && config.taxRecipients.length === 0) {
            addTaxRecipient();
        }
    };

    const totalShares = config.taxRecipients.reduce((sum: number, r: any) => sum + r.share, 0);

    return (
        <div className="min-h-screen bg-slate-900 text-white">
            <div className="container mx-auto px-6 py-8">
                <div className="text-center mb-8">
                    <h1 className="text-4xl font-bold mb-2">Generate a Smart Contract</h1>
                    <p className="text-slate-400">
                        Fill in the form and customize your contract code as desired. What you see is what you get,
                        but that doesn't mean you can't make a change or two.
                    </p>
                </div>

                <div className="max-w-4xl mx-auto">
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

                    <div className="bg-slate-800 rounded-lg p-6 mb-6 border border-slate-700">
                        <h2 className="text-xl font-semibold mb-4 pb-2 border-b border-slate-700">
                            Token Features
                        </h2>

                        <div className="space-y-3">
                            <label className="flex items-center gap-3 p-3 bg-slate-700/50 rounded border border-slate-600 hover:bg-slate-700 transition-colors cursor-pointer">
                                <input
                                    type="checkbox"
                                    checked={config.hasMinting}
                                    onChange={(e) => updateConfig({ hasMinting: e.target.checked })}
                                    className="w-4 h-4 text-blue-600 bg-slate-700 border-slate-600 rounded focus:ring-blue-500 focus:ring-2"
                                />
                                <div className="flex-1">
                                    <div className="font-medium">Mintable</div>
                                    <div className="text-sm text-slate-400">Allow minting new tokens</div>
                                </div>
                            </label>

                            <label className="flex items-center gap-3 p-3 bg-slate-700/50 rounded border border-slate-600 hover:bg-slate-700 transition-colors cursor-pointer">
                                <input
                                    type="checkbox"
                                    checked={config.hasBurning}
                                    onChange={(e) => updateConfig({ hasBurning: e.target.checked })}
                                    className="w-4 h-4 text-blue-600 bg-slate-700 border-slate-600 rounded focus:ring-blue-500 focus:ring-2"
                                />
                                <div className="flex-1">
                                    <div className="font-medium">Burnable</div>
                                    <div className="text-sm text-slate-400">Allow burning tokens</div>
                                </div>
                            </label>

                            <label className="flex items-center gap-3 p-3 bg-slate-700/50 rounded border border-slate-600 hover:bg-slate-700 transition-colors cursor-pointer">
                                <input
                                    type="checkbox"
                                    checked={config.isPausable}
                                    onChange={(e) => updateConfig({ isPausable: e.target.checked })}
                                    className="w-4 h-4 text-blue-600 bg-slate-700 border-slate-600 rounded focus:ring-blue-500 focus:ring-2"
                                />
                                <div className="flex-1">
                                    <div className="font-medium">Pausable</div>
                                    <div className="text-sm text-slate-400">Emergency stop functionality</div>
                                </div>
                            </label>
                        </div>
                    </div>

                    <div className="bg-slate-800 rounded-lg p-6 mb-6 border border-slate-700">
                        <h2 className="text-xl font-semibold mb-4 pb-2 border-b border-slate-700">
                            Access Control
                        </h2>

                        <label className="flex items-center gap-3 p-3 bg-slate-700/50 rounded border border-slate-600 hover:bg-slate-700 transition-colors cursor-pointer mb-4">
                            <input
                                type="checkbox"
                                checked={config.hasAccessControl}
                                onChange={(e) => updateConfig({ hasAccessControl: e.target.checked })}
                                className="w-4 h-4 text-blue-600 bg-slate-700 border-slate-600 rounded focus:ring-blue-500 focus:ring-2"
                            />
                            <span className="font-medium">Enable Access Control</span>
                        </label>

                        {config.hasAccessControl && (
                            <div className="bg-slate-700/30 rounded p-4 border border-slate-600">
                                <label htmlFor="accessControlType" className="block text-sm font-medium mb-2">
                                    Control Type
                                </label>
                                <select
                                    id="accessControlType"
                                    value={config.accessControlType}
                                    onChange={(e) => updateConfig({ accessControlType: parseInt(e.target.value) })}
                                    className="w-full bg-slate-700 border border-slate-600 rounded px-3 py-2 focus:ring-2 focus:ring-blue-500 focus:border-transparent"
                                >
                                    <option value={0}>Ownable (Single Owner)</option>
                                    <option value={1}>Role-Based Access Control</option>
                                </select>
                            </div>
                        )}
                    </div>

                    <div className="bg-slate-800 rounded-lg p-6 mb-6 border border-slate-700">
                        <h2 className="text-xl font-semibold mb-4 pb-2 border-b border-slate-700">
                            Tax System
                        </h2>

                        <label className="flex items-center gap-3 p-3 bg-slate-700/50 rounded border border-slate-600 hover:bg-slate-700 transition-colors cursor-pointer mb-4">
                            <input
                                type="checkbox"
                                checked={config.hasTax}
                                onChange={(e) => handleTaxToggle(e.target.checked)}
                                className="w-4 h-4 text-blue-600 bg-slate-700 border-slate-600 rounded focus:ring-blue-500 focus:ring-2"
                            />
                            <span className="font-medium">Enable Transaction Tax</span>
                        </label>

                        {config.hasTax && (
                            <div className="space-y-4">
                                <div>
                                    <label htmlFor="taxFee" className="block text-sm font-medium mb-2">
                                        Tax Rate (%)
                                    </label>
                                    <input
                                        id="taxFee"
                                        type="number"
                                        min="0"
                                        max="100"
                                        step="0.1"
                                        value={config.taxFee}
                                        onChange={(e) => updateConfig({ taxFee: parseFloat(e.target.value) || 0 })}
                                        className="w-full bg-slate-700 border border-slate-600 rounded px-3 py-2 focus:ring-2 focus:ring-blue-500 focus:border-transparent"
                                    />
                                </div>

                                <div>
                                    <div className="flex justify-between items-center mb-3">
                                        <div>
                                            <h3 className="font-medium">Tax Recipients</h3>
                                            <p className="text-sm text-slate-400">
                                                Total allocated: {totalShares.toFixed(2)}%
                                                {totalShares > 100 && (
                                                    <span className="text-red-400 ml-2">⚠ Exceeds 100%</span>
                                                )}
                                            </p>
                                        </div>
                                        <button
                                            type="button"
                                            onClick={addTaxRecipient}
                                            className="flex items-center gap-2 px-3 py-1 bg-blue-600 hover:bg-blue-700 rounded text-sm transition-colors"
                                        >
                                            <Plus className="w-4 h-4" />
                                            Add Recipient
                                        </button>
                                    </div>

                                    <div className="space-y-3">
                                        {config.taxRecipients.length === 0 ? (
                                            <div className="text-center py-8 text-slate-400">
                                                <p>No tax recipients configured</p>
                                                <p className="text-sm">Click "Add Recipient" to start</p>
                                            </div>
                                        ) : (
                                            config.taxRecipients.map((recipient: any) => (
                                                <div key={recipient.id} className="grid grid-cols-1 md:grid-cols-3 gap-3 p-3 bg-slate-700/50 rounded border border-slate-600">
                                                    <div className="md:col-span-2">
                                                        <label className="block text-sm font-medium mb-1">
                                                            Recipient Address
                                                        </label>
                                                        <input
                                                            type="text"
                                                            value={recipient.address}
                                                            onChange={(e) => updateTaxRecipient(recipient.id, 'address', e.target.value)}
                                                            placeholder="0x..."
                                                            className="w-full bg-slate-700 border border-slate-600 rounded px-3 py-2 focus:ring-2 focus:ring-blue-500 focus:border-transparent text-sm"
                                                        />
                                                    </div>

                                                    <div className="flex gap-2">
                                                        <div className="flex-1">
                                                            <label className="block text-sm font-medium mb-1">
                                                                Share (%)
                                                            </label>
                                                            <input
                                                                type="number"
                                                                min="0"
                                                                max="100"
                                                                step="0.01"
                                                                value={recipient.share}
                                                                onChange={(e) => updateTaxRecipient(recipient.id, 'share', parseFloat(e.target.value) || 0)}
                                                                className="w-full bg-slate-700 border border-slate-600 rounded px-3 py-2 focus:ring-2 focus:ring-blue-500 focus:border-transparent text-sm"
                                                            />
                                                        </div>
                                                        <button
                                                            type="button"
                                                            onClick={() => removeTaxRecipient(recipient.id)}
                                                            className="self-end p-2 text-red-400 hover:text-red-300 hover:bg-red-500/20 rounded transition-colors"
                                                            title="Remove recipient"
                                                        >
                                                            <Trash2 className="w-4 h-4" />
                                                        </button>
                                                    </div>
                                                </div>
                                            ))
                                        )}
                                    </div>
                                </div>
                            </div>
                        )}
                    </div>

                    <div className="text-center mb-8">
                        <button
                            onClick={handleGenerate}
                            disabled={isGenerating}
                            className="bg-blue-600 hover:bg-blue-700 disabled:bg-blue-800 disabled:cursor-not-allowed px-8 py-3 rounded-lg font-medium transition-colors inline-flex items-center gap-2"
                        >
                            {isGenerating ? (
                                <>
                                    <div className="w-4 h-4 border-2 border-white border-t-transparent rounded-full animate-spin"></div>
                                    Generating...
                                </>
                            ) : (
                                <>
                                    <Code className="w-5 h-5" />
                                    Generate Smart Contract
                                </>
                            )}
                        </button>
                    </div>

                    {generatedCode && (
                        <div
                            id="result-section"
                            className="bg-slate-800 rounded-lg p-6 border border-slate-700"
                        >
                            <div className="flex justify-between items-center mb-4">
                                <h2 className="text-xl font-semibold">Generated Smart Contract</h2>
                                <div className="flex gap-2">
                                    <button
                                        onClick={handleCopy}
                                        className="flex items-center gap-2 px-3 py-2 bg-slate-700 hover:bg-slate-600 rounded text-sm transition-colors"
                                    >
                                        {copySuccess ? <Check className="w-4 h-4" /> : <Copy className="w-4 h-4" />}
                                        {copySuccess ? 'Copied!' : 'Copy'}
                                    </button>
                                    <button
                                        onClick={handleDownload}
                                        className="flex items-center gap-2 px-3 py-2 bg-slate-700 hover:bg-slate-600 rounded text-sm transition-colors"
                                    >
                                        <Download className="w-4 h-4" />
                                        Download
                                    </button>
                                </div>
                            </div>

                            <textarea
                                value={generatedCode}
                                readOnly
                                className="w-full h-96 bg-slate-900 border border-slate-600 rounded p-4 font-mono text-sm resize-none focus:ring-2 focus:ring-blue-500 focus:border-transparent"
                                style={{ fontFamily: '"Monaco", "Menlo", "Ubuntu Mono", monospace' }}
                            />

                            <div className="mt-4 p-4 bg-slate-700/50 rounded border border-slate-600">
                                <h3 className="font-medium mb-2">Next Steps:</h3>
                                <ul className="text-sm text-slate-300 space-y-1">
                                    <li>1. Review the generated contract code</li>
                                    <li>2. Test the contract on a testnet before mainnet deployment</li>
                                    <li>3. Consider getting a security audit for production use</li>
                                    <li>4. Deploy using Remix, Hardhat, or your preferred development framework</li>
                                </ul>
                            </div>
                        </div>
                    )}
                </div>
            </div>

            {isGenerating && (
                <div className="fixed inset-0 bg-black bg-opacity-50 flex items-center justify-center z-50">
                    <div className="bg-slate-800 rounded-lg p-8 text-center border border-slate-700">
                        <div className="w-12 h-12 border-4 border-slate-600 border-t-blue-500 rounded-full animate-spin mx-auto mb-4" />
                        <p className="text-white text-lg">Generating smart contract...</p>
                    </div>
                </div>
            )}
        </div>
    );
};

export default GenerateContractPage;