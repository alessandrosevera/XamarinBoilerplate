using System;
using Xamarin.Essentials;

namespace MobileAppForms.Ux
{
    public static class ScalableUi
    {
        #region const

        private const double REFERENCE_HEIGHT = 812;
        private const double REFERENCE_WIDTH = 375;

        private const double PROPERTY_LIST_ITEM_IMAGE_HEIGHT_RATIO = 0.77639751552795d;

        #endregion

        #region properties

        public static double PropertyListItemImageHeight = 125;

        public static double FS22 = 22;
        public static double FS16 = 16;
        public static double FS14 = 14;

        public static double ScaledFS22 = 22;
        public static double ScaledFS16 = 16;
        public static double ScaledFS14 = 14;

        public static double ScaledLimitFS22 = 22;
        public static double ScaledLimitFS16 = 16;
        public static double ScaledLimitFS14 = 14;



        public static double W161 = 161;

        public static double ScaledW161 = 161;

        public static double ScaledLimitW161 = 161;



        public static double H110 = 110;

        public static double ScaledH110 = 110;

        public static double ScaledLimitH110 = 110;



        public static bool DidScale = false;

        #endregion

        #region access methods

        public static void Scale(double deviceHeight, double deviceWidth, DevicePlatform platform)
        {
            if (DidScale) return;

            DidScale = true;


            var fontYMultiplier = deviceHeight / REFERENCE_HEIGHT;
            fontYMultiplier = Math.Max(Math.Min(fontYMultiplier, 1.15), 0.85);

            var frameXMultiplier = deviceWidth / REFERENCE_WIDTH;
            var frameYMultiplier = deviceHeight / REFERENCE_HEIGHT;

            /*var frameYMultiplier = deviceHeight / REFERENCE_HEIGHT;
            frameYMultiplier = Math.Min(frameYMultiplier, 1.6);

            var frameXMultiplier = deviceWidth / REFERENCE_WIDTH;*/

            // ScaledFS16 = Math.Floor(FS16 * fontYMultiplier);
            // ScaledLimitFS16 = Math.Floor(FS16 * Math.Min(fontYMultiplier, 1));


            ScaledFS22 = FS22 * fontYMultiplier;
            ScaledLimitFS22 = FS22 * Math.Min(fontYMultiplier, 1);

            ScaledFS16 = FS16 * fontYMultiplier;
            ScaledLimitFS16 = FS16 * Math.Min(fontYMultiplier, 1);

            ScaledFS14 = FS14 * fontYMultiplier;
            ScaledLimitFS14 = FS14 * Math.Min(fontYMultiplier, 1);

            ScaledW161 = W161 * frameXMultiplier;
            ScaledLimitW161 = W161 * Math.Min(frameXMultiplier, 1);

            ScaledH110 = H110 * frameYMultiplier;
            ScaledLimitH110 = H110 * Math.Min(frameYMultiplier, 1);

            PropertyListItemImageHeight = ScaledLimitW161 * PROPERTY_LIST_ITEM_IMAGE_HEIGHT_RATIO;
        }

        #endregion
    }
}
