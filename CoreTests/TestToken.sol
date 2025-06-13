
// SPDX-License-Identifier: MIT
pragma solidity >= 0.8.20;

contract TestToken {
	error OwnableUnauthorizedAccount(address account);
	error OwnableInvalidOwner(address owner);

	event Transfer(address from, address to, uint256 value);

	event Approval(address owner, address spender, uint256 value);

	event OwnershipTransferred(address previousOwner, address newOwner);


	string public symbol = "TEST";
	string public decimals = "18";
	uint256 public max_supply = 0;
	string public name = "TestToken";
	string private _name;
	string private _symbol;
	uint8 private _decimals = 18;
	uint256 private _totalSupply;
	mapping(address => uint256) private _balances;
	mapping(address => mapping(address => uint256)) private _allowances;
	uint256 private taxFee;
	address[] private _taxRecipients;
	address private _owner;

	modifier onlyOwner() {
	    // Modifier onlyOwner implementation
	    _;
	    }

	// Error rendering constructor for TestToken: Constructor has duplicate parameter names.

	function name()
	    public returns (string) {
	    return _name;


	}

	function symbol()
	    public returns (string) {
	    return _symbol;


	}

	function decimals()
	    public returns (uint8) {
	    return _decimals;


	}

	function totalSupply()
	    public returns (uint256) {
	    return _totalSupply;


	}

	function balanceOf(address account)
	    public returns (uint256) {
	    return _balances[account];


	}

	function allowance(address owner, address spender)
	    public returns (uint256) {
	    return _allowances[owner][spender];


	}

	function transfer(address to, uint256 value)
	    public returns (bool) {
	    _transfer(msg.sender, to, value)
	    return true;


	}

	function transferFrom(address from, address to, uint256 value)
	    public returns (bool) {
	    _spendAllowance(from, msg.sender, value)
	    _transfer(from, to, value)
	    return true;


	}

	function approve(address spender, uint256 value)
	    public returns (bool) {
	    _approve(msg.sender, spender, value)
	    return true;


	}

	function _transfer(address from, address to, uint256 value)
	    internal {
	    require(from != address(0), "Transfer from zero address")
	    require(to != address(0), "Transfer to zero address")
	    uint256  amountToTax= value * taxFee / 100;
	    uint256  netValue= value - amountToTax;
	    _balances[from] = _balances[from] - value;
	    _balances[to] = _balances[to] + value;
	    emit Transfer(from, to, value);

    
	    if (amountToTax > 0) {
	        _balances[_owner] = _balances[_owner] + amountToTax;
	        emit Transfer(from, _owner, amountToTax);

	    }


	}

	function _approve(address owner, address spender, uint256 value)
	    internal {
	    require(owner != address(0), "Approve from zero address")
	    require(spender != address(0), "Approve to zero address")
	    _allowances[owner][spender] = value;
	    emit Approval(owner, spender, value);


	}

	function _update(address from, address to, uint256 value)
	    internal {
    
	    if (from == address(0)) {
	        _totalSupply = _totalSupply + value;
	    }
	    else {

	            if (_balances[from] < value) {
	                revert ();

	            }

	        _balances[from] = fromBalance - value;
	    }

    
	    if (to == address(0)) {
	        _totalSupply = _totalSupply - value;
	    }
	    else {
	        _balances[to] = _balances[to] + value;
	    }

	    emit Transfer();


	}

	function _spendAllowance(address owner, address spender, uint256 value)
	    internal {
	    uint256  currentAllowance= ;
	    currentAllowance = allowance(owner, spender);
    
	    if (currentAllowance < type(uint256).max) {

	            if (currentAllowance < value) {
	                revert (spender, currentAllowance, value);

	            }

	    }


	}

}

