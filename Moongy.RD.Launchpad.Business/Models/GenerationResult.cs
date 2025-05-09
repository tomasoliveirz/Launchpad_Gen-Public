using Moongy.RD.Launchpad.Data.Forms;

namespace Moongy.RD.Launchpad.Business.Models;

public class GenerationResult<TForm> where TForm:TokenBaseModel
{
    public required string Code { get; set; }
    public required TForm UpdatedModel { get; set; }
}
