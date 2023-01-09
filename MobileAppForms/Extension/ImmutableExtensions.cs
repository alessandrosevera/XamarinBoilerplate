using System;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace MobileAppForms.Extension
{
    public static class ImmutableExtensions
    {
        #region extension methods

        public static ImmutableArray<T> ToSafeImmutableArray<T>(this IEnumerable<T> enumerable)
        {
            return enumerable is null ? new ImmutableArray<T>() : enumerable.ToImmutableArray();
        }

        #endregion
    }
}
