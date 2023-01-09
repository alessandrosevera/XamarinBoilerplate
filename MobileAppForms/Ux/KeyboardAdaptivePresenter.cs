using SimpleTouchView;
using VueSharp.Abstraction;
using VueSharp.Model;
using Xamarin.Essentials;
using Xamarin.Forms;
namespace MobileAppForms.Ux
{
    // TODO IMPROVE THIS - DUPLICATION

    public abstract class KeyboardAdaptiveLayout : AbsoluteLayout
    {
        #region abstract properties

        protected abstract VisualElement MovableElement { get; }

        #endregion

        #region auto-properties

        protected SafeAreaInsets SafeAreaInsets { get; set; }

        #endregion

        #region properties

        protected bool IsMakeRoomForKeyboardReady = false;
        protected double CurrentKeyboardHeight = -1;
        protected uint CurrentKeyboardAnimationDurationMs = 0;

        #endregion

        #region access methods

        public void MakeRoomForKeyboard(double keyboardHeight, uint keyboardAnimationDurationMs)
        {
            IsMakeRoomForKeyboardReady = true;
            CurrentKeyboardHeight = keyboardHeight;
            if (DeviceInfo.Platform == DevicePlatform.Android)
            {
                CurrentKeyboardHeight = keyboardHeight / Xamarin.Essentials.DeviceDisplay.MainDisplayInfo.Density;
                if (DeviceInfo.Version.Major >= 11) CurrentKeyboardHeight -= SafeAreaInsets.Top;
            }
            if (keyboardAnimationDurationMs > 0)
            {
                CurrentKeyboardAnimationDurationMs = keyboardAnimationDurationMs;
            }
        }

        public void UnmakeRoomForKeyboard(uint keyboardAnimationDurationMs)
        {
            IsMakeRoomForKeyboardReady = false;
            CurrentKeyboardHeight = -1;

            MovableElement?.TranslateTo(0, 0, keyboardAnimationDurationMs);
        }

        #endregion

        #region protected methods

        protected void Configure(SafeAreaInsets safeAreaInsets)
        {
            SafeAreaInsets = safeAreaInsets;
        }

        protected virtual void TryAndActuateMakeRoomForKeyboard(VisualElement element)
        {
            if (!IsMakeRoomForKeyboardReady || CurrentKeyboardHeight <= 0) return;

            var screenHeight = Xamarin.Essentials.DeviceDisplay.MainDisplayInfo.Height / Xamarin.Essentials.DeviceDisplay.MainDisplayInfo.Density;

            var absElY = GetAbsoluteY(element);
            if (DeviceInfo.Platform == DevicePlatform.iOS) absElY -= SafeAreaInsets.Top;
            absElY += element.Height;

            var kRelY = screenHeight - CurrentKeyboardHeight;
            if (DeviceInfo.Platform == DevicePlatform.Android && DeviceInfo.Version.Major < 11)
            {
                kRelY -= SafeAreaInsets.Bottom;
                kRelY -= SafeAreaInsets.Top;
            }
            else if (DeviceInfo.Platform == DevicePlatform.iOS)
            {
                kRelY -= SafeAreaInsets.Top;
            }

            if (absElY > kRelY)
            {
                var targetY = (absElY - kRelY) + 20;
                MovableElement.TranslateTo(0, -targetY, CurrentKeyboardAnimationDurationMs);
            }
        }

        #endregion

        #region helper methods

        protected double GetAbsoluteY(VisualElement element)
        {
            double y = element.Y;
            if (!(element.Parent is null) && element.Parent is VisualElement visualElement) y += GetAbsoluteY(visualElement);
            return y;
        }

        #endregion
    }

    public abstract class KeyboardAdaptivePushablePage : PushablePresenter
    {
        #region abstract properties

        protected abstract VisualElement MovableElement { get; }

        #endregion

        #region auto-properties

        protected SafeAreaInsets SafeAreaInsets { get; set; }

        #endregion

        #region properties

        protected bool IsMakeRoomForKeyboardReady = false;
        protected double CurrentKeyboardHeight = -1;
        protected uint CurrentKeyboardAnimationDurationMs = 0;

        #endregion

        #region access methods

        public void MakeRoomForKeyboard(double keyboardHeight, uint keyboardAnimationDurationMs)
        {
            IsMakeRoomForKeyboardReady = true;
            CurrentKeyboardHeight = keyboardHeight;
            if (DeviceInfo.Platform == DevicePlatform.Android)
            {
                CurrentKeyboardHeight = keyboardHeight / Xamarin.Essentials.DeviceDisplay.MainDisplayInfo.Density;
                if (DeviceInfo.Version.Major >= 11) CurrentKeyboardHeight -= SafeAreaInsets.Top;
            }
            if (keyboardAnimationDurationMs > 0)
            {
                CurrentKeyboardAnimationDurationMs = keyboardAnimationDurationMs;
            }
        }

        public void UnmakeRoomForKeyboard(uint keyboardAnimationDurationMs)
        {
            IsMakeRoomForKeyboardReady = false;
            CurrentKeyboardHeight = -1;

            MovableElement?.TranslateTo(0, 0, keyboardAnimationDurationMs);
        }

        #endregion

        #region protected methods

        protected void Configure(SafeAreaInsets safeAreaInsets)
        {
            SafeAreaInsets = safeAreaInsets;
        }

        protected virtual void TryAndActuateMakeRoomForKeyboard(VisualElement element)
        {
            if (!IsMakeRoomForKeyboardReady || CurrentKeyboardHeight <= 0) return;

            var screenHeight = Xamarin.Essentials.DeviceDisplay.MainDisplayInfo.Height / Xamarin.Essentials.DeviceDisplay.MainDisplayInfo.Density;

            var absElY = GetAbsoluteY(element);
            if (DeviceInfo.Platform == DevicePlatform.iOS) absElY -= SafeAreaInsets.Top;
            absElY += element.Height;

            var kRelY = screenHeight - CurrentKeyboardHeight;
            if (DeviceInfo.Platform == DevicePlatform.Android && DeviceInfo.Version.Major < 11)
            {
                kRelY -= SafeAreaInsets.Bottom;
                kRelY -= SafeAreaInsets.Top;
            }
            else if (DeviceInfo.Platform == DevicePlatform.iOS)
            {
                kRelY -= SafeAreaInsets.Top;
            }

            if (absElY > kRelY)
            {
                var targetY = (absElY - kRelY) + 20;
                MovableElement.TranslateTo(0, -targetY, CurrentKeyboardAnimationDurationMs);
            }
        }

        #endregion

        #region helper methods

        protected double GetAbsoluteY(VisualElement element)
        {
            double y = element.Y;
            if (!(element.Parent is null) && element.Parent is VisualElement visualElement) y += GetAbsoluteY(visualElement);
            return y;
        }

        #endregion
    }

    public abstract class KeyboardAdaptiveGestureLayout : PanelGestureAbsoluteLayout
    {
        #region abstract properties

        protected abstract VisualElement MovableElement { get; }

        #endregion

        #region auto-properties

        protected SafeAreaInsets SafeAreaInsets { get; set; }

        #endregion

        #region properties

        protected bool IsMakeRoomForKeyboardReady = false;
        protected double CurrentKeyboardHeight = -1;
        protected uint CurrentKeyboardAnimationDurationMs = 0;

        #endregion

        #region access methods

        public void MakeRoomForKeyboard(double keyboardHeight, uint keyboardAnimationDurationMs)
        {
            IsMakeRoomForKeyboardReady = true;
            CurrentKeyboardHeight = keyboardHeight;
            if (DeviceInfo.Platform == DevicePlatform.Android)
            {
                CurrentKeyboardHeight = keyboardHeight / Xamarin.Essentials.DeviceDisplay.MainDisplayInfo.Density;
                if (DeviceInfo.Version.Major >= 11) CurrentKeyboardHeight -= SafeAreaInsets.Top;
            }
            CurrentKeyboardAnimationDurationMs = keyboardAnimationDurationMs;
        }

        public void UnmakeRoomForKeyboard(uint keyboardAnimationDurationMs)
        {
            IsMakeRoomForKeyboardReady = false;
            CurrentKeyboardHeight = -1;

            MovableElement?.TranslateTo(0, 0, keyboardAnimationDurationMs);
        }

        #endregion

        #region protected methods

        protected void Configure(SafeAreaInsets safeAreaInsets)
        {
            SafeAreaInsets = safeAreaInsets;
        }

        protected virtual void TryAndActuateMakeRoomForKeyboard(VisualElement element)
        {
            if (!IsMakeRoomForKeyboardReady || CurrentKeyboardHeight <= 0) return;

            var screenHeight = Xamarin.Essentials.DeviceDisplay.MainDisplayInfo.Height / Xamarin.Essentials.DeviceDisplay.MainDisplayInfo.Density;

            var absElY = GetAbsoluteY(element);
            if (DeviceInfo.Platform == DevicePlatform.iOS) absElY -= SafeAreaInsets.Top;
            absElY += element.Height;

            var kRelY = screenHeight - CurrentKeyboardHeight;
            if (DeviceInfo.Platform == DevicePlatform.Android && DeviceInfo.Version.Major < 11)
            {
                kRelY -= SafeAreaInsets.Bottom;
                kRelY -= SafeAreaInsets.Top;
            }
            else if (DeviceInfo.Platform == DevicePlatform.iOS)
            {
                kRelY -= SafeAreaInsets.Top;
            }

            if (absElY > kRelY)
            {
                var targetY = (absElY - kRelY) + 20;
                MovableElement.TranslateTo(0, -targetY, CurrentKeyboardAnimationDurationMs);
            }
        }

        #endregion

        #region overrides

        protected override void HandleDown(object sender, TouchViewEventArgs e)
        {
            if (!IsMakeRoomForKeyboardReady || CurrentKeyboardHeight <= 0)
            {
                base.HandleDown(sender, e);
            }
        }

        #endregion

        #region helper methods

        protected double GetAbsoluteY(VisualElement element)
        {
            double y = element.Y;
            if (!(element.Parent is null) && element.Parent is VisualElement visualElement) y += GetAbsoluteY(visualElement);
            return y;
        }

        #endregion
    }
}
