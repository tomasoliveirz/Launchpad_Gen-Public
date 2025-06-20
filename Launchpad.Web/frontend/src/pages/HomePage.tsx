import React from 'react';
import { Code, Info, ChevronRight } from 'lucide-react';
import type { NavigationProps } from '../types';
import '../App.css';

const HomePage: React.FC<NavigationProps> = ({ onNavigate }) => {
    return (
        <div className="home-main">
            <h1 className="home-heading">Launchpad</h1>
            <p className="home-subtitle">Smart Contract Generator</p>

            <div className="home-button-group">
                <button onClick={() => onNavigate('generate')}>
                    <div className="flex flex-col items-center text-center">
                        <Code className="home-icon" />
                        <h3>Generate Contract</h3>
                        <p>Create and customize your smart contract with advanced features</p>
                        <ChevronRight className="home-chevron" />
                    </div>
                </button>

                <button onClick={() => onNavigate('about')}>
                    <div className="flex flex-col items-center text-center">
                        <Info className="home-icon" />
                        <h3>About Us</h3>
                        <p>Learn more about our platform and smart contract generation</p>
                        <ChevronRight className="home-chevron" />
                    </div>
                </button>
            </div>
        </div>

    );
};

export default HomePage;
