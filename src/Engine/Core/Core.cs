using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CefSharp;
using CefSharp.WinForms;

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
    /// The core of the application.
    /// </summary>
    class Core
    {
        #region "Declarations"
        #region "Framework Information"
        /// <summary>
        /// The name of the engine.
        /// </summary>
        public static string Name = "SoulApp";
        /// <summary>
        /// The version of the engine.
        /// </summary>
        public static string Version = "1";
        /// <summary>
        /// The GUID of the application. Used on windows to prevent multi-instancing.
        /// The default SoulApp GUID - e4767775-5a21-49cd-998d-fc2bc6106916
        /// </summary>
        public static string GUID = "e4767775-5a21-49cd-998d-fc2bc6106916";
        #endregion
        #region "Internal Objects"
        /// <summary>
        /// The form instance the framework uses.
        /// </summary>
        public static Engine host;
        /// <summary>
        /// The browser that hosts the application.
        /// </summary>
        public static ChromiumWebBrowser browser;
        #endregion
        #region "Flags"
        /// <summary>
        /// Whether the primary update loop should be running.
        /// </summary>
        public static bool UpdateLoop = true;
        /// <summary>
        /// Used to track whether the browser is loading.
        /// </summary>
        private static bool Loading = false;
        /// <summary>
        /// Used to track whether the browser has initialized.
        /// </summary>
        private static bool browserInit = false;
        /// <summary>
        /// The page that was loaded before the current one.
        /// </summary>
        private static string lastLoadedPage = "";
        /// <summary>
        /// Whether the application should close.
        /// </summary>
        private static bool queueExit = false;
        #endregion
        #endregion

        #region "Boot"
        /// <summary>
        /// Creates the form instance.
        /// </summary>
        public static void Setup()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            host = new Engine();
            Application.Run(host);
        }
        /// <summary>
        /// Setups the engine.
        /// </summary>
        public static void StartSequence()
        {
            //Apply settings.
            ApplySettings();

            //Setup the browser.
            SetupBrowser();

            //Show the application.
            host.Show();

            //Start the user beginning method.
            User.LoadingComplete();

            //Start the loop.
            Update();
        }
		/// <summary>
        /// Applies the Settings class.
        /// </summary>
        private static void ApplySettings()
        {
            //Window
            host.Text = Settings.win_name; //The window's text.
            host.ClientSize = new System.Drawing.Size(Settings.win_width, Settings.win_height); //The size of the window.

            if (Settings.Resizable == false)
            {
                host.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
                host.MaximizeBox = false;
            }
        }
		/// <summary>
        /// Setup the browser that hosts the application.
        /// </summary>
		private static void SetupBrowser()
        {
            CefSettings GlobalSettings = new CefSettings();
            // Initialize cef with the provided settings
            Cef.Initialize(GlobalSettings);
            // Create a browser component
            browser = new ChromiumWebBrowser("");
            LoadScreen(Settings.StartScreen);
            // Add it to the form and fill it to the form window.
            host.Controls.Add(browser);
            host.Dock = DockStyle.Fill;

            BrowserSettings BrowserSettings = new BrowserSettings();
            BrowserSettings.FileAccessFromFileUrls = CefState.Enabled;
            BrowserSettings.UniversalAccessFromFileUrls = CefState.Enabled;
            browser.BrowserSettings = BrowserSettings;
            browser.AllowDrop = false;
            browser.DragHandler = new CustomDraggerHandler();
            browser.MenuHandler = new CustomRightClickMenuContextHandler();
            browser.AddressChanged += PageChanged;

            //Register the app bridge to be used by the javascript.
            BindingOptions bindOptions = new BindingOptions();
            bindOptions.CamelCaseJavascriptNames = false;
            browser.RegisterJsObject("SoulApp", new SoulAppBridge(), bindOptions);
        }
        #region "Browser Events"
        /// <summary>
        /// Is used to trigger the screen refreshing event when redirected from inside the browser.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void PageChanged(object sender, AddressChangedEventArgs e)
        {
            //If the page is changed we need to run new page code.
            UpdateScreen();
        }
        #endregion
        #endregion
        #region "Loop and Functions"
        /// <summary>
        /// The primary update loop.
        /// </summary>
        private static void Update()
        {
			while(UpdateLoop)
            {
                //Update the application.
                Application.DoEvents();

                //Hide the browser if not loaded, and show the loading circle.
				if(host.IsDisposed == false)
                {
                    browser.Visible = !browser.IsLoading;
                    host.Controls.Find("loadingbox", true)[0].Visible = browser.IsLoading;
                }
               
                //Check for browser loading changes.
                if (browser.IsBrowserInitialized == true && Loading != browser.IsLoading)
                {
                    //If not loading then run the page finished code.
                    if (browser.IsLoading == false)
                    {
                        //Run the user's page loaded code.
                        User.BrowserReady(ReversePath(browser.Address));
                    }
                    else //If loading then run the page will load code.
                    {
                        //Set the last loaded page.
                        lastLoadedPage = ReversePath(browser.Address);
                        //Run the user's page loading code.
                        User.BrowserNotReady(lastLoadedPage, ReversePath(browser.Address));
                    }

                    //Update the tracker.
                    Loading = browser.IsLoading;
                }

                //Is run when the browser is first initialized.
                if (browser.IsBrowserInitialized != browserInit && browserInit == false)
                {
                    //Start debug mode, if enabled.
                    DebugMode();

                    //Run the user code for initialization.
                    User.BrowserInitialized();

                    //Update the tracker.
                    browserInit = true;
                }

                //Check if closing.
                if (queueExit == true)
                {
                    if (browser.IsBrowserInitialized == true) browser.Stop(); //Stop loading the page.

                    host.Close();
                }

                //Run the user's update code.
                User.Update();
            }
        }
        /// <summary>
        /// Executes the specified javascript code on the host browser.
        /// </summary>
        /// <param name="script">The script to execute as an array.</param>
        public static void RunJS(string[] script)
        {
            string scriptToExec = string.Join("\n", script);

            browser.ExecuteScriptAsync(scriptToExec);
        }
        /// <summary>
        /// Executes the specified javascript file.
        /// </summary>
        /// <param name="scriptPath">The path of the JS file.</param>
        public static void ExecuteScriot(string scriptPath)
        {
            //Check files for tampering.
            CheckFiles();
            //Run the script.
            RunJS(Soul.IO.ReadFile(FormatPath(scriptPath, "js"), true));
        }
        /// <summary>
        /// Checks the integrity of all linked files.
        /// This is used to prevent tampering.
        /// </summary>
        public static void CheckFiles()
        {
            //If a close is already queued, no need to check.
            if (queueExit == true) return;

            Console.WriteLine("===================================================");
            Console.WriteLine("File check queued.");

            //Loop through all files.
            for (int i = 0; i < Settings.Files.Length; i++)
            {
                //Calculate the hash of the file. We send an empty extension to the FormatPath function because the path is intended to be included with the file.
                string hash = Soul.Encryption.GetMD5(FormatPath(Settings.Files[i], ""));
                //Check the hash agaisnt the one that it is supposed to be.
                if (Settings.Hashes.Length - 1 < i || hash != Settings.Hashes[i])
                {
                    Console.WriteLine("Hash check failed for [" + Settings.Files[i] + "]: {" + hash + "}");

                    //If tamper checking is on, then close the application.
                    if (Settings.TamperCheck == true)
                    {
                        MessageBox.Show("The application's files have been editted or are corrupted. Please redownload or reinstall the application.",
                            "Corrupted Application", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        queueExit = true;
                        Console.WriteLine("Stopping Application, File Check Error");
                        break;
                    }
                }
            }
            Console.WriteLine("===================================================");
        }
        #endregion
        #region "Screen Loading"
        /// <summary>
        /// Loads an html file onto the browser as specified by its path. The root of the path should be
        /// the "www" directory in the executable's folder.
        /// </summary>
        /// <param name="path"></param>
        public static void LoadScreen(string path)
        {
            //Fix the path.
            path = FormatPath(path);
            //Load the page.
            browser.Load(path);
        }
        /// <summary>
        /// Loads an html file onto the browser from an array where each line is a line of html.
        /// </summary>
        /// <param name="htmlFile">The html file as an array.</param>
        public static void LoadScreen(string[] htmlFile)
        {
            //Load the html into the browser.
            browser.LoadHtml(string.Join("\n", htmlFile), "http://localhost/");
        }
        /// <summary>
        /// Updates the current screen.
        /// </summary>
        public static void UpdateScreen()
        {
            //Check if block internet is on, and we are trying to visit a page on the internet.
            if(!(browser.Address.Contains("file:///")) && Settings.BlockInternet && !(browser.Address == "about:blank"))
            {
                //Define a variable to hold whether we've found the domain in the whitelist.
                bool inWhitelist = false;
                //Get the domain of the current address.
                string domain = browser.Address.Substring(browser.Address.IndexOf("//") + 2);
                domain = domain.Substring(0, domain.IndexOf('/'));
                //Go through the whitelist.
                for (int i = 0; i < Settings.PageWhitelist.Length; i++)
                {
                    //Check if the current domain in the whitelist is the current address.
                    if(Settings.PageWhitelist[i] == domain)
                    {
                        inWhitelist = true;
                    }
                    //Check if the current domain has a wild card.
                    if(Settings.PageWhitelist[i].Contains("*"))
                    {
                        //Get the domain from the whitelist without the part after the wildcard.
                        string whitelistBeforeWildcard = Settings.PageWhitelist[i].Substring(0, Settings.PageWhitelist[i].IndexOf("*"));
                        //Check if the current domain contains the wildcarded one.
                        if(domain.Contains(whitelistBeforeWildcard))
                        {
                            inWhitelist = true;
                        }
                    }
                }
                if(inWhitelist == false) browser.Load("about:blank");
            }

            //Check file integrity in case edits have been made since last load.
            CheckFiles();
        }
        #endregion
        #region "Other"
        /// <summary>
        /// Cleanup before closing is executed here.
        /// </summary>
        public static void Closing()
        {
            //Stop the loop.
            UpdateLoop = false;
            //Hide the application.
            host.Visible = false;

            if (Settings.QuickShutdown == true) return;

            //Stop the browser.
            Cef.Shutdown();
        }
		/// <summary>
        /// Opens the browser's dev tools.
        /// </summary>
        private static void DebugMode()
        {
            //Check if browser is not ready.
            if (browser.IsBrowserInitialized == false) return;

            //Reflect the status on the dev tools.
            if (Settings.Debug == true)
                browser.ShowDevTools();
            else
                browser.CloseDevTools();
        }
        #endregion
        #region "Helpers"
        /// <summary>
        /// Formats a file path to be the full path on the file system, and adds an extension. (app + \www\)
        /// </summary>
        /// <param name="internalPath">The internally used path.</param>
        /// <param name="extension">The file's extension.</param>
        public static string FormatPath(string internalPath, string extension = "html")
        {
            return Application.StartupPath + @"/www/" + internalPath + "." + extension;
        }
        /// <summary>
        /// Transforms a file path to an internal path.
        /// </summary>
        /// <param name="fullPath">The full file path.</param>
        /// <param name="extension">The file's extension.</param>
        public static string ReversePath(string fullPath, string extension = "html")
        {
            return fullPath.Replace(Application.StartupPath + @"/www/", "").Replace("." + extension, "");
        }
        #endregion

    }
}
