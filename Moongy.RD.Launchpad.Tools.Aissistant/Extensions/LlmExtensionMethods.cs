using System.Reflection;
using Moongy.RD.Launchpad.Tools.Aissistant.Attributes;
using Moongy.RD.Launchpad.Tools.Aissistant.Enums;

namespace Moongy.RD.Launchpad.Tools.Aissistant.Extensions;

public static class LlmSchemaExtensions
{
    /// <summary>
    /// gets the schema value from an enum decorated with LlmSchemaAttribute.
    /// </summary>
    public static string? Parse<TEnum>(this TEnum value, LlmService? service = null)
        where TEnum : struct, Enum
    {
        var member = typeof(TEnum).GetMember(value.ToString()).FirstOrDefault();
        if (member == null)
            return null;

        var attributes = member.GetCustomAttributes<LlmSchemaAttribute>().ToList();
        if (!attributes.Any())
            return null;

        var selected = service == null
            ? attributes.FirstOrDefault()
            : attributes.FirstOrDefault(x => x.Service == service);

        return selected?.Value;
    }

    /// <summary>
    /// Checks if the enum value is valid for the given service.
    /// </summary>
    public static bool IsValidForService<TEnum>(this TEnum value, LlmService service)
        where TEnum : struct, Enum
    {
        var member = typeof(TEnum).GetMember(value.ToString()).FirstOrDefault();
        if (member == null)
            return false;

        return member.GetCustomAttributes<LlmSchemaAttribute>().Any(x => x.Service == service);
    }

    /// <summary>
    /// fetches the model's assigned service
    /// </summary>
    public static LlmService? GetService(this LlmModel model)
    {
        var member = typeof(LlmModel)
           .GetMember(model.ToString(), BindingFlags.Public | BindingFlags.Static)
           .FirstOrDefault();

        if (member == null)
            throw new ArgumentException($"No member info for LlmModel.{model}", nameof(model));

        // grab the first schema attribute
        var schema = member
            .GetCustomAttributes<LlmSchemaAttribute>(inherit: false)
            .FirstOrDefault();


        return schema?.Service??LlmService.None;
    }

    /// <summary>
    /// Fetches the best models to execute an operation. It's currently ordering by the position where that type stands
    /// </summary>

    public static IEnumerable<LlmModel> GetBestModelsFor(
        this OperationType operation,
        LlmService? service = null)
    {
        return Enum.GetValues<LlmModel>()
            .Cast<LlmModel>()
            .Where(m => m != LlmModel.None)
            .Select(m =>
            {
                var member = typeof(LlmModel)
                    .GetMember(m.ToString())
                    .FirstOrDefault();
                var attrs = member?
                    .GetCustomAttributes<LlmSchemaAttribute>()
                    .Where(a => !service.HasValue || a.Service == service.Value)
                    .ToArray()
                    ?? Array.Empty<LlmSchemaAttribute>();

                var attr = attrs.FirstOrDefault();
                var bestFor = attr?.BestFor ?? Array.Empty<OperationType>();
                return (Model: m, BestFor: bestFor);
            })
            .Where(x => x.BestFor.Contains(operation))
            .OrderBy(x => Array.IndexOf(x.BestFor, operation))
            .Select(x => x.Model);
    }
}

public static class StringExtensionMethods
{
    public static double? ToDouble(this string? str)
    {
        if (str == null) return null;
        var success = double.TryParse(str, out var value);
        return success ? value : null;
    }
}
