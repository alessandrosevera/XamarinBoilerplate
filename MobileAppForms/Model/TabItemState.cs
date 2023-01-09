using System;
using PropertyChanged;
using Xamarin.Forms;

namespace MobileAppForms.Model
{
    [AddINotifyPropertyChangedInterface]
    public class TabItemState
    {
        #region auto-properties

        public string Icon { get; set; }
        public string SelectedIcon { get; set; }
        public string DisabledIcon { get; set; }

        public Color UnselectedColor { get; set; }
        public Color SelectedColor { get; set; }

        public bool IsDisabled { get; set; }
        public Color DisabledColor { get; set; }

        #endregion
    }
}

