using System;
using System.Collections.Generic;
using MobileAppForms.Model;
using VuexSharp;

namespace MobileAppForms.Store
{
    public readonly struct AppContextState : State
    {
        #region auto-properties

        public Configuration? Configuration { get; }
        public string CustomTwoLetterIsoLanguageUI { get; }
        public Dictionary<AppTabs, TabItemState> TabItems { get; }

        #endregion

        #region ctor(s)

        public AppContextState(Configuration? configuration, string customTwoLetterIsoLanguageUI,
            Dictionary<AppTabs, TabItemState> tabItems)
        {
            Configuration = configuration;
            CustomTwoLetterIsoLanguageUI = customTwoLetterIsoLanguageUI;
            TabItems = tabItems;
        }

        #endregion


        #region immutable methods

        public AppContextState WithConfiguration(Configuration configuration)
        {
            return new AppContextState(configuration, CustomTwoLetterIsoLanguageUI, TabItems);
        }

        public AppContextState WithCustomTwoLetterIsoLanguageUI(string customTwoLetterIsoLanguageUI)
        {
            return new AppContextState(Configuration, customTwoLetterIsoLanguageUI, TabItems);
        }

        public AppContextState WithTabItems(Dictionary<AppTabs, TabItemState> tabItems)
        {
            return new AppContextState(Configuration, CustomTwoLetterIsoLanguageUI, tabItems);
        }

        #endregion
    }
}
