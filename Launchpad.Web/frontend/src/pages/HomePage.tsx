import React from 'react';
import { Code, Info, ChevronRight } from 'lucide-react';
import type { NavigationProps } from '../types';

const HomePage: React.FC<NavigationProps> = ({ onNavigate }) => {
    return (
        <div className="min-h-screen bg-slate-900 text-white">
            <div className="container mx-auto px-6 py-20">
                <div className="text-center mb-16">
                    <h1 className="text-6xl font-bold mb-4 bg-gradient-to-r from-blue-400 to-purple-600 bg-clip-text text-transparent">
                        Launchpad
                    </h1>
                    <p className="text-xl text-slate-400">Smart Contract Generator</p>
                </div>

                <div className="flex justify-center gap-8 flex-wrap">
                    <button
                        onClick={() => onNavigate('generate')}
                        className="group bg-slate-800 hover:bg-slate-700 border border-slate-600 rounded-lg p-8 w-64 transition-all duration-300 hover:scale-105 hover:shadow-xl hover:shadow-blue-500/20"
                    >
                        <div className="flex flex-col items-center text-center">
                            <Code className="w-12 h-12 mb-4 text-blue-400 group-hover:text-blue-300" />
                            <h3 className="text-xl font-semibold mb-2">Generate Contract</h3>
                            <p className="text-slate-400 text-sm">Create and customize your smart contract with advanced features</p>
                            <ChevronRight className="w-5 h-5 mt-4 text-slate-500 group-hover:text-white group-hover:translate-x-1 transition-all" />
                        </div>
                    </button>

                    <button
                        onClick={() => onNavigate('about')}
                        className="group bg-slate-800 hover:bg-slate-700 border border-slate-600 rounded-lg p-8 w-64 transition-all duration-300 hover:scale-105 hover:shadow-xl hover:shadow-purple-500/20"
                    >
                        <div className="flex flex-col items-center text-center">
                            <Info className="w-12 h-12 mb-4 text-purple-400 group-hover:text-purple-300" />
                            <h3 className="text-xl font-semibold mb-2">About Us</h3>
                            <p className="text-slate-400 text-sm">Learn more about our platform and smart contract generation</p>
                            <ChevronRight className="w-5 h-5 mt-4 text-slate-500 group-hover:text-white group-hover:translate-x-1 transition-all" />
                        </div>
                    </button>
                </div>
            </div>
        </div>
    );
};

export default HomePage;