import React from 'react';
import { Home, Code, Info } from 'lucide-react';
import type { NavigationProps, Page } from '../../types';

interface SidebarProps extends NavigationProps {
    currentPage: Page;
}

const Sidebar: React.FC<SidebarProps> = ({ onNavigate, currentPage }) => {
    const navItems = [
        {
            id: 'home' as Page,
            icon: Home,
            active: currentPage === 'home'
        },
        {
            id: 'generate' as Page,
            icon: Code,
            active: currentPage === 'generate'
        },
        {
            id: 'about' as Page,
            icon: Info,
            active: currentPage === 'about'
        }
    ];

    return (
        <div
            className="fixed left-0 top-0 bottom-0 w-16 bg-slate-800 border-r border-slate-700 flex flex-col items-center justify-start pt-8 z-50"
            style={{ height: '100vh' }}
        >
            <div className="flex flex-col gap-4">
                {navItems.map((item) => {
                    const Icon = item.icon;
                    return (
                        <button
                            key={item.id}
                            onClick={() => onNavigate(item.id)}
                            style={{ background: 'none', border: 'none', outline: 'none' }}
                            className={`
                w-14 h-14 flex items-center justify-center transition-all duration-200
                ${item.active
                                    ? 'text-blue-400'
                                    : 'text-slate-400 hover:text-white'
                                }
              `}
                        >
                            <Icon className="w-8 h-8" />
                        </button>
                    );
                })}
            </div>
        </div>
    );
};

export default Sidebar;