namespace Moongy.RD.Launchpad.Data.Pocos;
public class CodeGenerationResult<TToken>
{
    public string? Code { get; set; }
    public List<string>? Errors { get; set; }
    public TToken? Model { get; set; }
}
