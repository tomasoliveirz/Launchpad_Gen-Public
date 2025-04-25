namespace Moongy.RD.Launchpad.Core.Enums
{
    public enum PropertyType
    {
        None,      
        Event,
        Error,
        Struct,
        Enum,
        Library,

        Flag,       // for bools 
        Option,     // for enums or nullable configs
        Numeric,    // for things like PremintAmount, Circulation
        String,     // for text values like Symbol
    }

}
