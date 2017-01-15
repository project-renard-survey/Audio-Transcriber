using System.Diagnostics;

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
    /// This class is the one that will be created as a SoulApp object to be used in the browser.
    /// All interfacing between the JS and the application is done through here.
    /// </summary>
    partial class SoulAppBridge
    {
        #region "Declarations"

        #endregion
        #region "System Functions"
        /// <summary>
        /// Loads the screen by its name.
        /// </summary>
        /// <param name="screenName">The name of the screen to load.</param>
        public void LoadScreen(string screenName)
        {
            Core.LoadScreen(screenName);
        }
        /// <summary>
        /// Returns the framework's information.
        /// </summary>
        /// <returns></returns>
        public string GetFrameworkInfo()
        {
            return Core.Name + " ver." + Core.Version;
        }
        /// <summary>
        /// Opens the link in the user's default browser.
        /// </summary>
        /// <param name="url"></param>
        public void OpenLink(string url)
        {
            //Check if a link.
            if(url.Contains("http://") || url.Contains("https://"))
            {
                Process.Start(url);
            }
        }
        /// <summary>
        /// Executes the specified javascript file.
        /// </summary>
        /// <param name="scriptName">The javascript file to execute.</param>
        public void RunScript(string scriptName)
        {
            Core.ExecuteScriot(scriptName);
        }
        #endregion
    }
}
