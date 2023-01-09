using System;
using Xamarin.Forms;

namespace MobileAppForms.Model
{
    public class KeyboardEventArgs : EventArgs
    {
        #region auto-properties

        public Rectangle KeyboardFrame { get; set; }
        public uint KeyboardAnimationDurationMs { get; set; }

        #endregion
    }
}
