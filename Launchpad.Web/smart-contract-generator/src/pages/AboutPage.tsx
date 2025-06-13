import React from 'react';
import type { NavigationProps } from '../types';
import ScrollToTop from '../componnents/layout/ScrollToTop';

const AboutPage: React.FC<NavigationProps> = ({ onNavigate }) => {
    return (
        <div className="min-h-screen bg-slate-900 text-white">
            <div className="container mx-auto px-6 py-20">
                <div className="max-w-4xl mx-auto">
                    <h1 className="text-4xl font-bold mb-8">About Launchpad</h1>

                    <div className="space-y-6 text-slate-300 leading-relaxed">
                        <p className="text-lg">
                            The Launchpad project emerges in the context of an academic internship developed at MoOngy S.A.,
                            a company that identified a market opportunity in simplifying the process of digital asset creation.
                        </p>

                        <p className="text-lg">
                            Launchpad is a cutting-edge smart contract generator that empowers developers and entrepreneurs
                            to create sophisticated ERC-20 tokens without extensive Solidity knowledge.
                        </p>

                        <h2 className="text-2xl font-semibold text-white mt-8 mb-4">Features</h2>
                        <ul className="space-y-2 ml-6">
                            <li>Customizable token parameters (name, symbol, supply, decimals)</li>
                            <li>Advanced features like minting, burning, and pausable functionality</li>
                            <li>Built-in tax system with multiple recipients</li>
                            <li>Access control with owner or role-based permissions</li>
                            <li>OpenZeppelin-based secure implementations</li>
                            <li>Instant contract generation and download</li>
                        </ul>

                        <h2 className="text-2xl font-semibold text-white mt-8 mb-4">Technology</h2>
                        <p>
                            Built with React, TypeScript, and modern web technologies, Launchpad provides a seamless
                            user experience for smart contract creation. All generated contracts follow industry
                            best practices and security standards.
                        </p>

                        <h2 className="text-2xl font-semibold text-white mt-8 mb-4">Security</h2>
                        <p>
                            Our smart contracts are built using OpenZeppelin's battle-tested libraries, ensuring
                            the highest level of security and reliability. Every contract follows established
                            patterns and includes comprehensive safety checks.
                        </p>

                        <h2 className="text-2xl font-semibold text-white mt-8 mb-4">Creators</h2>
                        <div className="bg-slate-800 rounded-lg p-6 border border-slate-700">
                            <p className="mb-4">This project was brought to life by a passionate team of developers:</p>
                            <div className="grid grid-cols-1 md:grid-cols-2 gap-4">
                                <div className="bg-slate-700/50 rounded-lg p-4 border border-slate-600">
                                    <h3 className="text-lg font-semibold text-blue-400 mb-2">Fábio Jesus</h3>
                                    <p className="text-sm text-slate-400">Advisor at MoOngy S.A.</p>
                                    <p className="text-sm mt-2">Responsible for technical guidance and business integration.</p>
                                </div>

                                <div className="bg-slate-700/50 rounded-lg p-4 border border-slate-600">
                                    <h3 className="text-lg font-semibold text-green-400 mb-2">Diogo Ferreira</h3>
                                    <p className="text-sm text-slate-400">Informatics and Computing Engineering Student @ FEUP</p>
                                </div>

                                <div className="bg-slate-700/50 rounded-lg p-4 border border-slate-600">
                                    <h3 className="text-lg font-semibold text-green-400 mb-2">Pedro Marinho</h3>
                                    <p className="text-sm text-slate-400">Informatics and Computing Engineering Student @ FEUP</p>
                                </div>

                                <div className="bg-slate-700/50 rounded-lg p-4 border border-slate-600">
                                    <h3 className="text-lg font-semibold text-green-400 mb-2">Tomás Oliveira</h3>
                                    <p className="text-sm text-slate-400">Informatics and Computing Engineering Student @ FEUP</p>
                                </div>
                            </div>
                            <div className="mt-4 pt-4 border-t border-slate-600">
                                <p className="text-sm text-slate-400 italic">
                                    💡 <strong>Want to contribute?</strong> We welcome contributions from the community!
                                    Check out our GitHub repository to get started.
                                </p>
                            </div>
                        </div>

                        <h2 className="text-2xl font-semibold text-white mt-8 mb-4">Support</h2>
                        <p>
                            Need help? Our documentation and community support channels are available to assist
                            you in creating and deploying your smart contracts. Join our community to connect
                            with other developers and get expert guidance.
                        </p>
                    </div>
                </div>
            </div>

            <ScrollToTop />
        </div>
    );
};

export default AboutPage;