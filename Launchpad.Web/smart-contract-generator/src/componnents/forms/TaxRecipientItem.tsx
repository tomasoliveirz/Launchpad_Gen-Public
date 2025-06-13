import React from 'react';
import { Trash2 } from 'lucide-react';
import { TaxRecipient } from './types/contract.ts';
interface TaxRecipientItemProps {
    recipient: TaxRecipient;
    onUpdate: (field: 'address' | 'share', value: string | number) => void;
    onRemove: () => void;
}

const TaxRecipientItem: React.FC<TaxRecipientItemProps> = ({
    recipient,
    onUpdate,
    onRemove
}) => {
    return (
        <div className="grid grid-cols-1 md:grid-cols-3 gap-3 p-3 bg-slate-700/50 rounded border border-slate-600">
            <div className="md:col-span-2">
                <label className="block text-sm font-medium mb-1">
                    Recipient Address
                </label>
                <input
                    type="text"
                    value={recipient.address}
                    onChange={(e) => onUpdate('address', e.target.value)}
                    placeholder="0x..."
                    pattern="^0x[a-fA-F0-9]{40}$"
                    className="w-full bg-slate-700 border border-slate-600 rounded px-3 py-2 focus:ring-2 focus:ring-blue-500 focus:border-transparent text-sm"
                />
            </div>

            <div className="flex gap-2">
                <div className="flex-1">
                    <label className="block text-sm font-medium mb-1">
                        Share (%)
                    </label>
                    <input
                        type="number"
                        min="0"
                        max="100"
                        step="0.01"
                        value={recipient.share}
                        onChange={(e) => onUpdate('share', parseFloat(e.target.value) || 0)}
                        className="w-full bg-slate-700 border border-slate-600 rounded px-3 py-2 focus:ring-2 focus:ring-blue-500 focus:border-transparent text-sm"
                    />
                </div>
                <button
                    type="button"
                    onClick={onRemove}
                    className="self-end p-2 text-red-400 hover:text-red-300 hover:bg-red-500/20 rounded transition-colors"
                    title="Remove recipient"
                >
                    <Trash2 className="w-4 h-4" />
                </button>
            </div>
        </div>
    );
};

export default TaxRecipientItem;