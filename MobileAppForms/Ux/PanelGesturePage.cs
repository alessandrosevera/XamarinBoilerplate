using System;
using Xamarin.Forms;

namespace MobileAppForms.Ux
{
    // TODO IMPROVE THIS! DUPLICATION!
    public abstract class PanelGestureAbsoluteLayout : AbsoluteLayout
    {
        #region auto-properties

        protected bool CanDrag { get; private set; }
        protected float? DragStartY { get; private set; }
        protected float? DragLatestY { get; private set; }
        protected float DragDistanceY { get; private set; }
        protected float DragTotalY { get; private set; }

        protected abstract double AdditionalDraggableHeight { get; }
        protected abstract View PanelDraggableArea { get; }
        protected abstract View Panel { get; }

        #endregion

        #region event handlers

        protected virtual void HandleDown(System.Object sender, SimpleTouchView.TouchViewEventArgs e)
        {
            var activePointStartY = GetAbsoluteY(PanelDraggableArea, true);
            var activePointEndY = activePointStartY + PanelDraggableArea.Height + AdditionalDraggableHeight;
            var hitPoint = e.Y;

            CanDrag = (hitPoint >= activePointStartY && hitPoint <= activePointEndY);
            if (CanDrag)
            {
                DragStartY = hitPoint;
                DragLatestY = hitPoint;
            }
        }

        protected virtual void HandleUp(System.Object sender, SimpleTouchView.TouchViewEventArgs e)
        {
            CanDrag = false;
        }

        protected virtual void HandlePanning(System.Object sender, SimpleTouchView.TouchViewEventArgs e)
        {
            var hitPoint = e.Y;
            bool mustPreventDrag = false;

            if (Panel.TranslationY == 0 && hitPoint < DragStartY)
            {
                var activePointStartY = GetAbsoluteY(PanelDraggableArea, true);
                var activePointEndY = activePointStartY + PanelDraggableArea.Height;
                mustPreventDrag = !(hitPoint >= activePointStartY && hitPoint <= activePointEndY);
            }

            if (CanDrag && !mustPreventDrag && DragStartY.HasValue && DragLatestY.HasValue)
            {
                var maxTranslationY = Panel.Height - (PanelDraggableArea.Height + AdditionalDraggableHeight);

                DragTotalY = DragStartY.Value - hitPoint;
                DragDistanceY = DragLatestY.Value - hitPoint;

                double newTranslationY = Panel.TranslationY - DragDistanceY;
                if (newTranslationY < 0) newTranslationY = 0;
                if (newTranslationY > maxTranslationY) newTranslationY = maxTranslationY;
                Panel.TranslationY = newTranslationY;

                DragLatestY = hitPoint;
            }
        }

        protected virtual void HandlePanned(System.Object sender, SimpleTouchView.TouchViewEventArgs e)
        {
            CanDrag = false;
            DragStartY = null;
            DragLatestY = null;
            DragDistanceY = 0;
            DragTotalY = 0;
        }

        #endregion

        #region helper methods

        protected double GetAbsoluteY(VisualElement element, bool includeTranslation = false)
        {
            double y = element.Y;
            if (includeTranslation) y += element.TranslationY;
            if (!(element.Parent is null) && element.Parent is VisualElement visualElement) y += GetAbsoluteY(visualElement, includeTranslation);
            return y;
        }

        #endregion
    }

    public abstract class PanelGesturePage : ContentPage
    {
        #region auto-properties

        protected bool CanDrag { get; private set; }
        protected float? DragStartY { get; private set; }
        protected float? DragLatestY { get; private set; }
        protected float DragDistanceY { get; private set; }
        protected float DragTotalY { get; private set; }

        protected abstract double AdditionalDraggableHeight { get; }
        protected abstract View PanelDraggableArea { get; }
        protected abstract View Panel { get; }

        #endregion

        #region event handlers

        protected void HandleDown(System.Object sender, SimpleTouchView.TouchViewEventArgs e)
        {
            if (!(PanelDraggableArea is null))
            {
                var activePointStartY = GetAbsoluteY(PanelDraggableArea, true);
                var activePointEndY = activePointStartY + PanelDraggableArea.Height + AdditionalDraggableHeight;
                var hitPoint = e.Y;

                CanDrag = (hitPoint >= activePointStartY && hitPoint <= activePointEndY);
                if (CanDrag)
                {
                    DragStartY = hitPoint;
                    DragLatestY = hitPoint;
                }
            }
            else
            {
                CanDrag = false;
            }
        }

        protected void HandleUp(System.Object sender, SimpleTouchView.TouchViewEventArgs e)
        {
            CanDrag = false;
        }

        protected void HandlePanning(System.Object sender, SimpleTouchView.TouchViewEventArgs e)
        {
            var hitPoint = e.Y;
            bool mustPreventDrag = false;

            if (!(PanelDraggableArea is null) && !(Panel is null))
            {
                if (Panel.TranslationY == 0 && hitPoint < DragStartY)
                {
                    var activePointStartY = GetAbsoluteY(PanelDraggableArea, true);
                    var activePointEndY = activePointStartY + PanelDraggableArea.Height;
                    mustPreventDrag = !(hitPoint >= activePointStartY && hitPoint <= activePointEndY);
                }

                if (CanDrag && !mustPreventDrag && DragStartY.HasValue && DragLatestY.HasValue)
                {
                    var maxTranslationY = Panel.Height - (PanelDraggableArea.Height + AdditionalDraggableHeight);

                    DragTotalY = DragStartY.Value - hitPoint;
                    DragDistanceY = DragLatestY.Value - hitPoint;

                    double newTranslationY = Panel.TranslationY - DragDistanceY;
                    if (newTranslationY < 0) newTranslationY = 0;
                    if (newTranslationY > maxTranslationY) newTranslationY = maxTranslationY;
                    Panel.TranslationY = newTranslationY;

                    DragLatestY = hitPoint;
                }
            }
            else
            {
                // TODO ???
            }
        }

        protected virtual void HandlePanned(System.Object sender, SimpleTouchView.TouchViewEventArgs e)
        {
            CanDrag = false;
            DragStartY = null;
            DragLatestY = null;
            DragDistanceY = 0;
            DragTotalY = 0;
        }

        #endregion

        #region helper methods

        protected double GetAbsoluteY(VisualElement element, bool includeTranslation = false)
        {
            double y = element.Y;
            if (includeTranslation) y += element.TranslationY;
            if (!(element.Parent is null) && element.Parent is VisualElement visualElement) y += GetAbsoluteY(visualElement, includeTranslation);
            return y;
        }

        #endregion
    }
}
