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
    /// User's settings.
    /// </summary>
    static class Settings
    {
        #region "Application"
        /// <summary>
        /// The width of the window.
        /// </summary>
        public static int win_width = 960;
        /// <summary>
        /// The height of the window.
        /// </summary>
        public static int win_height = 540;
        /// <summary>
        /// Whether the window is resizable.
        /// </summary>
        public static bool Resizable = false;
        /// <summary>
        /// The name of the window.
        /// </summary>
        public static string win_name = "Audio Transcriber";
        /// <summary>
        /// Is the application should be single instance only.
        /// </summary>
        public static bool SingleInstance = true;
        #endregion
        #region "Features"
        /// <summary>
        /// Whether to open the development tools of the browser.
        /// Can be used to assign other debugging functionality as well.
        /// </summary>
        public static bool Debug = false;
        /// <summary>
        /// Whether to check files for tampering.
        /// </summary>
        public static bool TamperCheck = true;
        /// <summary>
        /// Whether to skip shutting down the browser.
        /// Experimental, might produce memory leaks, or ghost processes.
        /// </summary>
        public static bool QuickShutdown = false;
        /// <summary>
        /// Whether the application can load non-local pages.
        /// Scripts from the internet can still be loaded.
        /// </summary>
        public static bool BlockInternet = false;
        /// <summary>
        /// A whitelist of domains that can be accessed even when blockInternet is set to true.
        /// Supports ('*') wildcards.
        /// </summary>
        public static string[] PageWhitelist = { };
        #endregion
        #region "Screens"
        /// <summary>
        /// The screen that will be loaded at the start.
        /// Must be a file path originating inside the 'www' folder in the application's executable.
        /// Remember file paths use forward slashes. ('\')
        /// </summary>
        public static string StartScreen = "index";
        /// <summary>
        /// A list of all files to be hashed and checked for tampering.
        /// </summary>
        public static string[] Files = new string[]
        {
            "libs/jquery.js",
            "index.html",
            "index.css",
            "index.js"
        };
        /// <summary>
        /// A list of all files' hashes. Used to check for tampering.
        /// </summary>
        public static string[] Hashes = new string[]
        {
            "e071abda8fe61194711cfc2ab99fe104",
            "73aeafd1ed562970a0b8ba78f4028e0c",
            "390a6cf19fc936f362268eb893b85ba7",
            "25f7202faa7a4db05fe4d651b795b4f5"
        };
        #endregion
    }
}
