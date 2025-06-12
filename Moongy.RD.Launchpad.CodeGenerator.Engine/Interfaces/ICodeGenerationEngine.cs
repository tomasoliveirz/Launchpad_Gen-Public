namespace Moongy.RD.Launchpad.CodeGenerator.Engine.Interfaces
{
    public interface ICodeGenerationEngine
    {
        Task<string> GenerateAsync<TForm>(TForm form) where TForm : class;
    }
}