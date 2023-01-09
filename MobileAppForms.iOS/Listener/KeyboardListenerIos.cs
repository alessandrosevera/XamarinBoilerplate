using System;
using UIKit;
using MobileAppForms.Model;
using MobileAppForms.Service.Core;
using Xamarin.Forms;

namespace MobileAppForms.iOS
{
    public class KeyboardListenerIos : KeyboardListener
    {
        #region auto-properties

        private Action<KeyboardEventArgs> ActOnKeyboardWillShow { get; set; }
        private Action<KeyboardEventArgs> ActOnKeyboardWillHide { get; set; }

        #endregion

        #region ctor(s)

        public KeyboardListenerIos()
        {
            _ = UIKeyboard.Notifications.ObserveWillShow((sender, args) => HandleObserveWillShow(args));
            _ = UIKeyboard.Notifications.ObserveWillHide((sender, args) => HandleObserveWillHide(args));
        }

        #endregion

        #region KeyboardListener implementation

        public event EventHandler<KeyboardEventArgs> KeyboardWillShow;
        public event EventHandler<KeyboardEventArgs> KeyboardWillHide;

        public void Configure(Action<KeyboardEventArgs> actOnKeyboardWillShow, Action<KeyboardEventArgs> actOnKeyboardWillHide)
        {
            ActOnKeyboardWillShow = actOnKeyboardWillShow;
            ActOnKeyboardWillHide = actOnKeyboardWillHide;
        }

        #endregion

        #region event handler

        private void HandleObserveWillShow(UIKeyboardEventArgs args)
        {
            var frameBegin = args.FrameBegin;
            var frameEnd = args.FrameEnd;

            var rFrameBegin = new Rectangle(frameBegin.X, frameBegin.Y, frameBegin.Width, frameBegin.Height);
            var rFrameEnd = new Rectangle(frameEnd.X, frameEnd.Y, frameEnd.Width, frameEnd.Height);

            var keyboardEventArgs = new KeyboardEventArgs { KeyboardFrame = rFrameEnd, KeyboardAnimationDurationMs = (uint)(args.AnimationDuration * 1000.0) };
            if (!(ActOnKeyboardWillShow is null)) ActOnKeyboardWillShow(keyboardEventArgs);
            else if (!(KeyboardWillShow is null)) KeyboardWillShow.Invoke(this, keyboardEventArgs);

        }

        private void HandleObserveWillHide(UIKeyboardEventArgs args)
        {
            var frameBegin = args.FrameBegin;
            var frameEnd = args.FrameEnd;

            var rFrameBegin = new Rectangle(frameBegin.X, frameBegin.Y, frameBegin.Width, frameBegin.Height);
            var rFrameEnd = new Rectangle(frameEnd.X, frameEnd.Y, frameEnd.Width, frameEnd.Height);

            var keyboardEventArgs = new KeyboardEventArgs { KeyboardFrame = rFrameEnd, KeyboardAnimationDurationMs = (uint)(args.AnimationDuration * 1000.0) };
            if (!(ActOnKeyboardWillHide is null)) ActOnKeyboardWillHide(keyboardEventArgs);
            else if (!(KeyboardWillHide is null)) KeyboardWillHide.Invoke(this, keyboardEventArgs);
        }

        #endregion
    }
}
