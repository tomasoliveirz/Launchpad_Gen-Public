class SmartContractGenerator {
    constructor() {
        this.recipientCounter = 0;
        this.init();
    }

    init() {
        this.bindEvents();
        this.setupFormToggling();
    }

    bindEvents() {
        document.getElementById('contractForm').addEventListener('submit', (e) => this.handleFormSubmit(e));

        document.getElementById('hasTax').addEventListener('change', (e) => this.toggleTaxOptions(e.target.checked));

        document.getElementById('hasAccessControl').addEventListener('change', (e) => this.toggleAccessControlOptions(e.target.checked));

        document.getElementById('addRecipient').addEventListener('click', () => this.addTaxRecipient());

        document.getElementById('copyCode').addEventListener('click', () => this.copyToClipboard());
        document.getElementById('downloadCode').addEventListener('click', () => this.downloadContract());
    }

    setupFormToggling() {
        this.toggleTaxOptions(document.getElementById('hasTax').checked);
        this.toggleAccessControlOptions(document.getElementById('hasAccessControl').checked);

        if (document.getElementById('hasTax').checked) {
            this.addTaxRecipient();
        }
    }

    toggleTaxOptions(enabled) {
        const taxOptions = document.getElementById('taxOptions');
        if (enabled) {
            taxOptions.style.display = 'block';
            if (this.recipientCounter === 0) {
                this.addTaxRecipient();
            }
        } else {
            taxOptions.style.display = 'none';
        }
    }

    toggleAccessControlOptions(enabled) {
        const accessControlOptions = document.getElementById('accessControlOptions');
        accessControlOptions.style.display = enabled ? 'block' : 'none';
    }

    addTaxRecipient() {
        this.recipientCounter++;
        const recipientsList = document.getElementById('recipientsList');

        const recipientDiv = document.createElement('div');
        recipientDiv.className = 'recipient-item';
        recipientDiv.dataset.recipientId = this.recipientCounter;

        recipientDiv.innerHTML = `
            <div class="recipient-inputs">
                <div class="form-group">
                    <label>Recipient Address</label>
                    <input type="text" 
                           class="recipient-address" 
                           placeholder="0x..." 
                           pattern="^0x[a-fA-F0-9]{40}$"
                           title="Enter a valid Ethereum address">
                </div>
                <div class="form-group">
                    <label>Share (%)</label>
                    <input type="number" 
                           class="recipient-share" 
                           min="0" 
                           max="100" 
                           step="0.01"
                           placeholder="0">
                </div>
            </div>
            <button type="button" class="btn-danger" onclick="app.removeRecipient(${this.recipientCounter})">
                Remove
            </button>
        `;

        recipientsList.appendChild(recipientDiv);
    }

    removeRecipient(recipientId) {
        const recipientDiv = document.querySelector(`[data-recipient-id="${recipientId}"]`);
        if (recipientDiv) {
            recipientDiv.remove();
        }
    }

    getTaxRecipients() {
        const recipients = [];
        const recipientItems = document.querySelectorAll('.recipient-item');

        recipientItems.forEach(item => {
            const address = item.querySelector('.recipient-address').value.trim();
            const share = parseFloat(item.querySelector('.recipient-share').value);

            if (address && share > 0) {
                recipients.push({ address, share });
            }
        });

        return recipients;
    }

    validateForm() {
        const form = document.getElementById('contractForm');
        const formData = new FormData(form);
        const errors = [];

        // basic validation
        if (!formData.get('name').trim()) {
            errors.push('Token name is required');
        }

        if (!formData.get('symbol').trim()) {
            errors.push('Token symbol is required');
        }

        const totalSupply = parseInt(formData.get('totalSupply'));
        const premint = parseInt(formData.get('premint'));

        if (premint > totalSupply) {
            errors.push('Initial mint cannot exceed total supply');
        }

        // tax validation
        if (document.getElementById('hasTax').checked) {
            const recipients = this.getTaxRecipients();
            const totalShares = recipients.reduce((sum, r) => sum + r.share, 0);

            if (recipients.length === 0) {
                errors.push('At least one tax recipient is required when tax is enabled');
            } else if (totalShares > 100) {
                errors.push('Total tax recipient shares cannot exceed 100%');
            }
        }

        return errors;
    }

    async handleFormSubmit(e) {
        e.preventDefault();

        // validate form
        const errors = this.validateForm();
        if (errors.length > 0) {
            alert('Please fix the following errors:\n\n' + errors.join('\n'));
            return;
        }

        this.showLoading();

        const formData = this.collectFormData();

        try {
            const response = await fetch('/api/generate', {
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify(formData)
            });

            const data = await response.json();

            if (data.success) {
                this.showResult(data.code);
            } else {
                this.showError(data.error);
            }
        } catch (error) {
            this.showError(error.message);
        } finally {
            this.hideLoading();
        }
    }

    collectFormData() {
        const form = document.getElementById('contractForm');
        const formData = new FormData(form);

        return {
            name: formData.get('name').trim(),
            symbol: formData.get('symbol').trim(),
            decimals: parseInt(formData.get('decimals')),
            premint: parseInt(formData.get('premint')),
            supply: parseInt(formData.get('totalSupply')),
            hasMinting: document.getElementById('hasMinting').checked,
            hasBurning: document.getElementById('hasBurning').checked,
            isPausable: document.getElementById('isPausable').checked,
            hasTax: document.getElementById('hasTax').checked,
            taxFee: parseFloat(document.getElementById('taxFee').value) || 0,
            taxRecipients: this.getTaxRecipients(),
            hasAccessControl: document.getElementById('hasAccessControl').checked,
            accessControlType: parseInt(document.getElementById('accessControlType').value),
            roles: []
        };
    }

    showLoading() {
        document.getElementById('loading').style.display = 'flex';
        document.getElementById('result').style.display = 'none';
    }

    hideLoading() {
        document.getElementById('loading').style.display = 'none';
    }

    showResult(code) {
        document.getElementById('contractCode').value = code;
        document.getElementById('result').style.display = 'block';

        // Scroll to result
        document.getElementById('result').scrollIntoView({ behavior: 'smooth' });
    }

    showError(message) {
        alert('Error generating contract:\n\n' + message);
    }

    copyToClipboard() {
        const code = document.getElementById('contractCode');
        code.select();
        code.setSelectionRange(0, 99999); // For mobile devices

        try {
            document.execCommand('copy');
            this.showToast('Contract copied to clipboard!');
        } catch (err) {
            console.error('Failed to copy: ', err);
            alert('Failed to copy to clipboard');
        }
    }

    downloadContract() {
        const code = document.getElementById('contractCode').value;
        const name = document.getElementById('name').value.trim() || 'Contract';

        const blob = new Blob([code], { type: 'text/plain' });
        const url = URL.createObjectURL(blob);

        const a = document.createElement('a');
        a.href = url;
        a.download = `${name}.sol`;
        document.body.appendChild(a);
        a.click();
        document.body.removeChild(a);

        URL.revokeObjectURL(url);
        this.showToast('Contract downloaded!');
    }

    showToast(message) {
        const toast = document.createElement('div');
        toast.textContent = message;
        toast.style.cssText = `
            position: fixed;
            top: 20px;
            right: 20px;
            background: #10b981;
            color: white;
            padding: 12px 20px;
            border-radius: 4px;
            z-index: 1001;
            font-weight: 500;
        `;

        document.body.appendChild(toast);

        setTimeout(() => {
            document.body.removeChild(toast);
        }, 3000);
    }
}

const app = new SmartContractGenerator();