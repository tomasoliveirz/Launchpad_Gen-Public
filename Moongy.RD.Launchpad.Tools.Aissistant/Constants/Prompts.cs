using Moongy.RD.Launchpad.Tools.Aissistant.Enums;

namespace Moongy.RD.Launchpad.Tools.Aissistant.Constants
{
    public class Prompts
    {
   

        private static string GENERATE_PROMPT = @"You are an expert Solidity engineer. 
                                                  Generate a complete smart contract based on the following description:
                                                  {Description}
                                                  • Adhere to best security practices.
                                                  • Output only valid {Language} {Version} code.
                                                  • Respond with only the final Solidity code. Do not include any explanations or commentary.
                                                  ";

        private static string ANALYZE_PROMPT => @"You are a smart‐contract security auditor. 
                                                  Given this Solidity code:
                                                  {Code}
                                                  and this context:
                                                    {Description}
                                                    • List all security vulnerabilities with line references.
                                                    • Suggest fixes in bullet points.
                                                    **Provide only the annotated Solidity code (with line comments showing issues/fixes). No other text.**
";

        private static string DOCUMENT_PROMPT => @"You are a documentation specialist. 
                                                    Given this Solidity code:
                                                    {Code}
                                                    and its intended purpose:
                                                    {Description}
                                                    • Add NatSpec comments to every function and event.
                                                    • Do not change any logic.
                                                    **Return only the fully commented Solidity code. Do not include any additional text.**";

        private static string FORMAT_PROMPT => @"You are a code formatter. 
                                                Given this Solidity code:
                                                {Code}
                                                • Reformat it to 4-space indentation.
                                                • Order imports and contracts per the Solidity Style Guide.
                                                • Output only the reformatted code.
                                                **Respond with only the reformatted Solidity code. No explanations.**";


        private static string OPTIMIZE_PROMPT => @"You are a gas-optimization expert. 
                                                    Given this Solidity code:
                                                    {Code}
                                                    • Remove dead code and redundant state.
                                                    • Inline or refactor to minimize gas.
                                                    • Show the optimized contract only.
                                                    **Provide only the optimized Solidity code. Do not include any commentary.**";

         public static readonly Dictionary<OperationType, string> OperationFor = new()
         {
             [OperationType.Analyze] = ANALYZE_PROMPT ?? "",
             [OperationType.Document] = DOCUMENT_PROMPT ?? "",
             [OperationType.Format] = FORMAT_PROMPT ?? "",
             [OperationType.Generate] = GENERATE_PROMPT ?? "",
             [OperationType.Optimize] = OPTIMIZE_PROMPT ?? "",
         };
    }
}
