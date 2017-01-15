using CefSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoulApp
{
    //////////////////////////////////////////////////////////////////////////////
    // SoulApp - A framework for running JS based applications on the desktop.  //
    //                                                                          //
    // Copyright © 2016 Vlad Abadzhiev - TheCryru@gmail.com                     //
    //                                                                          //
    // For any questions and issues: https://github.com/Cryru/SoulApp           //
    //////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// Defines a custom dragging interface, to prevent items from being dragged onto the window.
    /// </summary>
    public class CustomDraggerHandler : IDragHandler
    {
        public bool OnDragEnter(IWebBrowser browserControl, IBrowser browser, IDragData dragData, DragOperationsMask mask)
        {
            return true;
        }

        public void OnDraggableRegionsChanged(IWebBrowser browserControl, IBrowser browser, IList<DraggableRegion> regions)
        {
            
        }
    }
}
