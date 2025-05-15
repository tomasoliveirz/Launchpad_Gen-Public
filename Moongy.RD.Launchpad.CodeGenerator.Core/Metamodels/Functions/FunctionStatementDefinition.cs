using Moongy.RD.Launchpad.CodeGenerator.Core.Metamodels.Others;

namespace Moongy.RD.Launchpad.CodeGenerator.Core.Metamodels.Functions;

public class FunctionStatementDefinition
{
    public FunctionStatementKind Kind { get; set; }


    #region Local Declaration
    public ParameterDefinition? LocalParameter { get; set; }
    #endregion

    #region Assignment
    public AssignmentDefinition? ParameterAssignment { get; set; }
    #endregion


    #region Expression
    public ExpressionDefinition? Expression { get; set; }
    #endregion

    #region Condition
    public List<ConditionBranch> ConditionBranches { get; set; } = [];
    #endregion

    #region ConditionLoop
    public ExpressionDefinition? LoopCondition { get; set; }

    #endregion

    #region Range Loop
    public ParameterDefinition? Iterator { get; set; }
    public ExpressionDefinition? RangeStart { get; set; }
    public ExpressionDefinition? RangeEnd { get; set; }
    #endregion

    #region Loops
    public List<FunctionStatementDefinition>? LoopBody { get; set; }
    #endregion

    #region Trigger
    public TriggerDefinition? Trigger { get; set; }
    public List<ExpressionDefinition> TriggerArguments { get; set; } = [];
    #endregion

    #region Return
    public List<ExpressionDefinition> ReturnValues { get; set; } = [];
    #endregion
}