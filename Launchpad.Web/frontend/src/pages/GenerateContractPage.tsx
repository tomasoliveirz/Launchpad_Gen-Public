import React, { useState, useEffect } from 'react';
import { Code, Copy, Download, Check, Plus, Trash2, ChevronDown } from 'lucide-react';
import type { NavigationProps } from '../types';
import { useContractConfig } from '../hooks/useContractConfig';
import { validateContractConfig, ValidationErrors } from '../utils/validation';
import { generateSolidityContract, downloadContract } from '../utils/contractGenerator';
import { copyToClipboard } from '../utils/clipboard';
import ScrollToTop from '../componnents/layout/ScrollToTop';
import SolidityViewer from '../componnents/common/SolidityViewer';

type SetupStep = 'language-selection' | 'model-selection' | 'loading' | 'form';

const GenerateContractPage: React.FC<NavigationProps> = ({ onNavigate }) => {
    const [currentStep, setCurrentStep] = useState<SetupStep>('language-selection');
    const [selectedLanguage, setSelectedLanguage] = useState('solidity');
    const [selectedModel, setSelectedModel] = useState('erc20');
    const [step1Complete, setStep1Complete] = useState(false);
    const [step2Complete, setStep2Complete] = useState(false);

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

    const handleLanguageNext = () => {
        setStep1Complete(true);
        setTimeout(() => {
            setCurrentStep('model-selection');
        }, 1000);
    };

    const handleModelNext = () => {
        setStep2Complete(true);
        setTimeout(() => {
            setCurrentStep('loading');
        }, 1000);

        setTimeout(() => {
            setCurrentStep('form');
        }, 3000);
    };

    const ClipboardIcon = () => (
        <svg width="100" height="100" viewBox="0 0 100 100" style={{ margin: '0 auto' }}>
            <rect x="20" y="15" width="60" height="70" fill="#a0d2db" stroke="#000" strokeWidth="4" rx="6" />
            <rect x="35" y="5" width="30" height="15" fill="#000" rx="3" />
            <rect x="37" y="7" width="26" height="11" fill="#7ec8d3" rx="2" />
            <rect x="25" y="25" width="50" height="55" fill="#fff3b8" stroke="#000" strokeWidth="2" rx="3" />
            <g stroke="#000" strokeWidth="3" fill="none">
                <path d="M30 35 L35 40 L45 30" strokeLinecap="round" strokeLinejoin="round" />
                <rect x="47" y="33" width="20" height="4" fill="#90ee90" rx="2" />
                <path d="M30 45 L35 50 L45 40" strokeLinecap="round" strokeLinejoin="round" />
                <rect x="47" y="43" width="20" height="4" fill="#90ee90" rx="2" />
                <path d="M30 55 L35 60 L45 50" strokeLinecap="round" strokeLinejoin="round" />
                <rect x="47" y="53" width="20" height="4" fill="#90ee90" rx="2" />
            </g>
            <g transform="translate(75, 20)" className="animate-pen-write">
                <rect x="0" y="0" width="4" height="25" fill="#4a90e2" rx="2" />
                <circle cx="2" cy="0" r="3" fill="#ffd700" />
                <polygon points="0,25 4,25 2,30" fill="#333" />
            </g>
        </svg>
    );

    const ProgressSquares = ({ step1Done, step2Done }: { step1Done: boolean, step2Done: boolean }) => (
        <svg width="80" height="30" viewBox="0 0 80 30" style={{ margin: '20px auto', display: 'block' }}>
            <rect
                x="10" y="5" width="20" height="20"
                fill={step1Done ? "#90ee90" : "#fff"}
                stroke="#000" strokeWidth="2" rx="3"
            />
            {step1Done && (
                <path
                    d="M15 15 L18 18 L25 11"
                    stroke="#000" strokeWidth="2" fill="none"
                    strokeLinecap="round" strokeLinejoin="round"
                    style={{
                        strokeDasharray: '20',
                        strokeDashoffset: '0',
                        animation: 'draw-check 0.5s ease-in-out'
                    }}
                />
            )}
            <rect
                x="50" y="5" width="20" height="20"
                fill={step2Done ? "#90ee90" : "#fff"}
                stroke="#000" strokeWidth="2" rx="3"
            />
            {step2Done && (
                <path
                    d="M55 15 L58 18 L65 11"
                    stroke="#000" strokeWidth="2" fill="none"
                    strokeLinecap="round" strokeLinejoin="round"
                    style={{
                        strokeDasharray: '20',
                        strokeDashoffset: '0',
                        animation: 'draw-check 0.5s ease-in-out'
                    }}
                />
            )}
        </svg>
    );

    const GearIcon = () => (
        <svg width="80" height="80" viewBox="0 0 80 80" style={{ margin: '0 auto' }}>
            <defs>
                <linearGradient id="gearGradient" x1="0%" y1="0%" x2="100%" y2="100%">
                    <stop offset="0%" stopColor="#5a7a95" />
                    <stop offset="50%" stopColor="#4a6b8a" />
                    <stop offset="100%" stopColor="#3d5a75" />
                </linearGradient>
                <linearGradient id="innerGradient" x1="0%" y1="0%" x2="100%" y2="100%">
                    <stop offset="0%" stopColor="#9bb4c7" />
                    <stop offset="100%" stopColor="#7a95b0" />
                </linearGradient>
            </defs>
            <g style={{
                transformOrigin: '40px 40px',
                animation: 'spin 2s linear infinite'
            }}>
                <path
                    d="M40 8 L42 12 L46 8 L48 12 L52 8 L54 12 L58 10 L62 14 L66 12 L68 16 L72 16 L72 20 L76 22 L74 26 L78 28 L76 32 L78 36 L74 38 L76 42 L72 44 L72 48 L68 48 L66 52 L62 50 L58 54 L54 52 L52 56 L48 54 L46 56 L42 52 L40 56 L38 52 L34 56 L32 52 L28 54 L26 50 L22 52 L20 48 L16 48 L16 44 L12 42 L14 38 L10 36 L12 32 L10 28 L14 26 L12 22 L16 20 L16 16 L20 16 L22 12 L26 14 L28 10 L32 12 L34 8 L38 12 L40 8 Z"
                    fill="url(#gearGradient)"
                    stroke="#2c4e70"
                    strokeWidth="1"
                />

                <circle
                    cx="40"
                    cy="40"
                    r="20"
                    fill="url(#innerGradient)"
                    stroke="#5a7a95"
                    strokeWidth="1"
                />

                <circle
                    cx="40"
                    cy="40"
                    r="14"
                    fill="#ffffff"
                    stroke="#9bb4c7"
                    strokeWidth="2"
                />

                <circle
                    cx="40"
                    cy="40"
                    r="8"
                    fill="none"
                    stroke="#9bb4c7"
                    strokeWidth="3"
                />
            </g>
        </svg>
    );

    if (currentStep === 'language-selection') {
        return (
            <>
                <style>{`
                    @keyframes draw-check {
                        0% { stroke-dasharray: 0 20; }
                        100% { stroke-dasharray: 20 0; }
                    }
                    @keyframes pen-write {
                        0%, 100% { transform: rotate(0deg); }
                        50% { transform: rotate(-15deg); }
                    }
                    @keyframes spin {
                        0% { transform: rotate(0deg); }
                        100% { transform: rotate(360deg); }
                    }
                    .animate-pen-write {
                        animation: pen-write 2s ease-in-out infinite;
                        transform-origin: center;
                    }
                `}</style>
                <div style={{
                    position: 'fixed',
                    top: 0,
                    left: 0,
                    right: 0,
                    bottom: 0,
                    backgroundColor: 'rgba(0,0,0,0.6)',
                    display: 'flex',
                    alignItems: 'center',
                    justifyContent: 'center',
                    zIndex: 1000
                }}>
                    <div style={{
                        backgroundColor: 'white',
                        borderRadius: '12px',
                        padding: '40px',
                        textAlign: 'center',
                        maxWidth: '450px',
                        margin: '20px',
                        color: '#374151',
                        boxShadow: '0 20px 25px -5px rgba(0, 0, 0, 0.1), 0 10px 10px -5px rgba(0, 0, 0, 0.04)'
                    }}>
                        <div style={{ marginBottom: '30px' }}>
                            <ClipboardIcon />
                            <ProgressSquares step1Done={step1Complete} step2Done={false} />
                        </div>

                        <h2 style={{
                            fontSize: '24px',
                            fontWeight: 'bold',
                            marginBottom: '20px',
                            color: '#1f2937'
                        }}>
                            Now let's choose your contract language!
                        </h2>

                        <div style={{ marginBottom: '30px' }}>
                            <label style={{
                                display: 'block',
                                fontSize: '14px',
                                fontWeight: '600',
                                marginBottom: '8px',
                                textAlign: 'left',
                                color: '#374151'
                            }}>
                                Contract Language
                            </label>
                            <div style={{ position: 'relative' }}>
                                <select
                                    value={selectedLanguage}
                                    onChange={(e) => setSelectedLanguage(e.target.value)}
                                    style={{
                                        width: '100%',
                                        padding: '12px 40px 12px 12px',
                                        border: '2px solid #d1d5db',
                                        borderRadius: '8px',
                                        backgroundColor: 'white',
                                        color: '#374151',
                                        fontSize: '16px',
                                        appearance: 'none',
                                        cursor: 'pointer'
                                    }}
                                >
                                    <option value="solidity">Solidity</option>
                                </select>
                                <ChevronDown style={{
                                    position: 'absolute',
                                    right: '12px',
                                    top: '50%',
                                    transform: 'translateY(-50%)',
                                    width: '20px',
                                    height: '20px',
                                    color: '#9ca3af',
                                    pointerEvents: 'none'
                                }} />
                            </div>
                        </div>

                        <button
                            onClick={handleLanguageNext}
                            style={{
                                width: '100%',
                                backgroundColor: '#22c55e',
                                color: 'white',
                                fontWeight: '600',
                                padding: '14px 24px',
                                borderRadius: '8px',
                                border: 'none',
                                cursor: 'pointer',
                                fontSize: '16px',
                                transition: 'all 0.2s ease'
                            }}
                            onMouseOver={(e) => {
                                (e.target as HTMLButtonElement).style.backgroundColor = '#16a34a';
                                (e.target as HTMLButtonElement).style.transform = 'translateY(-1px)';
                            }}
                            onMouseOut={(e) => {
                                (e.target as HTMLButtonElement).style.backgroundColor = '#22c55e';
                                (e.target as HTMLButtonElement).style.transform = 'translateY(0)';
                            }}
                        >
                            Next
                        </button>
                    </div>
                </div>
            </>
        );
    }

    if (currentStep === 'model-selection') {
        return (
            <>
                <style>{`
                    @keyframes draw-check {
                        0% { stroke-dasharray: 0 20; }
                        100% { stroke-dasharray: 20 0; }
                    }
                    @keyframes pen-write {
                        0%, 100% { transform: rotate(0deg); }
                        50% { transform: rotate(-15deg); }
                    }
                    @keyframes spin {
                        0% { transform: rotate(0deg); }
                        100% { transform: rotate(360deg); }
                    }
                    .animate-pen-write {
                        animation: pen-write 2s ease-in-out infinite;
                        transform-origin: center;
                    }
                `}</style>
                <div style={{
                    position: 'fixed',
                    top: 0,
                    left: 0,
                    right: 0,
                    bottom: 0,
                    backgroundColor: 'rgba(0,0,0,0.6)',
                    display: 'flex',
                    alignItems: 'center',
                    justifyContent: 'center',
                    zIndex: 1000
                }}>
                    <div style={{
                        backgroundColor: 'white',
                        borderRadius: '12px',
                        padding: '40px',
                        textAlign: 'center',
                        maxWidth: '450px',
                        margin: '20px',
                        color: '#374151',
                        boxShadow: '0 20px 25px -5px rgba(0, 0, 0, 0.1), 0 10px 10px -5px rgba(0, 0, 0, 0.04)'
                    }}>
                        <div style={{ marginBottom: '30px' }}>
                            <ClipboardIcon />
                            <ProgressSquares step1Done={true} step2Done={step2Complete} />
                        </div>

                        <h2 style={{
                            fontSize: '24px',
                            fontWeight: 'bold',
                            marginBottom: '20px',
                            color: '#1f2937'
                        }}>
                            Now let's choose your contract template!
                        </h2>

                        <div style={{ marginBottom: '30px' }}>
                            <label style={{
                                display: 'block',
                                fontSize: '14px',
                                fontWeight: '600',
                                marginBottom: '8px',
                                textAlign: 'left',
                                color: '#374151'
                            }}>
                                Contract Template
                            </label>
                            <div style={{ position: 'relative' }}>
                                <select
                                    value={selectedModel}
                                    onChange={(e) => setSelectedModel(e.target.value)}
                                    style={{
                                        width: '100%',
                                        padding: '12px 40px 12px 12px',
                                        border: '2px solid #d1d5db',
                                        borderRadius: '8px',
                                        backgroundColor: 'white',
                                        color: '#374151',
                                        fontSize: '16px',
                                        appearance: 'none',
                                        cursor: 'pointer'
                                    }}
                                >
                                    <option value="erc20">ERC20</option>
                                </select>
                                <ChevronDown style={{
                                    position: 'absolute',
                                    right: '12px',
                                    top: '50%',
                                    transform: 'translateY(-50%)',
                                    width: '20px',
                                    height: '20px',
                                    color: '#9ca3af',
                                    pointerEvents: 'none'
                                }} />
                            </div>
                        </div>

                        <button
                            onClick={handleModelNext}
                            style={{
                                width: '100%',
                                backgroundColor: '#22c55e',
                                color: 'white',
                                fontWeight: '600',
                                padding: '14px 24px',
                                borderRadius: '8px',
                                border: 'none',
                                cursor: 'pointer',
                                fontSize: '16px',
                                transition: 'all 0.2s ease'
                            }}
                            onMouseOver={(e) => {
                                (e.target as HTMLButtonElement).style.backgroundColor = '#16a34a';
                                (e.target as HTMLButtonElement).style.transform = 'translateY(-1px)';
                            }}
                            onMouseOut={(e) => {
                                (e.target as HTMLButtonElement).style.backgroundColor = '#22c55e';
                                (e.target as HTMLButtonElement).style.transform = 'translateY(0)';
                            }}
                        >
                            Next
                        </button>
                    </div>
                </div>
            </>
        );
    }

    if (currentStep === 'loading') {
        return (
            <div style={{
                position: 'fixed',
                top: 0,
                left: 0,
                right: 0,
                bottom: 0,
                backgroundColor: '#0f172a',
                display: 'flex',
                flexDirection: 'column',
                alignItems: 'center',
                justifyContent: 'center',
                zIndex: 1000
            }}>
                <style>{`
                    @keyframes pulse {
                        0%, 100% { opacity: 0.3; transform: scale(1); }
                        50% { opacity: 1; transform: scale(1.05); }
                    }
                    
                    @keyframes slideIn {
                        0% { opacity: 0; transform: translateX(-50px); }
                        100% { opacity: 1; transform: translateX(0); }
                    }
                    
                    @keyframes slideInRight {
                        0% { opacity: 0; transform: translateX(50px); }
                        100% { opacity: 1; transform: translateX(0); }
                    }
                    
                    @keyframes typewriter {
                        0% { width: 0; }
                        100% { width: 100%; }
                    }
                    
                    @keyframes blink {
                        0%, 50% { opacity: 1; }
                        51%, 100% { opacity: 0; }
                    }
                    
                    @keyframes blockchainPulse {
                        0% { transform: scale(1); opacity: 0.7; }
                        50% { transform: scale(1.1); opacity: 1; }
                        100% { transform: scale(1); opacity: 0.7; }
                    }
                    
                    @keyframes connectLine {
                        0% { stroke-dasharray: 0 100; }
                        100% { stroke-dasharray: 100 0; }
                    }
                    
                    @keyframes loadingBar {
                        0% { width: 0%; }
                        33% { width: 30%; }
                        66% { width: 70%; }
                        100% { width: 100%; }
                    }
                    
                    .blockchain-block {
                        animation: blockchainPulse 2s ease-in-out infinite;
                    }
                    
                    .blockchain-block:nth-child(2) {
                        animation-delay: 0.3s;
                    }
                    
                    .blockchain-block:nth-child(3) {
                        animation-delay: 0.6s;
                    }
                    
                    .connecting-line {
                        animation: connectLine 1.5s ease-in-out infinite;
                    }
                    
                    .code-line {
                        overflow: hidden;
                        white-space: nowrap;
                        animation: typewriter 2s steps(40) infinite;
                    }
                    
                    .loading-progress {
                        animation: loadingBar 3s ease-in-out infinite;
                    }
                `}</style>

                <div style={{ textAlign: 'center', maxWidth: '600px', padding: '0 20px' }}>

                    <div style={{ marginBottom: '40px' }}>
                        <svg width="300" height="100" viewBox="0 0 300 100" style={{ margin: '0 auto' }}>
                            
                            <line x1="70" y1="50" x2="130" y2="50" stroke="#22c55e" strokeWidth="3" className="connecting-line" />
                            <line x1="170" y1="50" x2="230" y2="50" stroke="#22c55e" strokeWidth="3" className="connecting-line" style={{ animationDelay: '0.5s' }} />

                            <g className="blockchain-block">
                                <rect x="20" y="25" width="50" height="50" fill="#1e293b" stroke="#22c55e" strokeWidth="2" rx="5" />
                                <rect x="25" y="30" width="40" height="8" fill="#22c55e" opacity="0.7" rx="2" />
                                <rect x="25" y="42" width="30" height="6" fill="#22c55e" opacity="0.5" rx="1" />
                                <rect x="25" y="52" width="35" height="6" fill="#22c55e" opacity="0.5" rx="1" />
                                <circle cx="60" cy="35" r="3" fill="#22c55e" />
                            </g>

                            <g className="blockchain-block">
                                <rect x="130" y="25" width="50" height="50" fill="#1e293b" stroke="#16a34a" strokeWidth="2" rx="5" />
                                <rect x="135" y="30" width="40" height="8" fill="#16a34a" opacity="0.7" rx="2" />
                                <rect x="135" y="42" width="25" height="6" fill="#16a34a" opacity="0.5" rx="1" />
                                <rect x="135" y="52" width="30" height="6" fill="#16a34a" opacity="0.5" rx="1" />
                                <circle cx="170" cy="35" r="3" fill="#16a34a" />
                            </g>

                            <g className="blockchain-block">
                                <rect x="230" y="25" width="50" height="50" fill="#1e293b" stroke="#15803d" strokeWidth="2" rx="5" />
                                <rect x="235" y="30" width="40" height="8" fill="#15803d" opacity="0.7" rx="2" />
                                <rect x="235" y="42" width="35" height="6" fill="#15803d" opacity="0.5" rx="1" />
                                <rect x="235" y="52" width="20" height="6" fill="#15803d" opacity="0.5" rx="1" />
                                <circle cx="270" cy="35" r="3" fill="#15803d" />
                            </g>
                        </svg>
                    </div>

                    <div style={{ marginBottom: '30px' }}>
                        <div style={{ display: 'flex', justifyContent: 'center', alignItems: 'center', gap: '20px', marginBottom: '20px' }}>
                            <div style={{ display: 'flex', alignItems: 'center', gap: '8px' }}>
                                <div style={{ width: '10px', height: '10px', borderRadius: '50%', backgroundColor: '#22c55e' }}></div>
                                <span style={{ color: '#22c55e', fontSize: '14px', fontWeight: '500' }}>Analyzing Template</span>
                            </div>
                            <div style={{ display: 'flex', alignItems: 'center', gap: '8px' }}>
                                <div style={{ width: '10px', height: '10px', borderRadius: '50%', backgroundColor: '#fbbf24', animation: 'pulse 1s infinite' }}></div>
                                <span style={{ color: '#fbbf24', fontSize: '14px', fontWeight: '500' }}>Generating Code</span>
                            </div>
                            <div style={{ display: 'flex', alignItems: 'center', gap: '8px' }}>
                                <div style={{ width: '10px', height: '10px', borderRadius: '50%', backgroundColor: '#64748b' }}></div>
                                <span style={{ color: '#64748b', fontSize: '14px', fontWeight: '500' }}>Compiling Contract</span>
                            </div>
                        </div>

                        <div style={{ width: '100%', height: '4px', backgroundColor: '#1e293b', borderRadius: '2px', overflow: 'hidden' }}>
                            <div style={{ height: '100%', backgroundColor: '#22c55e', borderRadius: '2px' }} className="loading-progress"></div>
                        </div>
                    </div>

                    <div style={{
                        backgroundColor: '#1e1e1e',
                        borderRadius: '8px',
                        padding: '20px',
                        marginBottom: '30px',
                        border: '1px solid #374151',
                        maxWidth: '500px',
                        margin: '0 auto 30px auto'
                    }}>
                        <div style={{ display: 'flex', alignItems: 'center', marginBottom: '15px', gap: '8px' }}>
                            <div style={{ width: '12px', height: '12px', borderRadius: '50%', backgroundColor: '#ef4444' }}></div>
                            <div style={{ width: '12px', height: '12px', borderRadius: '50%', backgroundColor: '#f59e0b' }}></div>
                            <div style={{ width: '12px', height: '12px', borderRadius: '50%', backgroundColor: '#22c55e' }}></div>
                            <span style={{ color: '#94a3b8', fontSize: '12px', marginLeft: '10px' }}>Contract.sol</span>
                        </div>

                        <div style={{ fontFamily: 'Monaco, monospace', fontSize: '13px', lineHeight: '1.4' }}>
                            <div style={{ color: '#22c55e', marginBottom: '5px' }}>
                                <span className="code-line">pragma solidity ^0.8.0;</span>
                                <span style={{ animation: 'blink 1s infinite', marginLeft: '2px' }}>|</span>
                            </div>
                            <div style={{ color: '#3b82f6', marginBottom: '5px', animationDelay: '0.5s' }}>
                                <span className="code-line">import "@openzeppelin/contracts/token/ERC20/ERC20.sol";</span>
                            </div>
                            <div style={{ color: '#a78bfa', animationDelay: '1s' }}>
                                <span className="code-line">{'contract YourToken is ERC20 {'}</span>
                            </div>
                        </div>
                    </div>

                    <div style={{ marginBottom: '20px' }}>
                        <h2 style={{
                            fontSize: '28px',
                            fontWeight: '700',
                            color: 'white',
                            marginBottom: '10px',
                            background: 'linear-gradient(90deg, #22c55e, #16a34a)',
                            WebkitBackgroundClip: 'text',
                            WebkitTextFillColor: 'transparent',
                            backgroundClip: 'text'
                        }}>
                            Let's start the magic!
                        </h2>
                        <p style={{
                            fontSize: '16px',
                            color: '#94a3b8',
                            maxWidth: '400px',
                            margin: '0 auto',
                            lineHeight: '1.5'
                        }}>
                            We're crafting your smart contract using the latest security standards and best practices.
                        </p>
                    </div>

                    <div style={{ position: 'absolute', top: '20%', left: '10%', animation: 'pulse 3s infinite' }}>
                        <div style={{ width: '6px', height: '6px', backgroundColor: '#22c55e', borderRadius: '50%', opacity: 0.6 }}></div>
                    </div>
                    <div style={{ position: 'absolute', top: '30%', right: '15%', animation: 'pulse 3s infinite', animationDelay: '1s' }}>
                        <div style={{ width: '4px', height: '4px', backgroundColor: '#3b82f6', borderRadius: '50%', opacity: 0.4 }}></div>
                    </div>
                    <div style={{ position: 'absolute', bottom: '25%', left: '20%', animation: 'pulse 3s infinite', animationDelay: '2s' }}>
                        <div style={{ width: '8px', height: '8px', backgroundColor: '#f59e0b', borderRadius: '50%', opacity: 0.5 }}></div>
                    </div>
                    <div style={{ position: 'absolute', bottom: '35%', right: '25%', animation: 'pulse 3s infinite', animationDelay: '0.5s' }}>
                        <div style={{ width: '5px', height: '5px', backgroundColor: '#8b5cf6', borderRadius: '50%', opacity: 0.3 }}></div>
                    </div>
                </div>
            </div>
        );
    }

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
                                            if (config.hasMinting) return;
                                            updateConfig({ hasAccessControl: e.target.checked });
                                        }}
                                        className="checkbox-input"
                                        disabled={config.hasMinting}
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
                                                <path d="M30 60 L170 60 L170 65 Q170 70 165 70 L35 70 Q30 70 30 65 Z" fill="#f59e0b" className="animate-folder-close" style={{ transformOrigin: "100px 60px" }}></path>
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