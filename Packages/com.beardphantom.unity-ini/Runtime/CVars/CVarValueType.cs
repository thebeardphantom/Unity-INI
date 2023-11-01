namespace BeardPhantom.CVars
{
    /// <summary>
    /// Because cvars represent multiple values at once (strings, ints, floats, and bools), its sometimes useful to indicate cvar set 
    /// </summary>
    public enum CVarValueType
    {
        String = 0,
        Int = 1,
        Float = 2,
        Bool = 3
    }
}