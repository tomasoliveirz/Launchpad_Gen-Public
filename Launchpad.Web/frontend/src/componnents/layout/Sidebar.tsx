import React from 'react';
import { Home, Code, Info } from 'lucide-react';
import type { NavigationProps, Page } from '../../types';

interface SidebarProps extends NavigationProps {
    currentPage: Page;
}

const Sidebar: React.FC<SidebarProps> = ({ onNavigate, currentPage }) => {
    const navItems = [
        { id: 'home' as Page, icon: Home, active: currentPage === 'home' },
        { id: 'generate' as Page, icon: Code, active: currentPage === 'generate' },
        { id: 'about' as Page, icon: Info, active: currentPage === 'about' }
    ];

    return (
        <nav className="sidebar">
            {navItems.map((item) => {
                const Icon = item.icon;
                return (
                    <button
                        key={item.id}
                        onClick={() => onNavigate(item.id)}
                        className={item.active ? 'active' : ''}
                        aria-label={item.id}
                        type="button"
                    >
                        <Icon />
                    </button>
                );
            })}
        </nav>
    );
};

export default Sidebar;
