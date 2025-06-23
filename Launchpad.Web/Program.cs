using Moongy.RD.Launchpad.CodeGenerator.Engine.Extensions;
using Moongy.RD.Launchpad.CodeGenerator.Engine.Interfaces;
using Moongy.RD.Launchpad.Data.Forms;
using Moongy.RD.Launchpad.Data.Forms.Extensions;
using Moongy.RD.Launchpad.Data.Forms.Tokenomics;

var builder = WebApplication.CreateBuilder(args);

// add code generation engine services
builder.Services.AddCodeGenerationEngine();

var app = builder.Build();

// serve static files
app.UseDefaultFiles(); // serves index.html by default
app.UseStaticFiles();

// api endpoint to generate smart contract
app.MapPost("/api/generate", async (GenerateRequest request, ICodeGenerationEngine engine) =>
{
    try
    {
        var form = new FungibleTokenForm
        {
            Name = request.Name,
            Symbol = request.Symbol,
            Decimals = (byte)request.Decimals,
            Premint = (ulong)request.Premint,
            MaxSupply = (ulong) request.Supply,
            HasMinting = request.HasMinting,
            HasBurning = request.HasBurning,
            IsPausable = request.IsPausable
        };

        // add tax if enabled
        if (request.HasTax)
        {
            form.Tax = new TaxTokenomic
            {
                TaxFee = request.TaxFee,
                Recipients = request.TaxRecipients?.Select(r => new TaxRecipient 
                { 
                    Address = r.Address, 
                    Share = (int)r.Share 
                }).ToList() ?? new List<TaxRecipient>()
            };
        }

        // add access control if enabled
        if (request.HasAccessControl)
        {
            form.AccessControl = new AccessControl
            {
                Type = request.AccessControlType,
                Roles = request.Roles ?? new List<string>()
            };
        }

        var solidityCode = await engine.GenerateAsync(form);
        
        return Results.Ok(new { success = true, code = solidityCode });
    }
    catch (Exception ex)
    {
        return Results.BadRequest(new { success = false, error = ex.Message });
    }
});

app.Run();


// request models
public record GenerateRequest(
    string Name,
    string Symbol,
    int Decimals,
    long Premint,
    long Supply,
    bool HasMinting,
    bool HasBurning,
    bool IsPausable,
    bool HasTax,
    double TaxFee,
    List<TaxRecipientRequest>? TaxRecipients,
    bool HasAccessControl,
    AccessControlType AccessControlType,
    List<string>? Roles
);

public record TaxRecipientRequest(string Address, double Share);