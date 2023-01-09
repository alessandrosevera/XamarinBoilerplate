using System;
using Android.App;
using MobileAppForms.Model;
using MobileAppForms.Service.Core;

namespace MobileAppForms.Listener
{
    public class KeyboardListenerAndroid : KeyboardListener
    {
        #region auto-properties

        private Action<KeyboardEventArgs> ActOnKeyboardWillShow { get; set; }
        private Action<KeyboardEventArgs> ActOnKeyboardWillHide { get; set; }

        #endregion

        #region events

        private event EventHandler<KeyboardEventArgs> _keyboardWillShow;
        private event EventHandler<KeyboardEventArgs> _keyboardWillHide;

        #endregion

        #region auto-properties

        private GlobalLayoutListener GlobalLayoutListener { get; set; }
        private Activity Activity { get; }

        #endregion

        #region ctor(s)

        public KeyboardListenerAndroid(Activity activity)
        {
            Activity = activity;
        }

        #endregion

        #region KeyboardListener implementation


        public event EventHandler<KeyboardEventArgs> KeyboardWillShow
        {
            add
            {
                _keyboardWillShow += value;
                CheckListener();
            }
            remove { _keyboardWillShow -= value; }
        }

        public event EventHandler<KeyboardEventArgs> KeyboardWillHide
        {
            add
            {
                _keyboardWillHide += value;
                CheckListener();
            }
            remove { _keyboardWillHide -= value; }
        }

        public void Configure(Action<KeyboardEventArgs> actOnKeyboardWillShow, Action<KeyboardEventArgs> actOnKeyboardWillHide)
        {
            ActOnKeyboardWillShow = actOnKeyboardWillShow;
            ActOnKeyboardWillHide = actOnKeyboardWillHide;
            CheckListener();
        }

        #endregion


        #region event handler

        public void RaiseObserveWillShow(KeyboardEventArgs args)
        {
            if (!(ActOnKeyboardWillShow is null)) ActOnKeyboardWillShow(args);
            else if (!(_keyboardWillShow is null)) _keyboardWillShow(this, args);
        }

        public void RaiseObserveWillHide(KeyboardEventArgs args)
        {
            if (!(ActOnKeyboardWillHide is null)) ActOnKeyboardWillHide(args);
            else if (!(_keyboardWillHide is null)) _keyboardWillHide(this, args);
        }

        #endregion

        #region helper methods

        private void CheckListener()
        {
            if (GlobalLayoutListener is null)
            {
                GlobalLayoutListener = new GlobalLayoutListener(Activity, this);

                var rootView = Activity.FindViewById(Android.Resource.Id.Content);
                var boh = Activity.Window.DecorView;

                rootView.ViewTreeObserver.AddOnGlobalLayoutListener(GlobalLayoutListener);
            }
        }

        #endregion
    }
}
