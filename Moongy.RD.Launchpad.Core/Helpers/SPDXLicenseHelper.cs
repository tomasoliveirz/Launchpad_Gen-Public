using System.Reflection;
using Moongy.RD.Launchpad.Core.Attributes;
using Moongy.RD.Launchpad.Core.Enums;

namespace Moongy.RD.Launchpad.Core.Helpers;

public static class SPDXLicenseHelper
{


    public static string? GetValue(SpdxLicense license)
    {
        var member = typeof(SpdxLicense)
            .GetMember(license.ToString())
            .FirstOrDefault();
        var attr = member?.GetCustomAttribute<EnumLabelAttribute>();
        return attr == null ? throw new ArgumentException($"No EnumLabelAttribute found for {license}", nameof(license)) : attr.Value;
    }


    public static IReadOnlyList<string> GetAllDisplays()
    {
        return typeof(SpdxLicense)
            .GetFields(BindingFlags.Public | BindingFlags.Static)
            .Select(f => f.GetCustomAttribute<EnumLabelAttribute>()?.Display)
            .Where(d => d != null)
            .ToList()!;
    }


    public static SpdxLicense FromDisplay(string display)
    {
        foreach (var field in typeof(SpdxLicense)
                     .GetFields(BindingFlags.Public | BindingFlags.Static))
        {
            var attr = field.GetCustomAttribute<EnumLabelAttribute>();
            if (attr != null && attr.Display != null && attr.Display.Equals(display, StringComparison.Ordinal))
            {
                return (SpdxLicense)field.GetValue(null)!;
            }
        }
        throw new ArgumentException($"No SpdxLicense with display '{display}'", nameof(display));
    }
}