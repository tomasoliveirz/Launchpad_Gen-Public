namespace Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Enums;

/// <summary>
/// Define os diferentes tipos de funções disponíveis em Solidity.
/// </summary>
public enum SolidityFunctionTypeEnum
{
    /// <summary>Função regular com nome.</summary>
    Normal,
    
    /// <summary>Construtor do smart contract.</summary>
    Constructor,
    
    /// <summary>Função receive (para receber Ether sem dados).</summary>
    Receive,
    
    /// <summary>Função fallback (para chamadas de função desconhecidas).</summary>
    Fallback,
    
    /// <summary>Função livre fora do contrato.</summary>
    Free
}