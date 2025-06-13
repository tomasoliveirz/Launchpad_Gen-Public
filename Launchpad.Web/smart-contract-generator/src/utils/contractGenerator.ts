import type { ContractConfig } from '../types';

export const generateSolidityContract = (config: ContractConfig): string => {
    const contractName = config.name.replace(/\s+/g, '');

    const imports = [
        'import "@openzeppelin/contracts/token/ERC20/ERC20.sol";'
    ];

    if (config.hasMinting || config.hasAccessControl) {
        imports.push('import "@openzeppelin/contracts/access/Ownable.sol";');
    }

    if (config.hasBurning) {
        imports.push('import "@openzeppelin/contracts/token/ERC20/extensions/ERC20Burnable.sol";');
    }

    if (config.isPausable) {
        imports.push('import "@openzeppelin/contracts/security/Pausable.sol";');
    }

    const inheritance = ['ERC20'];

    if (config.hasBurning) {
        inheritance.push('ERC20Burnable');
    }

    if (config.isPausable) {
        inheritance.push('Pausable');
    }

    if (config.hasAccessControl) {
        inheritance.push('Ownable');
    }

    const taxSystemCode = config.hasTax ? `
    uint256 public taxFee = ${Math.floor(config.taxFee * 100)}; // ${config.taxFee}% in basis points
    
    struct TaxRecipient {
        address recipient;
        uint256 share; // Share in basis points (100 = 1%)
    }
    
    TaxRecipient[] public taxRecipients;
    
    event TaxDistributed(address indexed recipient, uint256 amount);
    event TaxFeeUpdated(uint256 newFee);
    ` : '';

    const constructorCode = `
    constructor() ERC20("${config.name}", "${config.symbol}") {
        _mint(msg.sender, ${config.premint} * 10**${config.decimals});
        ${config.hasTax ? config.taxRecipients.map((r: any) => `
        taxRecipients.push(TaxRecipient(${r.address || '0x0000000000000000000000000000000000000000'}, ${Math.floor(r.share * 100)}));`).join('') : ''}
    }`;

    const additionalFunctions = [];

    if (config.decimals !== 18) {
        additionalFunctions.push(`
    function decimals() public pure override returns (uint8) {
        return ${config.decimals};
    }`);
    }

    if (config.hasMinting && config.hasAccessControl) {
        additionalFunctions.push(`
    function mint(address to, uint256 amount) public onlyOwner {
        _mint(to, amount);
    }`);
    }

    if (config.hasTax) {
        additionalFunctions.push(`
    function _transfer(address from, address to, uint256 amount) internal override ${config.isPausable ? 'whenNotPaused ' : ''}{
        if (taxFee > 0 && from != owner() && to != owner()) {
            uint256 taxAmount = (amount * taxFee) / 10000;
            uint256 transferAmount = amount - taxAmount;
            
            // Distribute tax to recipients
            for (uint256 i = 0; i < taxRecipients.length; i++) {
                uint256 recipientAmount = (taxAmount * taxRecipients[i].share) / 10000;
                if (recipientAmount > 0) {
                    super._transfer(from, taxRecipients[i].recipient, recipientAmount);
                    emit TaxDistributed(taxRecipients[i].recipient, recipientAmount);
                }
            }
            
            super._transfer(from, to, transferAmount);
        } else {
            super._transfer(from, to, amount);
        }
    }
    
    function updateTaxFee(uint256 newFee) public onlyOwner {
        require(newFee <= 1000, "Tax fee cannot exceed 10%");
        taxFee = newFee;
        emit TaxFeeUpdated(newFee);
    }
    
    function addTaxRecipient(address recipient, uint256 share) public onlyOwner {
        require(recipient != address(0), "Invalid recipient address");
        require(share > 0, "Share must be greater than 0");
        taxRecipients.push(TaxRecipient(recipient, share));
    }
    
    function removeTaxRecipient(uint256 index) public onlyOwner {
        require(index < taxRecipients.length, "Invalid index");
        taxRecipients[index] = taxRecipients[taxRecipients.length - 1];
        taxRecipients.pop();
    }`);
    } else if (config.isPausable) {
        additionalFunctions.push(`
    function _beforeTokenTransfer(address from, address to, uint256 amount) internal override whenNotPaused {
        super._beforeTokenTransfer(from, to, amount);
    }`);
    }

    if (config.isPausable && config.hasAccessControl) {
        additionalFunctions.push(`
    function pause() public onlyOwner {
        _pause();
    }
    
    function unpause() public onlyOwner {
        _unpause();
    }`);
    }

    return `// SPDX-License-Identifier: MIT
pragma solidity ^0.8.20;

${imports.join('\n')}

/**
 * @title ${contractName}
 * @dev ERC20 Token with advanced features
 * 
 * Features:
 * - Name: ${config.name}
 * - Symbol: ${config.symbol}
 * - Decimals: ${config.decimals}
 * - Total Supply: ${config.totalSupply.toLocaleString()}
 * - Initial Mint: ${config.premint.toLocaleString()}${config.hasMinting ? '\n * - Mintable: Yes' : ''}${config.hasBurning ? '\n * - Burnable: Yes' : ''}${config.isPausable ? '\n * - Pausable: Yes' : ''}${config.hasTax ? `\n * - Tax System: ${config.taxFee}% transaction tax` : ''}${config.hasAccessControl ? '\n * - Access Control: Owner-based' : ''}
 */
contract ${contractName} is ${inheritance.join(', ')} {${taxSystemCode}${constructorCode}${additionalFunctions.join('')}
}`;
};

export const downloadContract = (code: string, filename: string) => {
    const blob = new Blob([code], { type: 'text/plain' });
    const url = URL.createObjectURL(blob);

    const link = document.createElement('a');
    link.href = url;
    link.download = `${filename}.sol`;
    document.body.appendChild(link);
    link.click();
    document.body.removeChild(link);

    URL.revokeObjectURL(url);
};