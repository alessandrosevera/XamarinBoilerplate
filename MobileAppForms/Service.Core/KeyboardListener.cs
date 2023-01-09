using System;
using MobileAppForms.Model;

namespace MobileAppForms.Service.Core
{
    public interface KeyboardListener
    {
        // https://stackoverflow.com/questions/18435112/how-to-get-android-soft-keyboard-height
        // https://stackoverflow.com/questions/46360257/xamarin-forms-check-if-keyboard-is-open-or-not

        event EventHandler<KeyboardEventArgs> KeyboardWillShow;
        event EventHandler<KeyboardEventArgs> KeyboardWillHide;

        void Configure(Action<KeyboardEventArgs> actOnKeyboardWillShow, Action<KeyboardEventArgs> actOnKeyboardWillHide);
    }
}

