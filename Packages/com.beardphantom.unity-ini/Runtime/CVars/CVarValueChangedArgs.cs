namespace BeardPhantom.CVars
{
    public readonly struct CVarValueChangedArgs
    {
        #region Fields

        public readonly CVar Cvar;

        public readonly CVarValuesState ValuesStateOld;

        public readonly CVarValuesState ValuesStateNew;

        public readonly CVarValueType ChangedType;

        #endregion

        #region Constructors

        public CVarValueChangedArgs(
            CVar cvar,
            in CVarValuesState valuesStateOld,
            in CVarValuesState valuesStateNew,
            in CVarValueType changedType)
        {
            Cvar = cvar;
            ValuesStateOld = valuesStateOld;
            ValuesStateNew = valuesStateNew;
            ChangedType = changedType;
        }

        #endregion
    }
}