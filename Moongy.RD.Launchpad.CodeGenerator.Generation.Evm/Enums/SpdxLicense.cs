using Moongy.RD.Launchpad.CodeGenerator.Core.Attributes;

namespace Moongy.RD.Launchpad.CodeGenerator.Generation.Evm.Enums;

public enum SpdxLicense
{
    [EnumLabel(Display = "MIT License",
        Description = "A permissive license that is short and to the point, allowing reuse with minimal obligations.",
        Value = "MIT")]
    MIT,

    [EnumLabel(
        Display = "Apache License 2.0",
        Description = "A permissive license with explicit grant of patent rights from contributors.",
        Value = "Apache-2.0")]
    Apache_2_0,

    [EnumLabel(
        Display = "GNU GPL v3.0 only",
        Description = "A strong copyleft license requiring derivatives to be licensed under the same terms.",
        Value = "GPL-3.0-only")]
    GPL_3_0_only,

    [EnumLabel(
        Display = "GNU LGPL v3.0 only",
        Description = "A “library” copyleft license allowing linking from non-free code.",
        Value = "LGPL-3.0-only")]
    LGPL_3_0_only,

    [EnumLabel(
        Display = "BSD 3-Clause “New” or “Revised” License",
        Description = "A permissive license with a short disclaimer and three clauses.",
        Value = "BSD-3-Clause")]
    BSD_3_Clause,

    [EnumLabel(
        Display = "Mozilla Public License 2.0",
        Description = "A weak copyleft license that requires modifications to be open but allows larger works to remain closed.",
        Value = "MPL-2.0")]
    MPL_2_0,

    [EnumLabel(
        Display = "GNU AGPL v3.0 or later",
        Description = "A copyleft license that, in addition to GPL requirements, mandates network-use disclosure.",
        Value = "AGPL-3.0-or-later")]
    AGPL_3_0_or_later,

    [EnumLabel(
        Display = "Eclipse Public License 2.0",
        Description = "A business-friendly weak copyleft license used by many open-source projects.",
        Value = "EPL-2.0")]
    EPL_2_0,

    [EnumLabel(
        Display = "Common Development and Distribution License 1.0",
        Description = "A weak copyleft license similar to MPL, used by Oracle for some projects.",
        Value = "CDDL-1.0")]
    CDDL_1_0,

    [EnumLabel(
        Display = "ISC License",
        Description = "A simple permissive license functionally equivalent to MIT/Expat.",
        Value = "ISC")]
    ISC
}
