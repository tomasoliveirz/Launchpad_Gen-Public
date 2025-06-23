import React, { useState } from 'react';
import { Code, Copy, Download, Check, Plus, Trash2 } from 'lucide-react';
import type { NavigationProps } from '../types';
import { useContractConfig } from '../hooks/useContractConfig';
import { validateContractConfig, ValidationErrors } from '../utils/validation';
import { generateSolidityContract, downloadContract } from '../utils/contractGenerator';
import { copyToClipboard } from '../utils/clipboard';
import ScrollToTop from '../componnents/layout/ScrollToTop';
import SolidityViewer from '../componnents/common/SolidityViewer';
import { useEffect } from 'react';

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
    const [showDownloadSuccess, setShowDownloadSuccess] = useState(false);
    const [errors, setErrors] = useState<ValidationErrors>({});
    const [decimalInput, setDecimalInput] = useState(config.decimals.toString());
    const [isFocused, setIsFocused] = useState(false);
 
    const [premintInput, setPremintInput] = useState(config.premint.toString());
    const [isPremintFocused, setIsPremintFocused] = useState(false);


    useEffect(() => {
        if (!isPremintFocused) {
            setPremintInput(config.premint.toString());
        }
    }, [config.premint, isPremintFocused]);

    useEffect(() => {
        if (!isFocused) {
            setDecimalInput(config.decimals.toString());
        }
    }, [config.decimals, isFocused]);
  
    const showToast = (message: string) => {
        alert(message);
    };

    const clearError = (fieldName: string) => {
        if (errors[fieldName]) {
            setErrors(prev => {
                const newErrors = { ...prev };
                delete newErrors[fieldName];
                return newErrors;
            });
        }
    };

    const handleGenerate = async () => {
        const validationErrors = validateContractConfig(config);

        if (Object.keys(validationErrors).length > 0) {
            setErrors(validationErrors);
            const firstErrorField = Object.keys(validationErrors)[0];
            const element = document.getElementById(firstErrorField);
            if (element) {
                element.scrollIntoView({ behavior: 'smooth', block: 'center' });
            }
            return;
        }

        setErrors({});
        setIsGenerating(true);

        try {
            const code = await generateSolidityContract(config);
            setGeneratedCode(code);

            setTimeout(() => {
                document.getElementById('result-section')?.scrollIntoView({
                    behavior: 'smooth'
                });
            }, 100);
        } catch (error) {
            console.error('Generation error:', error);
            const errorMessage = error instanceof Error ? error.message : 'Failed to generate contract. Please try again.';
            showToast(errorMessage);
        } finally {
            setIsGenerating(false);
        }
    };

    const handleCopy = async () => {
        const success = await copyToClipboard(generatedCode);
        if (success) {
            setCopySuccess(true);
            setTimeout(() => setCopySuccess(false), 2000);
        }
    };

    const handleDownload = () => {
        try {
            const filename = config.name.replace(/\s+/g, '') || 'Contract';
            downloadContract(generatedCode, filename);
            setShowDownloadSuccess(true);
            setTimeout(() => setShowDownloadSuccess(false), 2000);
        } catch (error) {
            showToast('Failed to download contract');
        }
    };

    const handleTaxToggle = (enabled: boolean) => {
        updateConfig({ hasTax: enabled });
        if (enabled && config.taxRecipients.length === 0) {
            addTaxRecipient();
        }
        clearError('taxRecipients');
    };

    const totalShares = config.taxRecipients.reduce((sum: number, r: any) => sum + r.share, 0);

    const ErrorMessage: React.FC<{ error?: string }> = ({ error }) => {
        if (!error) return null;
        return (
            <p
                style={{ color: '#f87171' }}
                className="text-sm mt-1 font-medium"
            >
                {error}
            </p>
        );
    };

    return (
        <>
            <style>{`
                @keyframes write-line {
                    from { stroke-dasharray: 1000; stroke-dashoffset: 1000; }
                    to { stroke-dasharray: 1000; stroke-dashoffset: 0; }
                }
                
                @keyframes pen-move {
                    0% { transform: translate(0, 0); }
                    25% { transform: translate(30px, 0); }
                    50% { transform: translate(60px, 15px); }
                    75% { transform: translate(20px, 30px); }
                    100% { transform: translate(0, 0); }
                }
                
                @keyframes paper-into-folder {
                    0% { transform: translateY(-50px) translateX(-20px) rotate(-5deg); opacity: 1; }
                    50% { transform: translateY(-10px) translateX(0px) rotate(0deg); opacity: 1; }
                    100% { transform: translateY(30px) translateX(20px) rotate(5deg); opacity: 0.3; }
                }
                
                @keyframes folder-close {
                    0% { transform: rotateX(0deg); }
                    50% { transform: rotateX(-15deg); }
                    100% { transform: rotateX(0deg); }
                }
                
                @keyframes success-popup {
                    0% { transform: scale(0.8); opacity: 0; }
                    10% { transform: scale(1.05); opacity: 1; }
                    20% { transform: scale(1); opacity: 1; }
                    100% { transform: scale(1); opacity: 1; }
                }
                
                .animate-write-1 {
                    animation: write-line 1s ease-in-out 0.5s both;
                }
                
                .animate-write-2 {
                    animation: write-line 1.2s ease-in-out 1.8s both;
                }
                
                .animate-write-3 {
                    animation: write-line 1s ease-in-out 3.2s both;
                }
                
                .animate-pen {
                    animation: pen-move 4s ease-in-out infinite;
                    transform-origin: center;
                }
                
                .animate-paper-folder {
                    animation: paper-into-folder 1.5s ease-in-out;
                }
                
                .animate-folder-close {
                    animation: folder-close 1.5s ease-in-out 0.5s;
                }
                
                .animate-success-popup {
                    animation: success-popup 0.3s ease-out;
                }
            `}</style>
            <div className="min-h-screen bg-slate-900 text-white">
                <div className="container mx-auto px-6 py-8">
                    <div className="text-center mb-8">
                        <h1 className="page-title-gradient-green">Generate a Smart Contract</h1>
                        <p className="text-slate-400">
                            Fill in the form and customize your contract code as desired. What you see is what you get,
                            but that doesn't mean you can't make a change or two.
                        </p>
                    </div>

                    <div className="max-w-4xl mx-auto">
                        <div className="selectors-wrapper">
                            <div className="selector-container solidity-selector-wrapper">
                                <div className="language-selector">
                                    <div className="selector-button active">
                                        <svg className="solidity-icon" viewBox="0 0 24 24" fill="currentColor">
                                            <path d="M12 2L2 8.5V15.5L12 22L22 15.5V8.5L12 2ZM12 4.47225L19.9815 9.15555L12 13.8388L4.01851 9.15555L12 4.47225ZM12 19.5278L4.01851 14.8445L12 10.1612L19.9815 14.8445L12 19.5278ZM12 15.5L8 13V9L12 6.5L16 9V13L12 15.5ZM12 14.075L14.7396 12.4417L12 10.8083L9.26043 12.4417L12 14.075Z" />
                                        </svg>
                                        Solidity
                                    </div>
                                </div>
                            </div>

                            <div className="selector-container erc20-selector-wrapper">
                                <div className="token-selector">
                                    <div className="selector-button active">
                                        <svg className="token-icon" viewBox="0 0 24 24">
                                            <circle cx="12" cy="12" r="10" fill="none" stroke="currentColor" strokeWidth="2" />
                                            <circle cx="12" cy="12" r="6" fill="currentColor" opacity="0.3" />
                                            <circle cx="12" cy="12" r="2" fill="currentColor" />
                                        </svg>
                                        ERC20
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>



                    <div className="max-w-4xl mx-auto">
                        <div className="bg-slate-800 rounded-lg p-6 mb-6 border border-slate-700">
                            <h2 className="section-heading-darker-green">
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
                                        onChange={(e) => {
                                            updateConfig({ name: e.target.value });
                                            clearError('name');
                                        }}
                                        className={`input-text-greish-green ${errors.name ? 'border-red-500' : ''}`}
                                        placeholder="MyToken"
                                    />
                                    <ErrorMessage error={errors.name} />
                                </div>

                                <div>
                                    <label htmlFor="symbol" className="block text-sm font-medium mb-2">
                                        Symbol
                                    </label>
                                    <input
                                        id="symbol"
                                        type="text"
                                        value={config.symbol}
                                        onChange={(e) => {
                                            updateConfig({ symbol: e.target.value.toUpperCase() });
                                            clearError('symbol');
                                        }}
                                        className={`input-text-greish-green ${errors.symbol ? 'border-red-500' : ''}`}
                                        placeholder="MTK"
                                        maxLength={10}
                                    />
                                    <ErrorMessage error={errors.symbol} />
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
                                            value={decimalInput}
                                            placeholder="0"
                                            onFocus={() => setIsFocused(true)}
                                            onBlur={(e) => {
                                                setIsFocused(false);
                                                let parsed = parseInt(e.target.value);
                                                if (isNaN(parsed)) parsed = 0;
                                                if (parsed > 18) parsed = 18;

                                                updateConfig({ decimals: parsed });
                                                setDecimalInput(parsed.toString());
                                            }}
                                            onChange={(e) => {
                                                const value = e.target.value;

                                                if (value === '') {
                                                    setDecimalInput('');
                                                    return;
                                                }

                                                const parsed = parseInt(value);

                                                if (!isNaN(parsed) && parsed <= 18) {
                                                    updateConfig({ decimals: parsed });
                                                    setDecimalInput(value);
                                                } else if (!isNaN(parsed) && parsed > 18) {
                                                    updateConfig({ decimals: 18 });
                                                    setDecimalInput('18');
                                                }

                                                clearError('decimals');
                                            }}
                                            className={`input-text-greish-green ${errors.decimals ? 'border-red-500' : ''}`}
                                        />


                                        <ErrorMessage error={errors.decimals} />
                                    </div>

                                    <div>
                                        <label htmlFor="premint" className="block text-sm font-medium mb-2">
                                            Initial Mint
                                        </label>
                                        <input
                                            id="premint"
                                            type="number"
                                            min="0"
                                            placeholder="0"
                                            value={premintInput}
                                            onFocus={() => setIsPremintFocused(true)}
                                            onBlur={(e) => {
                                                setIsPremintFocused(false);
                                                let parsed = parseInt(e.target.value);
                                                if (isNaN(parsed)) parsed = 0;
                                                updateConfig({ premint: parsed });
                                                setPremintInput(parsed.toString());
                                            }}
                                            onChange={(e) => {
                                                const value = e.target.value;

                                                if (value === '') {
                                                    setPremintInput('');
                                                    return;
                                                }

                                                const parsed = parseInt(value);

                                                if (!isNaN(parsed)) {
                                                    updateConfig({ premint: parsed });
                                                    setPremintInput(value);
                                                }

                                                clearError('premint');
                                            }}
                                            className={`input-text-greish-green ${errors.premint ? 'border-red-500' : ''}`}
                                        />
                                        <ErrorMessage error={errors.premint} />
                                    </div>
                                </div>

                            </div>
                        </div>

                        <div className="bg-slate-800 rounded-lg p-6 mb-6 border border-slate-700">
                            <h2 className="section-heading-darker-green">
                                Token Features
                            </h2>

                            <div className="space-y-3">
                                <label className="checkbox-label">
                                    <input
                                        type="checkbox"
                                        checked={config.hasMinting}
                                        onChange={(e) => {
                                            const hasMinting = e.target.checked;
                                            updateConfig({
                                                hasMinting,
                                                // If Mintable turned on, enforce Access Control on
                                                hasAccessControl: hasMinting ? true : config.hasAccessControl,
                                            });
                                        }}
                                        className="checkbox-input"
                                    />
                                    <div className="flex-1">
                                        <div className="checkbox-text-main">Mintable</div>
                                        <div className="checkbox-text-sub">Allow minting new tokens</div>
                                    </div>
                                </label>

                                <label className="checkbox-label">
                                    <input
                                        type="checkbox"
                                        checked={config.hasBurning}
                                        onChange={(e) => updateConfig({ hasBurning: e.target.checked })}
                                        className="checkbox-input"
                                    />
                                    <div className="flex-1">
                                        <div className="checkbox-text-main">Burnable</div>
                                        <div className="checkbox-text-sub">Allow burning tokens</div>
                                    </div>
                                </label>

                                <label className="checkbox-label">
                                    <input
                                        type="checkbox"
                                        checked={config.isPausable}
                                        onChange={(e) => updateConfig({ isPausable: e.target.checked })}
                                        className="checkbox-input"
                                    />
                                    <div className="flex-1">
                                        <div className="checkbox-text-main">Pausable</div>
                                        <div className="checkbox-text-sub">Emergency stop functionality</div>
                                    </div>
                                </label>
                            </div>

                            <div className="bg-slate-800 rounded-lg p-6 mb-6 border border-slate-700">
                                <h2 className="section-heading-darker-green">Access Control</h2>

                                <label
                                    className={`flex items-center gap-3 p-3 bg-slate-700/50 rounded border border-slate-600 hover:bg-slate-700 transition-colors cursor-pointer mb-4 ${config.hasMinting ? 'opacity-50 cursor-not-allowed' : ''
                                        }`}
                                >
                                    <input
                                        type="checkbox"
                                        checked={config.hasAccessControl}
                                        onChange={(e) => {
                                            // Prevent turning off if Mintable is enabled
                                            if (config.hasMinting) return;
                                            updateConfig({ hasAccessControl: e.target.checked });
                                        }}
                                        className="checkbox-input"
                                        disabled={config.hasMinting} // Disable if Mintable is checked
                                    />
                                    <span className="font-medium">Enable Access Control</span>
                                </label>

                                {config.hasAccessControl && (
                                    <div className="bg-slate-700/30 rounded p-4 border border-slate-600">
                                        <label htmlFor="accessControlType" className="block text-sm font-medium mb-2 text-gray-300">
                                            Control Type
                                        </label>
                                        <select
                                            id="accessControlType"
                                            value={config.accessControlType}
                                            onChange={(e) => updateConfig({ accessControlType: parseInt(e.target.value) })}
                                            className="input-text-greish-no-focus"
                                        >
                                            <option value={0}>Ownable (Single Owner)</option>
                                        </select>
                                    </div>
                                )}
                            </div>



                            {showDownloadSuccess && (
                                <div className="fixed inset-0 bg-black bg-opacity-50 flex items-center justify-center z-50">
                                    <div className="bg-slate-800 rounded-lg p-8 text-center border border-slate-700 max-w-md animate-success-popup">

                                        <div className="mb-6 relative">
                                            <svg width="200" height="140" viewBox="0 0 200 140" className="mx-auto">
                                                <path d="M30 60 L30 110 Q30 115 35 115 L165 115 Q170 115 170 110 L170 60 Z" fill="#fbbf24" stroke="#f59e0b" strokeWidth="1" />

                                                <path d="M30 60 L70 60 Q75 60 75 55 L75 50 Q75 45 80 45 L120 45 Q125 45 125 50 L125 55 Q125 60 130 60 L170 60" fill="#f59e0b" stroke="#d97706" strokeWidth="1" />

                                                <path d="M30 60 L170 60 L170 65 Q170 70 165 70 L35 70 Q30 70 30 65 Z" fill="#f59e0b" className="animate-folder-close" style={{ transformOrigin: "100px 60px" }}>
                                                </path>

                                                <g className="animate-paper-folder">
                                                    <rect x="60" y="20" width="80" height="100" fill="#f8fafc" rx="2" stroke="#e2e8f0" strokeWidth="1" />
                                                    <line x1="70" y1="35" x2="130" y2="35" stroke="#3b82f6" strokeWidth="1" />
                                                    <line x1="70" y1="45" x2="130" y2="45" stroke="#3b82f6" strokeWidth="1" />
                                                    <line x1="70" y1="55" x2="120" y2="55" stroke="#3b82f6" strokeWidth="1" />
                                                    <line x1="70" y1="65" x2="125" y2="65" stroke="#3b82f6" strokeWidth="1" />
                                                </g>
                                            </svg>
                                        </div>

                                        <div className="mb-4">
                                            <div className="w-12 h-12 bg-green-500 rounded-full flex items-center justify-center mx-auto mb-3">
                                                <svg className="w-6 h-6 text-white" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                                                    <path strokeLinecap="round" strokeLinejoin="round" strokeWidth="2" d="M5 13l4 4L19 7"></path>
                                                </svg>
                                            </div>
                                            <p className="text-white text-lg font-medium">Congratulations!</p>
                                            <p className="text-slate-300 text-sm mt-1">Your contract was downloaded successfully!</p>
                                        </div>
                                    </div>
                                </div>
                            )}
                        </div>

                        <div className="bg-slate-800 rounded-lg p-6 mb-6 border border-slate-700">
                            <h2 className="section-heading-darker-green">
                                Tax System
                            </h2>

                            <label className="flex items-center gap-3 p-3 bg-slate-700/50 rounded border border-slate-600 hover:bg-slate-700 transition-colors cursor-pointer mb-4">
                                <input
                                    type="checkbox"
                                    checked={config.hasTax}
                                    onChange={(e) => handleTaxToggle(e.target.checked)}
                                    className="checkbox-input"
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
                                            onChange={(e) => {
                                                updateConfig({ taxFee: parseFloat(e.target.value) || 0 });
                                                clearError('taxFee');
                                            }}
                                            className={`input-text-greish-green ${errors.taxFee ? 'border-red-500' : ''}`}
                                        />
                                        <ErrorMessage error={errors.taxFee} />
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
                                                <ErrorMessage error={errors.taxRecipients} />
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
                                                config.taxRecipients.map((recipient, index) => (
                                                    <div
                                                        key={recipient.id}
                                                        className="grid grid-cols-1 md:grid-cols-3 gap-3 p-3 bg-slate-700/50 rounded border border-slate-600"
                                                    >
                                                        <div className="md:col-span-2">
                                                            <label className="block text-sm font-medium mb-1">
                                                                Recipient Address
                                                            </label>
                                                            <input
                                                                type="text"
                                                                value={recipient.address}
                                                                onChange={(e) => {
                                                                    updateTaxRecipient(recipient.id, 'address', e.target.value);
                                                                    clearError(`taxRecipient${index}Address`);
                                                                }}
                                                                placeholder="0x..."
                                                                className={`input-text-greish-green ${errors[`taxRecipient${index}Address`] ? 'border-red-500' : ''}`}
                                                            />
                                                            <ErrorMessage error={errors[`taxRecipient${index}Address`]} />
                                                        </div>

                                                        <div>
                                                            <label className="block text-sm font-medium mb-1">
                                                                Share (%)
                                                            </label>
                                                            <div className="flex gap-2 items-end">
                                                                <div className="flex-1">
                                                                    <input
                                                                        type="number"
                                                                        min="0"
                                                                        max="100"
                                                                        step="0.01"
                                                                        value={recipient.share}
                                                                        onChange={(e) => {
                                                                            updateTaxRecipient(recipient.id, 'share', parseFloat(e.target.value) || 0);
                                                                            clearError(`taxRecipient${index}Share`);
                                                                            clearError('taxRecipients');
                                                                        }}
                                                                        className={`input-text-greish-green ${errors[`taxRecipient${index}Share`] ? 'border-red-500' : ''}`}
                                                                    />
                                                                </div>
                                                                <button
                                                                    type="button"
                                                                    onClick={() => removeTaxRecipient(recipient.id)}
                                                                    className="px-3 py-2 text-red-400 hover:text-red-300 hover:bg-red-500/20 rounded transition-colors border border-transparent h-10 flex items-center justify-center"
                                                                    title="Remove recipient"
                                                                >
                                                                    <Trash2 className="w-4 h-4" />
                                                                </button>
                                                            </div>
                                                            <ErrorMessage error={errors[`taxRecipient${index}Share`]} />
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
                                className="generate-button"
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
                                    <h2 className="section-heading-darker-green" style={{ borderBottom: 'none' }}>
                                        Generated Smart Contract
                                    </h2>
                                    <div className="flex gap-2">
                                        <button
                                            onClick={handleCopy}
                                            className="result-action-button"
                                        >
                                            {copySuccess ? <Check className="w-4 h-4" /> : <Copy className="w-4 h-4" />}
                                            {copySuccess ? 'Copied!' : 'Copy'}
                                        </button>
                                        <button
                                            onClick={handleDownload}
                                            className="result-action-button"
                                        >
                                            <Download className="w-4 h-4" />
                                            Download
                                        </button>
                                    </div>
                                </div>

                                <SolidityViewer generatedCode={generatedCode} />


                                <div className="mt-4 p-4 bg-slate-700/50 rounded border border-slate-600">
                                    <h3 className="font-medium mb-2 section-heading-darker-green">Next Steps:</h3>
                                    <ul>
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
                        <div className="bg-slate-800 rounded-lg p-8 text-center border border-slate-700 max-w-md">

                            <div className="mb-6 relative">
                                <svg width="200" height="140" viewBox="0 0 200 140" className="mx-auto">

                                    <rect x="20" y="20" width="160" height="100" fill="#f8fafc" rx="4" stroke="#e2e8f0" strokeWidth="1" />

                                    <line x1="30" y1="40" x2="170" y2="40" stroke="#e2e8f0" strokeWidth="1" />
                                    <line x1="30" y1="55" x2="170" y2="55" stroke="#e2e8f0" strokeWidth="1" />
                                    <line x1="30" y1="70" x2="170" y2="70" stroke="#e2e8f0" strokeWidth="1" />
                                    <line x1="30" y1="85" x2="170" y2="85" stroke="#e2e8f0" strokeWidth="1" />
                                    <line x1="30" y1="100" x2="170" y2="100" stroke="#e2e8f0" strokeWidth="1" />

                                    <line x1="30" y1="40" x2="90" y2="40" stroke="#3b82f6" strokeWidth="2" className="animate-write-1" />
                                    <line x1="30" y1="55" x2="120" y2="55" stroke="#3b82f6" strokeWidth="2" className="animate-write-2" />
                                    <line x1="30" y1="70" x2="80" y2="70" stroke="#3b82f6" strokeWidth="2" className="animate-write-3" />

                                    <g className="animate-pen">
                                        <line x1="85" y1="35" x2="95" y2="25" stroke="#374151" strokeWidth="3" strokeLinecap="round" />
                                        <circle cx="95" cy="25" r="2" fill="#fbbf24" />
                                        <line x1="85" y1="35" x2="90" y2="30" stroke="#1f2937" strokeWidth="2" strokeLinecap="round" />
                                    </g>
                                </svg>
                            </div>

                            <p className="text-white text-lg font-medium mb-2">We are generating your contract</p>
                            <p className="text-slate-400 text-sm">This may take a few moments...</p>

                            <div className="flex justify-center space-x-1 mt-4">
                                <div className="w-2 h-2 bg-blue-500 rounded-full animate-bounce" style={{ animationDelay: '0ms' }}></div>
                                <div className="w-2 h-2 bg-blue-500 rounded-full animate-bounce" style={{ animationDelay: '150ms' }}></div>
                                <div className="w-2 h-2 bg-blue-500 rounded-full animate-bounce" style={{ animationDelay: '300ms' }}></div>
                            </div>
                        </div>
                    </div>
                )}

                <ScrollToTop />
            </div>
        </>
    );
};

export default GenerateContractPage;