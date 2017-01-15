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
    /// The application's Core, form insides.
    /// </summary>
    partial class Engine
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region "Windows Form Designer generated code"
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Engine));
            this.loadingbox = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.loadingbox)).BeginInit();
            this.SuspendLayout();
            // 
            // loadingbox
            // 
            this.loadingbox.BackColor = System.Drawing.Color.Silver;
            this.loadingbox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.loadingbox.Image = global::SoulApp.Properties.Resources.loading;
            this.loadingbox.Location = new System.Drawing.Point(0, 0);
            this.loadingbox.Name = "loadingbox";
            this.loadingbox.Size = new System.Drawing.Size(960, 540);
            this.loadingbox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.loadingbox.TabIndex = 0;
            this.loadingbox.TabStop = false;
            this.loadingbox.WaitOnLoad = true;
            // 
            // Engine
            // 
            this.ClientSize = new System.Drawing.Size(960, 540);
            this.Controls.Add(this.loadingbox);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Engine";
            this.KeyPreview = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.DoubleBuffered = true;
            this.Load += new System.EventHandler(this.Loaded);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Unloading);
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            ((System.ComponentModel.ISupportInitialize)(this.loadingbox)).EndInit();
            this.ResumeLayout(false);

        }
        #endregion

        private System.Windows.Forms.PictureBox loadingbox;
    }
}