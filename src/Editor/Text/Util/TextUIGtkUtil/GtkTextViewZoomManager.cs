
using System;
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.Utilities;

namespace Microsoft.VisualStudio.Text.Utilities
{
    [ExportImplementation(typeof(ITextViewZoomManager))]
    [Name("Gtk zoom manager")]
    [Order(Before = "default")]
    internal class GtkTextViewZoomManager : ITextViewZoomManager
    {
        public double ZoomLevel(ITextView textView) => ((IXwtTextView)textView).ZoomLevel;

        public void ZoomIn(ITextView textView)
        {
            if (textView is null)
            {
                throw new ArgumentNullException(nameof(textView));
            }

            if (textView is IXwtTextView xwtTextView && xwtTextView.Roles.Contains(PredefinedTextViewRoles.Zoomable))
            {
                double zoomLevel = xwtTextView.ZoomLevel * ZoomConstants.ScalingFactor;
                if (zoomLevel < ZoomConstants.MaxZoom || Math.Abs(zoomLevel - ZoomConstants.MaxZoom) < 0.00001)
                {
                    xwtTextView.Options.GlobalOptions.SetOptionValue(DefaultTextViewOptions.ZoomLevelId, zoomLevel);
                }
            }
        }

        public void ZoomOut(ITextView textView)
        {
            if (textView is null)
            {
                throw new ArgumentNullException(nameof(textView));
            }

            if (textView is IXwtTextView xwtTextView && xwtTextView.Roles.Contains(PredefinedTextViewRoles.Zoomable))
            {
                double zoomLevel = xwtTextView.ZoomLevel / ZoomConstants.ScalingFactor;
                if (zoomLevel > ZoomConstants.MinZoom || Math.Abs(zoomLevel - ZoomConstants.MinZoom) < 0.00001)
                {
                    xwtTextView.Options.GlobalOptions.SetOptionValue(DefaultTextViewOptions.ZoomLevelId, zoomLevel);
                }
            }
        }

        public void ZoomTo(ITextView textView, double zoomLevel)
        {
            if (textView is null)
            {
                throw new ArgumentNullException(nameof(textView));
            }

            if (textView is IXwtTextView xwtTextView && xwtTextView.Roles.Contains(PredefinedTextViewRoles.Zoomable))
            {
                xwtTextView.Options.GlobalOptions.SetOptionValue(DefaultTextViewOptions.ZoomLevelId, zoomLevel);
            }
        }
    }
}
