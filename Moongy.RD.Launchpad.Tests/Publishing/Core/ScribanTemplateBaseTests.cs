using Moongy.RD.Launchpad.ContractGenerator.Publishing.Core.Templates;
using Scriban;
using Scriban.Runtime;
using Xunit.Abstractions;

namespace Moongy.RD.Launchpad.Tests.Publishing.Core
{

    public class ScribanTemplateBaseTests
    {
        private readonly ITestOutputHelper _output;
        public ScribanTemplateBaseTests(ITestOutputHelper output) => _output = output;

        // basic demo model for template testing
        public class TestModel
        {
            public string Name { get; set; } = "DefaultName";
            public string Body { get; set; } = "DefaultBody";
        }

        [Fact]
        public void Render_BasicFields_ReplacesTokens()
        {
            const string tpl = "function {{Model.Name}}() { {{Model.Body}} }";
            var template = new ScribanTemplateBase<TestModel>(tpl);
            var result   = template.Render(new TestModel { Name = "Foo", Body = "return true;" });
            Assert.Equal("function Foo() { return true; }", result);
        }

        // testing how null properties are handled in templates
        [Fact]
        public void Render_NullBody_RendersEmptySlot()
        {
            const string tpl = "function {{Model.Name}}() { {{Model.Body}} }";
            var template = new ScribanTemplateBase<TestModel>(tpl);
            var result   = template.Render(new TestModel { Name = "Foo", Body = null });
            Assert.Equal("function Foo() {  }", result); // empty but still valid
        }

        // testing loops with collections of items
        public class ListModel
        {
            public IList<string>? Items { get; set; }
        }

        [Fact]
        public void Render_ListLoop_JoinsWithCommas()
        {
            const string tpl = """
            Items: {{ for i in Model.Items }}{{ i }}{{ if !for.last }}, {{ end }}{{ end }}
            """;
            var template = new ScribanTemplateBase<ListModel>(tpl);
            var result   = template.Render(new ListModel { Items = ["A", "B", "C"] });
            Assert.Equal("Items: A, B, C", result.Trim());
        }

        // testing conditionals and string filters
        public class ConditionalModel
        {
            public bool IsPublic { get; set; }
            public required string VarName { get; set; }
        }

        [Theory]
        [InlineData(true,  "public myvar;") ]
        [InlineData(false, "private myvar;") ]
        public void Render_ConditionalVisibility(bool isPublic, string expected)
        {
            const string tpl = "{{ if Model.IsPublic }}public{{ else }}private{{ end }} {{ Model.VarName | string.downcase }};";
            var template = new ScribanTemplateBase<ConditionalModel>(tpl);
            var result   = template.Render(new ConditionalModel { IsPublic = isPublic, VarName = "MyVar" });
            Assert.Equal(expected, result);
        }

        // comparing our wrapper with direct scriban api usage
        [Fact]
        public void HelperVsDirect_RenderSameOutput()
        {
            const string tpl = "Hello {{ Model.Name }}!";
            var model = new TestModel { Name = "World" };

            var helperResult = new ScribanTemplateBase<TestModel>(tpl).Render(model);

            var template = Template.Parse(tpl);
            var globals  = new ScriptObject { { "Model", model } };
            var ctx      = new TemplateContext();
            ctx.MemberRenamer = member => member.Name; // keep PascalCase like the wrapper
            ctx.PushGlobal(globals);
            var directResult = template.Render(ctx);

            _output.WriteLine($"helper → '{helperResult}', direct → '{directResult}'");
            Assert.Equal(helperResult, directResult);
        }
    }
}