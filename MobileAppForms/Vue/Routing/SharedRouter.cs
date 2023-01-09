using System;
using System.Threading.Tasks;
using VueSharp;
using VueSharp.Provider.Core;
using VueSharp.Routing;
using VueSharp.Service.Core;
using Xamarin.Essentials;

namespace MobileAppForms.Vue
{
    public class SharedRouter : AbstractRouter
    {
        #region abstract properties implementation

        public override VueSharp.Abstraction.RootComponent RootComponent => ComponentFactory.CreateComponent<SharedRootComponent>();
        public override bool IsSafeAreaInsetsApplyiable => ((DeviceInfo.Platform == DevicePlatform.Android && DeviceInfo.Version.Major >= 11) || DeviceInfo.Platform == DevicePlatform.iOS);

        #endregion

        #region ctor(s)

        public SharedRouter(ComponentFactory componentFactory, CatalogProvider catalogProvider, SafeAreaInsetsService safeAreaInsetsService) : base(componentFactory, catalogProvider, safeAreaInsetsService)
        {
        }

        #endregion

        #region abstract methods implementation

        protected override bool CanNavigateBack(Component component)
        {
            return true;
        }

        public override async Task UnpresentRootComponent()
        {
            await base.UnpresentRootComponent();

            var searchComponent = ComponentFactory.CreateComponent<SearchComponent>();
            var favoritesComponent = ComponentFactory.CreateComponent<FavoritesComponent>();
            var agencyComponent = ComponentFactory.CreateComponent<AgencyComponent>();
            var profileComponent = ComponentFactory.CreateComponent<ProfileComponent>();

            bool? didSearchUnpresented = searchComponent?.Unpresent();
            if (didSearchUnpresented.HasValue && !didSearchUnpresented.Value)
            {
                searchComponent?.Dispose();
            }
            bool? didFavoritesUnpresented = favoritesComponent?.Unpresent();
            if (didFavoritesUnpresented.HasValue && !didFavoritesUnpresented.Value)
            {
                favoritesComponent?.Dispose();
            }
            bool? didAgencyUnpresented = agencyComponent?.Unpresent();
            if (didAgencyUnpresented.HasValue && !didAgencyUnpresented.Value)
            {
                agencyComponent?.Dispose();
            }
            bool? didProfileUnpresented = profileComponent?.Unpresent();
            if (didProfileUnpresented.HasValue && !didProfileUnpresented.Value)
            {
                profileComponent?.Dispose();
            }
        }

        #endregion
    }
}
