using System;
using System.Collections.Generic;

namespace MobileAppForms.Model
{
    public readonly struct Denomination
    {
        #region auto-properties

        public Dictionary<string, string> LocalizedDenominations { get; }

        #endregion

        #region ctor(s)

        public Denomination(Dictionary<string, string> localizedDenominations)
        {
            LocalizedDenominations = localizedDenominations;
        }

        #endregion
    }
}
