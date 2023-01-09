using System;
using VueSharp;
using Xamarin.Forms;

namespace MobileAppForms.Extension
{
    public static class MatchExtensions
    {
        public static bool MatchPage(this Page pageA, Page pageB)
        {
            Page currentPage = null;
            Page targetPage = null;

            if (pageA is NavigationPage navigationA)
            {
                currentPage = navigationA.RootPage;
            }
            else
            {
                currentPage = pageA;
            }

            if (pageB is NavigationPage navigationB)
            {
                targetPage = navigationB.RootPage;
            }
            else
            {
                targetPage = pageB;
            }

            return (!(currentPage is null) && !(targetPage is null) && currentPage == targetPage);
        }

        public static bool MatchComponentPresenter(this Component component, Page queuedPage)
        {
            Page componentPage = null;
            Page targetPage = null;

            if (component.Presenter is NavigationPage componentNavigation)
            {
                componentPage = componentNavigation.RootPage;
            }
            else
            {
                componentPage = component.Presenter as Page;
            }

            if (queuedPage is NavigationPage queuedNavigation)
            {
                targetPage = queuedNavigation.RootPage;
            }
            else
            {
                targetPage = queuedPage;
            }

            return (!(componentPage is null) && !(targetPage is null) && componentPage == targetPage);
        }
    }
}
