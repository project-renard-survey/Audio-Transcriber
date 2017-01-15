using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CefSharp;
using CefSharp.WinForms;
using System.Security.Cryptography;
using System.IO;

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
    /// The form hosting the application.
    /// </summary>
    public partial class Engine : Form
    {
        #region "Declarations"
        #region "Information"

        #endregion
        #region "Primary Objects"

        #endregion
        #region "Others"
       
        #endregion
        #endregion

        #region "Startup"
        /// <summary>
        /// Initializes the form.
        /// </summary>
        public Engine()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Is run when the Engine has finished loading.
        /// </summary>
        private void Loaded(object sender, EventArgs e)
        {
            Core.StartSequence();
        }
        /// <summary>
        /// Is run when form is closed.
        /// Objects are disposed here.
        /// </summary>
        private void Unloading(object sender, FormClosedEventArgs e)
        {
            Core.Closing();
        }
        #endregion
        #region "Security"

        #endregion
        #region "Debugging"
       
        #endregion
    }
}
