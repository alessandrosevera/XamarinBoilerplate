using System;
using MobileAppForms.iOS;
using MobileAppForms.Model;
using MobileAppForms.Model.Core;
using MobileAppForms.Service;
using MobileAppForms.Store;
using MobileAppForms.Ux;
using MobileAppForms.Vue;
using UIKit;
using VueSharp;
using VueSharp.Abstraction;
using VuexSharp.Ioc.Core;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(RootPage), typeof(TabbedRootPageRenderer))]
namespace MobileAppForms.iOS
{
    public class TabbedRootPageRenderer : TabbedRenderer
    {

        public override void ViewWillLayoutSubviews()
        {
            UpdateTabBarItemsAspect();
            base.ViewWillLayoutSubviews();
        }

        public override void ViewWillAppear(bool animated)
        {
            if (TabBar?.Items != null)
            {
                var control = (RootPage)Element;
                if (!(control is null))
                {
                    var tabs = Element as TabbedPage;
                    if (tabs != null)
                    {
                        if (TabBar.Items.Length == control.Children.Count)
                        {
                            this.ShouldSelectViewController = ShouldSelectViewControllerHandler;
                            control.Configure(UpdateTabBarItemsAspect);
                        }
                    }
                }
            }
            base.ViewWillAppear(animated);
        }

        public override void ItemSelected(UITabBar tabbar, UITabBarItem item)
        {
            // var page = ((TabbedPage)Element).CurrentPage;
        }

        private void UpdateTabBarItemsAspect()
        {
            InvokeOnMainThread(() =>
            {
                if (TabBar?.Items != null)
                {
                    var control = (RootPage)Element;
                    if (!(control is null))
                    {
                        var store = AppDelegate.Container.Resolve<InternalStore>();

                        TabBar.SelectedImageTintColor = control.SelectedTabColor.ToUIColor();
                        TabBar.UnselectedItemTintColor = control.UnselectedTabColor.ToUIColor();

                        var tabs = Element as TabbedPage;
                        if (tabs != null)
                        {
                            if (TabBar.Items.Length == control.Children.Count)
                            {
                                for (int i = 0; i < TabBar.Items.Length; i++)
                                {
                                    var childrenItem = control.Children[i] as TabItemPage;
                                    var tabBarItem = TabBar.Items[i];
                                    bool didSucceed = store.AppContextModule.State.TabItems.TryGetValue(childrenItem.AppTab, out TabItemState tabItemState);
                                    if (didSucceed)
                                    {
                                        UpdateItem(tabBarItem, tabItemState);
                                    }
                                }
                            }
                        }
                    }
                }
            });
        }

        private void UpdateItem(UITabBarItem item, TabItemState tabItemState)
        {
            InvokeOnMainThread(() =>
            {
                if (item == null) return;

                if (tabItemState.IsDisabled)
                {
                    item.Image = UIImage.FromBundle(tabItemState.DisabledIcon);
                    item.SetTitleTextAttributes(new UITextAttributes
                    {
                        Font = UIFont.FromName("Montserrat-SemiBold", 12f),
                        TextColor = tabItemState.DisabledColor.ToUIColor()
                    }, UIControlState.Selected);

                    item.SetTitleTextAttributes(new UITextAttributes
                    {
                        Font = UIFont.FromName("Montserrat-SemiBold", 12f),
                        TextColor = tabItemState.DisabledColor.ToUIColor()
                    }, UIControlState.Normal);
                }
                else
                {
                    item.Image = UIImage.FromBundle(tabItemState.Icon);
                    item.SelectedImage = UIImage.FromBundle(tabItemState.SelectedIcon);
                    item.SetTitleTextAttributes(new UITextAttributes
                    {
                        Font = UIFont.FromName("Montserrat-SemiBold", 12f),
                        TextColor = tabItemState.SelectedColor.ToUIColor()
                    }, UIControlState.Selected);

                    item.SetTitleTextAttributes(new UITextAttributes
                    {
                        Font = UIFont.FromName("Montserrat-SemiBold", 12f),
                        TextColor = tabItemState.UnselectedColor.ToUIColor()
                    }, UIControlState.Normal);
                }
            });
        }

        private bool ShouldSelectViewControllerHandler(UITabBarController tabBarController, UIViewController viewController)
        {
            var accessControl = AppDelegate.Container.Resolve<AccessControl>();
            if (accessControl.IsModalPresented)
            {
                var componentFactory = AppDelegate.Container.Resolve<ComponentFactory>();
                var authenticationPopupComponent = componentFactory.CreateComponent<AuthenticationRequiredPopupComponent>();
                if (authenticationPopupComponent != null)
                {
                    authenticationPopupComponent.Unpresent();
                }
            }

            bool canGo = false;
            if (viewController is PageRenderer pageRenderer && pageRenderer.Element is Page page)
            {
                var rootComponent = AppDelegate.Container.Resolve<SharedRootComponent>();
                canGo = rootComponent.CanSelectTabPage(page);
                if (!canGo)
                {
                    _ = rootComponent.TryAndSelectTabPage(page);
                }
            }

            return canGo;
        }
    }
}
