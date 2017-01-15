using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CefSharp;
using CefSharp.WinForms;
using System.Diagnostics;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;

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

        public void DownloadSubs(string link, string fileName)
        {
            //Define the request.
            WebRequest request = WebRequest.Create("https://downsub.com/?url=" + link);
            //Setup a response object.
            WebResponse response;
            try
            {
                //Try to get a response from the page.
                response = request.GetResponse();
                //Read the response stream.
                StreamReader data = new StreamReader(response.GetResponseStream());
                string dataRead = data.ReadToEnd();

                Match dlLink = new Regex("(?:<b><a href=\".)[\\s\\S]+?(?:\">)").Match(dataRead);
                if (dlLink.Success == false) { Core.RunJS(new string[] { "downloadFailed();" }); return;}

                string dlLinke = dlLink.Value;

                dlLinke = dlLinke.Replace("<b><a href=\".", "").Replace("\">", "");

                if (dlLinke == "") { Core.RunJS(new string[] { "downloadFailed();" }); return; }

                WebRequest DLrequest = WebRequest.Create("https://downsub.com" + dlLinke);
                //Setup a response object.
                WebResponse DLresponse;

                DLresponse = DLrequest.GetResponse();

                data = new StreamReader(DLresponse.GetResponseStream());
                dataRead = data.ReadToEnd();

                string[] file = dataRead.Split('\n');

                List<string> finaltext = new List<string>();

                for (int i = 0; i < file.Length; i++)
                {
                    bool skip = false;
                    int a;
                    if (file[i].Contains("-->")) skip = true;
                    if (file[i] == "") skip = true;
                    if (int.TryParse(file[i], out a) == true) skip = true;

                    if (skip == false) finaltext.Add(file[i].Replace("<font color=\"#CCCCCC\">", "").Replace("</font>", "").Replace("<font color=\"#E5E5E5\">", ""));          
                }

                System.IO.Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "Downloads");

                Soul.IO.WriteFile(AppDomain.CurrentDomain.BaseDirectory + "Downloads\\" + fileName + ".txt", finaltext.ToArray());

                Core.RunJS(new string[] { "downloadOK('Trascribe successful! Saved to Downloads folder.');" });

            }
            catch
            {
                Core.RunJS(new string[] { "downloadFailed();" });
            }
        }
    }
}
