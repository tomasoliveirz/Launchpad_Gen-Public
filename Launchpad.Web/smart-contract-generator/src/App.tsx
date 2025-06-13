import React, { useState } from 'react';
import HomePage from './pages/HomePage';
import GenerateContractPage from './pages/GenerateContractPage';
import AboutPage from './pages/AboutPage';
import Sidebar from './componnents/layout/Sidebar';
import type { Page } from './types';

const App: React.FC = () => {
    const [currentPage, setCurrentPage] = useState<Page>('home');

    const navigateTo = (page: Page) => {
        setCurrentPage(page);
    };

    const renderCurrentPage = () => {
        switch (currentPage) {
            case 'home':
                return <HomePage onNavigate={navigateTo} />;
            case 'generate':
                return <GenerateContractPage onNavigate={navigateTo} />;
            case 'about':
                return <AboutPage onNavigate={navigateTo} />;
            default:
                return <HomePage onNavigate={navigateTo} />;
        }
    };

    return (
        <div className="flex min-h-screen bg-slate-900">
            
            <Sidebar onNavigate={navigateTo} currentPage={currentPage} />

            <div className="flex-1 ml-16">
                {renderCurrentPage()}
            </div>
        </div>
    );
};

export default App;