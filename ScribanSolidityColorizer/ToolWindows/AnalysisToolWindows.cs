using System;
using System.Runtime.InteropServices;
using Microsoft.VisualStudio.Shell;

namespace ScribanSolidityColorizer.ToolWindows
{
    /// <summary>
    /// This class implements the tool window exposed by this package and hosts a user control.
    /// </summary>
    /// <remarks>
    /// In Visual Studio tool windows are composed of a frame (implemented by the shell) and a pane,
    /// usually implemented by the package implementer.
    /// <para>
    /// This class derives from the ToolWindowPane class provided from the MPF in order to use its
    /// implementation of the IVsUIElementPane interface.
    /// </para>
    /// </remarks>
    [Guid("ed278652-4b64-48fe-b2ad-6d2d36e44978")]
    public class AnalysisToolWindows : ToolWindowPane
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AnalysisToolWindows"/> class.
        /// </summary>
        public AnalysisToolWindows() : base(null)
        {
            this.Caption = "AnalysisToolWindows";

            // This is the user control hosted by the tool window; Note that, even if this class implements IDisposable,
            // we are not calling Dispose on this object. This is because ToolWindowPane calls Dispose on
            // the object returned by the Content property.
            this.Content = new AnalysisToolWindowsControl();
        }
    }
}
