import React from 'react';
import { FormSectionProps } from '../../types/contract.ts';

const AccessControlForm: React.FC<FormSectionProps> = ({ config, updateConfig }) => {
    return (
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

                    <div className="mt-3 text-sm text-slate-400">
                        {config.accessControlType === 0 ? (
                            <p>Single owner can control all administrative functions like minting, pausing, and tax management.</p>
                        ) : (
                            <p>Role-based system allows for more granular permission management with different roles for different functions.</p>
                        )}
                    </div>
                </div>
            )}
        </div>
    );
};

export default AccessControlForm;