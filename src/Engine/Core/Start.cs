using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

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
    /// The entry point for the application.
    /// </summary>
    static class Program
    {

        static Mutex mutex = new Mutex(true, "{" + Core.GUID + "}");

        [STAThread]
        static void Main()
        {
            if (Settings.SingleInstance == false || mutex.WaitOne(TimeSpan.Zero, true))
            {
                //Setup the core.
                Core.Setup();
            }
        }
    }
}
