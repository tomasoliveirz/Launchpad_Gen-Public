using Moongy.RD.Launchpad.Core.Enums;
using Moongy.RD.Launchpad.Core.ExtensionMethods;
using Moongy.RD.Launchpad.Generator.Contracts.AdvancedFungibleToken.Models;
using Moongy.RD.Launchpad.Generator.Contracts.Core.Models;
using Moongy.RD.Launchpad.Generator.Contracts.FungibleToken.Models;
using Moongy.RD.Launchpad.Generator.Contracts.NonFungibleToken.Models;
using Moongy.RD.Launchpad.Generator.Contracts.SemiFungibleToken.Models;
using Moongy.RD.Launchpad.Tools.TokenWizard.Models;

namespace Moongy.RD.Launchpad.Tools.TokenWizard
{
    public class GovernanceModel : BaseContractModel { }

    public class TokenWizard : ITokenWizard
    {
        public TokenWizardResponse GetToken(TokenWizardRequest request)
        {
            var decisionNode = GenerateDecisionTree();
            foreach(var response in request.Responses)
            {
                var choice = decisionNode.Results.Keys.FirstOrDefault(x => x == response);
                if (choice == null) throw new Exception("Invalid choice");
                decisionNode = decisionNode.Results[response];
            }
            if (decisionNode.IsQuestion)
                return new TokenWizardResponse()
                {
                    Question = decisionNode.Value,
                    PreviousResponses = request.Responses,
                    PossibleAnswers = decisionNode.Results.Select(x => x.Key)
                };

            return new TokenWizardResponse()
            {
                Token = decisionNode.Value,
                Tags = decisionNode.Tags
            };
        }

        private TokenWizardDecisionTreeNode GenerateDecisionTree()
        {
            return GetNonTokenDecisionNode();
        }

        #region Question Nodes 

        private TokenWizardDecisionTreeNode GetComplexTokenDecisionNode()
        {
            return new TokenWizardDecisionTreeNode()
            {
                Value = FungibleTokenQuestion,
                Results = new Dictionary<string, TokenWizardDecisionTreeNode>()
                            {
                                { NoAnswer, GetResponseNode<FungibleTokenModel>()},
                                {YesAnswer, GetResponseNode<AdvancedFungibleTokenModel>()}
                            }
            };
        }

        private TokenWizardDecisionTreeNode GetMultipleTokenTypeDecisionNode()
        {
            return new TokenWizardDecisionTreeNode()
            {
                Value = FungibleTokenQuestion,
                Results = new Dictionary<string, TokenWizardDecisionTreeNode>()
                            {
                                { NoAnswer, GetComplexTokenDecisionNode()},
                                {YesAnswer, GetResponseNode<SemiFungibleTokenModel>()}
                            }
            };
        }

        private TokenWizardDecisionTreeNode GetCollectionDecisionNode()
        {
            return new TokenWizardDecisionTreeNode()
            {
                Value = CollectableQuestion,
                Results = new Dictionary<string, TokenWizardDecisionTreeNode>()
                            {
                                { NoAnswer, GetMultipleCopiesDecisionNode()},
                                {YesAnswer, GetResponseNode<NonFungibleTokenModel>()}
                            }
            };
        }

        private TokenWizardDecisionTreeNode GetMultipleCopiesDecisionNode()
        {
            return new TokenWizardDecisionTreeNode()
            {
                Value = CollectableQuestion,
                Results = new Dictionary<string, TokenWizardDecisionTreeNode>()
                            {
                                { NoAnswer, GetResponseNode<NonFungibleTokenModel>()},
                                {YesAnswer, GetResponseNode<SemiFungibleTokenModel>()}
                            }
            };
        }

        private TokenWizardDecisionTreeNode GetFungibleTokenDecisionNode()
        {
            return new TokenWizardDecisionTreeNode()
            {
                Value = FungibleTokenQuestion,
                Results = new Dictionary<string, TokenWizardDecisionTreeNode>()
                            {
                                { NoAnswer, GetCollectionDecisionNode()},
                                {YesAnswer, GetMultipleTokenTypeDecisionNode()}
                            }
            };
        }

        private TokenWizardDecisionTreeNode GetNonTokenDecisionNode()
        {
            return new TokenWizardDecisionTreeNode() { Value = NonTokenQuestion, Results = new Dictionary<string, TokenWizardDecisionTreeNode>()
            {
                { NoAnswer, GetResponseNode<GovernanceModel>()},
                {YesAnswer, GetFungibleTokenDecisionNode()}
            } };
        }
        
        #endregion

        #region Response Nodes

        private TokenWizardDecisionTreeNode GetResponseNode<TokenType>() where TokenType : BaseContractModel
        {
            var type = typeof(TokenType);
            return new TokenWizardDecisionTreeNode() { IsQuestion = false, Value = type.GetTokenName(), Tags = type.GetTokenTags()   };
        }

        #endregion

        #region Questions
        private static string NonTokenQuestion => "Is it a token?";
        private static string FungibleTokenQuestion => "Do all units have the same value?";
        private static string ComplexFungibleTokenQuestion => "Does it require operators or complex operations?";
        private static string CollectableQuestion => "Does it represent collectables?";
        private static string MultipleQuestion => "Does it allow multiples of the same unit?";

        #endregion

        #region Answers
        private static string YesAnswer => "Yes";
        private static string NoAnswer => "No";
        #endregion
    }

    public class TokenWizardDecisionTreeNode
    {
        public string Value { get; set; }
        public TokenClassification[] Tags { get; set; }
        public bool IsQuestion { get; set; } = true;
        public Dictionary<string, TokenWizardDecisionTreeNode> Results { get; set; }
    }
}
