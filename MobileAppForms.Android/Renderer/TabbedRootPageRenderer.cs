using System;
using Android.Content;
using Android.Graphics;
using Android.Views;
using Android.Widget;
using Google.Android.Material.BottomNavigation;
using Google.Android.Material.Internal;
using Google.Android.Material.Tabs;
using MobileAppForms.Droid;
using MobileAppForms.Listener;
using MobileAppForms.Model;
using MobileAppForms.Model.Core;
using MobileAppForms.Store;
using MobileAppForms.Ux;
using VuexSharp;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms.Platform.Android.AppCompat;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;

[assembly: ExportRenderer(typeof(RootPage), typeof(TabbedRootPageRenderer))]
namespace MobileAppForms.Droid
{
    public class TabbedRootPageRenderer : TabbedPageRenderer
    {
        #region fields

        BottomNavigationView _bottomNavigationView;
        IMenuItem _lastItemSelected;
        RootPage _control;

        #endregion

        #region ctor(s)

        public TabbedRootPageRenderer(Context context) : base(context)
        {
        }

        #endregion

        #region overrides

        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.TabbedPage> e)
        {
            base.OnElementChanged(e);


            if (e.NewElement != null)
            {
                _control = e.NewElement as RootPage;
                if (!(_control is null))
                {
                    _bottomNavigationView = (GetChildAt(0) as Android.Widget.RelativeLayout).GetChildAt(1) as BottomNavigationView;
                    _control.Configure(UpdateTabBarItemsAspect);

                    var navigationItemSelectedListener = new NavigationItemSelectedListener();
                    navigationItemSelectedListener.Configure(_control);

                    var navigationItemReselectedListener = new NavigationItemReselectedListener();
                    navigationItemReselectedListener.Configure(_control);

                    _bottomNavigationView.SetOnItemSelectedListener(navigationItemSelectedListener);
                    _bottomNavigationView.SetOnItemReselectedListener(navigationItemReselectedListener);

                    UpdateTabBarItemsAspect();
                }
            }
        }

        #endregion

        #region helper methods

        private void UpdateTabBarItemsAspect()
        {
            MainThread.InvokeOnMainThreadAsync(() =>
            {
                if (!(_control is null))
                {
                    var store = MainActivity.Container.Resolve<InternalStore>();
                    for (int idx = 0; idx < _control.Children.Count; idx++)
                    {
                        var childrenItem = _control.Children[idx] as TabItemPage;
                        var tabBarItem = _bottomNavigationView.Menu.GetItem(idx);
                        bool didSucceed = store.AppContextModule.State.TabItems.TryGetValue(childrenItem.AppTab, out TabItemState tabItemState);
                        if (didSucceed)
                        {
                            string currentIcon = null;
                            try
                            {
                                if (_control.Children[idx].IconImageSource is FileImageSource fileImageSource)
                                {
                                    currentIcon = fileImageSource.File;
                                }
                                else
                                {
                                    // @AS Remove this when Icon is totally removed, in only a fallback
#pragma warning disable CS0618 // Type or member is obsolete
                                    currentIcon = _control.Children[idx].Icon;
#pragma warning restore CS0618 // Type or member is obsolete
                                }
                            }
                            catch(Exception ex)
                            {
                                System.Diagnostics.Debug.WriteLine(ex);
                            }

                            UpdateItem(idx, tabBarItem, tabItemState, currentIcon, null);
                        }
                    }
                }
            });
        }

        private void UpdateItem(int idx, IMenuItem item, TabItemState tabItemState, string icon, string badgeValue)
        {
            // item.SetIcon(Context.GetDrawable(icon + ".png"));
            if (item.IsChecked)
            {
                _lastItemSelected = item;
            }

            var bottomNavMenuView = _bottomNavigationView.GetChildAt(0) as BottomNavigationMenuView;
            var itemMenuView = bottomNavMenuView.GetChildAt(idx) as BottomNavigationItemView;
            var itemTitle = itemMenuView.GetChildAt(1);
            var smallTextView = ((TextView)((BaselineLayout)itemTitle).GetChildAt(0));
            var largeTextView = ((TextView)((BaselineLayout)itemTitle).GetChildAt(1));
            var fontFace = Typeface.CreateFromAsset(Context.Assets, "Montserrat-SemiBold.ttf");

            if (!(smallTextView is null))
            {
                smallTextView.SetTypeface(fontFace, TypefaceStyle.Normal);
                smallTextView.SetTextSize(Android.Util.ComplexUnitType.Dip, 11);
                smallTextView.SetTextColor((tabItemState.IsDisabled) ? tabItemState.DisabledColor.ToAndroid() : tabItemState.SelectedColor.ToAndroid());
            }
            if (!(largeTextView is null))
            {
                largeTextView.SetTypeface(fontFace, TypefaceStyle.Normal);
                largeTextView.SetTextSize(Android.Util.ComplexUnitType.Dip, 11);
                largeTextView.SetTextColor((tabItemState.IsDisabled) ? tabItemState.DisabledColor.ToAndroid() : tabItemState.SelectedColor.ToAndroid());
            }

            var applyiableIcon = tabItemState.IsDisabled ? tabItemState.DisabledIcon : tabItemState.SelectedIcon;
            if (applyiableIcon is null && !(icon is null)) applyiableIcon = icon;
            
            item.SetIcon(Context.GetDrawable(applyiableIcon + ".png"));

            // item.SetEnabled(!tabItem.IsDisabled);
        }

        #endregion



        /*
        //Remove tint color
        protected override void OnLayout(bool changed, int l, int t, int r, int b)
        {
            base.OnLayout(changed, l, t, r, b);

            if (bottomNavigationView != null)
            {
                bottomNavigationView.ItemIconTintList = null;
            }
        }
        */
    }
}

