using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace MobileAppForms
{
    public partial class MainPage : ContentPage
    {
        #region auto-properties

        private bool CanDrag { get; set; }
        private float? DragStartY { get; set; }
        private float? DragLatestY { get; set; }
        private float DragDistanceY { get; set; }
        private float DragTotalY { get; set; }

        #endregion

        #region ctor(s)

        public MainPage()
        {
            InitializeComponent();
        }

        #endregion

        #region event handlers

        private void HandleDown(System.Object sender, SimpleTouchView.TouchViewEventArgs e)
        {
            var activePointStartY = GetAbsoluteY(PanelDraggableArea, true);
            var activePointEndY = activePointStartY + PanelDraggableArea.Height;
            var hitPoint = e.Y;

            CanDrag = (hitPoint >= activePointStartY && hitPoint <= activePointEndY);
            if (CanDrag)
            {
                DragStartY = hitPoint;
                DragLatestY = hitPoint;
            }
        }

        private void HandleUp(System.Object sender, SimpleTouchView.TouchViewEventArgs e)
        {
            CanDrag = false;
        }

        private void HandlePanning(System.Object sender, SimpleTouchView.TouchViewEventArgs e)
        {
            var hitPoint = e.Y;
            bool mustPreventDrag = false;

            if (Panel.TranslationY == 0)
            {
                var activePointStartY = GetAbsoluteY(PanelDraggableArea, true);
                var activePointEndY = activePointStartY + PanelDraggableArea.Height;
                mustPreventDrag = !(hitPoint >= activePointStartY && hitPoint <= activePointEndY);
            }

            if (CanDrag && !mustPreventDrag && DragStartY.HasValue && DragLatestY.HasValue)
            {
                DragTotalY = DragStartY.Value - hitPoint;
                DragDistanceY = DragLatestY.Value - hitPoint;

                double newTranslationY = Panel.TranslationY - DragDistanceY;
                if (newTranslationY < 0) newTranslationY = 0;
                Panel.TranslationY = newTranslationY;

                DragLatestY = hitPoint;
            }
        }

        private void HandlePanned(System.Object sender, SimpleTouchView.TouchViewEventArgs e)
        {
            CanDrag = false;
            DragStartY = null;
            DragLatestY = null;
            DragDistanceY = 0;
            DragTotalY = 0;
        }

        #endregion

        #region helper methods

        private double GetAbsoluteY(VisualElement element, bool includeTranslation = false)
        {
            double y = element.Y;
            if (includeTranslation) y += element.TranslationY;
            if (!(element.Parent is null) && element.Parent is VisualElement visualElement) y += GetAbsoluteY(visualElement, includeTranslation);
            return y;
        }

        #endregion
    }
}
