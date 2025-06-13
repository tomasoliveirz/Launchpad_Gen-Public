import React from 'react';
import { Plus } from 'lucide-react';
import { TaxSystemProps } from '../../types/contract.ts';
import TaxRecipientItem from './TaxRecipientItem';
import Button from '../common/Button.tsx';

const TaxSystemForm: React.FC<TaxSystemProps> = ({
    config,
    updateConfig,
    addTaxRecipient,
    removeTaxRecipient,
    updateTaxRecipient
}) => {
    const handleTaxToggle = (enabled: boolean) => {
        updateConfig({ hasTax: enabled });
        if (enabled && config.taxRecipients.length === 0) {
            addTaxRecipient();
        }
    };

    const totalShares = config.taxRecipients.reduce((sum, r) => sum + r.share, 0);

    return (
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
                        <p className="text-sm text-slate-400 mt-1">
                            Maximum recommended: 10%
                        </p>
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
                            <Button
                                type="button"
                                onClick={addTaxRecipient}
                                variant="secondary"
                                size="sm"
                                className="flex items-center gap-2"
                            >
                                <Plus className="w-4 h-4" />
                                Add Recipient
                            </Button>
                        </div>

                        <div className="space-y-3">
                            {config.taxRecipients.length === 0 ? (
                                <div className="text-center py-8 text-slate-400">
                                    <p>No tax recipients configured</p>
                                    <p className="text-sm">Click "Add Recipient" to start</p>
                                </div>
                            ) : (
                                config.taxRecipients.map((recipient) => (
                                    <TaxRecipientItem
                                        key={recipient.id}
                                        recipient={recipient}
                                        onUpdate={(field, value) => updateTaxRecipient(recipient.id, field, value)}
                                        onRemove={() => removeTaxRecipient(recipient.id)}
                                    />
                                ))
                            )}
                        </div>
                    </div>
                </div>
            )}
        </div>
    );
};

export default TaxSystemForm;