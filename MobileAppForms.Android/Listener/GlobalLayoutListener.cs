using System;
using Android.App;
using Android.Content;
using Android.Views;
using Android.Views.InputMethods;
using MobileAppForms.Model;
using Xamarin.Forms;

namespace MobileAppForms.Listener
{
    public class GlobalLayoutListener : Java.Lang.Object, ViewTreeObserver.IOnGlobalLayoutListener
    {

        #region auto-properties

        private static InputMethodManager InputManager { get; set; }

        private Activity Activity { get; }
        private KeyboardListenerAndroid KeyboardListener { get; }
        private Android.Views.View RootView { get; set; }
        private Rectangle? ClosedKeyboardFrame { get; set; }

        private bool IsKeyboardShown { get; set; }

        #endregion


        #region ctor(s)

        public GlobalLayoutListener(Activity activity, KeyboardListenerAndroid keyboardListener)
        {
            Activity = activity;
            KeyboardListener = keyboardListener;
            InputManager = (InputMethodManager)activity.GetSystemService(Context.InputMethodService);
            // if (!(InputManager is null) && !InputManager.IsAcceptingText) ClosedKeyboardFrame = GetKeyboardFrame();
        }

        #endregion

        #region IOnGlobalLayoutListener implementation

        public void OnGlobalLayout()
        {
            if (InputManager is null || (!(InputManager is null && InputManager.Handle == IntPtr.Zero)))
            {
                InputManager = (InputMethodManager)Activity.GetSystemService(Context.InputMethodService);
            }
            if (InputManager is null) return;

            if (InputManager.IsAcceptingText)
            {
                var keyboardFrame = GetKeyboardFrame();
                if (keyboardFrame.HasValue)
                {
                    if (keyboardFrame.Value.Height > 0 && !IsKeyboardShown && !IsClosedKeyboard(keyboardFrame.Value))
                    {
                        IsKeyboardShown = true;

                        var keyboardEventArgs = new KeyboardEventArgs { KeyboardFrame = keyboardFrame.Value, KeyboardAnimationDurationMs = 250 };
                        KeyboardListener.RaiseObserveWillShow(keyboardEventArgs);
                    }
                    else if (IsClosedKeyboard(keyboardFrame.Value))
                    {
                        RaiseWillHide();
                    }
                }
            }
            else
            {
                if (!ClosedKeyboardFrame.HasValue) ClosedKeyboardFrame = GetKeyboardFrame();
                RaiseWillHide();
            }
        }

        #endregion

        #region helper methods

        private Rectangle? GetKeyboardFrame()
        {
            Rectangle? keyboardFrame = null;
            Android.Graphics.Rect r = new Android.Graphics.Rect();
            if (RootView is null)
            {
                RootView = Activity.FindViewById(Android.Resource.Id.Content);
            }
            if (!(RootView is null))
            {
                RootView.GetWindowVisibleDisplayFrame(r);
                keyboardFrame = new Rectangle(RootView.GetX(), r.Bottom, RootView.Width, RootView.Height - r.Bottom + r.Top);
            }
            return keyboardFrame;
        }

        private void RaiseWillHide()
        {
            if (IsKeyboardShown)
            {
                IsKeyboardShown = false;
                var eventArgs = GetKeyboardFrame();
                if (!(eventArgs is null))
                {
                    var keyboardFrame = new Rectangle(0, 0, 0, 0);
                    var keyboardEventArgs = new KeyboardEventArgs { KeyboardFrame = keyboardFrame, KeyboardAnimationDurationMs = 250 };
                    KeyboardListener.RaiseObserveWillHide(keyboardEventArgs);
                }
            }
        }

        private bool IsClosedKeyboard(Rectangle rect)
        {
            if (ClosedKeyboardFrame.HasValue)
            {
                return ClosedKeyboardFrame.Value.Height == rect.Height
                    && ClosedKeyboardFrame.Value.Y == rect.Y;
            }
            return false;
        }

        #endregion



    }
}