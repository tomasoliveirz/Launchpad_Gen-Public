using Moongy.RD.Launchpad.Tools.Aissistant.Enums;

namespace Moongy.RD.Launchpad.Tools.Aissistant.Constants
{
    public class Prompts
    {
        public static readonly Dictionary<OperationType, string> OperationFor = new()
        {
            [OperationType.Analyze] = ANALYZE_PROMPT??"",
            [OperationType.Document] = DOCUMENT_PROMPT ?? "",
            [OperationType.Format] = FORMAT_PROMPT ?? "",
            [OperationType.Generate] = GENERATE_PROMPT ?? "",
            [OperationType.Optimize] = OPTIMIZE_PROMPT ?? "",
        };

        private static string GENERATE_PROMPT = @"You are an expert Solidity engineer. 
                                                  Generate a complete smart contract based on the following description:
                                                  {Description}
                                                  • Adhere to best security practices.
                                                  • Output only valid {Language} {Version} code.
                                                  ";

        private static string ANALYZE_PROMPT => @"You are a smart‐contract security auditor. 
                                                  Given this Solidity code:
                                                  {Code}
                                                  and this context:
                                                    {Description}
                                                    • List all security vulnerabilities with line references.
                                                    • Suggest fixes in bullet points.
";

        private static string DOCUMENT_PROMPT => @"You are a documentation specialist. 
                                                    Given this Solidity code:
                                                    {Code}
                                                    and its intended purpose:
                                                    {Description}
                                                    • Add NatSpec comments to every function and event.
                                                    • Do not change any logic.";

        private static string FORMAT_PROMPT => @"You are a code formatter. 
                                                Given this Solidity code:
                                                {Code}
                                                • Reformat it to 4-space indentation.
                                                • Order imports and contracts per the Solidity Style Guide.
                                                • Output only the reformatted code.";


        private static string OPTIMIZE_PROMPT => @"You are a gas-optimization expert. 
                                                    Given this Solidity code:
                                                    {Code}
                                                    • Remove dead code and redundant state.
                                                    • Inline or refactor to minimize gas.
                                                    • Show the optimized contract only.";
    }
}
