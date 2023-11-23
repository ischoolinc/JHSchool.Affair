using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml.Linq;
using FISCA;

namespace JHSchool.Affair
{
    public partial class HelpContentPanel : UserControl
    {
        private IContainer components = null;

        private WebBrowser Web;

        public HelpContentPanel()
        {
            InitializeComponent();
            Web.ScriptErrorsSuppressed = true;
            Web.DocumentCompleted += Web_DocumentCompleted;
            Web.Navigated += Web_Navigated;
        }

        public bool NavigateBySetting(string urlKey)
        {
            try
            {
                string path = Assembly.GetExecutingAssembly().Location + ".urls";
                if (!File.Exists(path))
                {
                    return false;
                }
                XElement urls = XElement.Load(path);
                XElement url = urls.Element(urlKey);
                if (url == null)
                {
                    return false;
                }
                Web.Navigate(url.Value);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        private void Web_Navigated(object sender, WebBrowserNavigatedEventArgs e)
        {
            if (Web.Document != null)
            {
                Web.Document.Click -= Document_Click;
            }
        }

        private void Web_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            Web.Document.Click += Document_Click;
        }

        private void Document_Click(object sender, HtmlElementEventArgs e)
        {
            TryInvokeFeature(e);
        }

        private void TryInvokeFeature(HtmlElementEventArgs e)
        {
            //IL_00b8: Unknown result type (might be due to invalid IL or missing references)
            //IL_00bf: Expected O, but got Unknown
            if (!(Web.Document.ActiveElement != null))
            {
                return;
            }
            HtmlElement link = Web.Document.ActiveElement;
            string url = link.GetAttribute("href");
            if (string.IsNullOrEmpty(url) || !url.StartsWith("http://feature.ischool.com.tw", StringComparison.OrdinalIgnoreCase))
            {
                return;
            }
            Regex cmdpattern = new Regex("^http://feature.ischool.com.tw/([\\w\\.\\-/]+)\\??(.*)", RegexOptions.Singleline);
            Regex argpattern = new Regex("\\?(.*)", RegexOptions.Singleline);
            Match strCmdMatch = cmdpattern.Match(url);
            if (!strCmdMatch.Success)
            {
                return;
            }
            Match strArgMatch = argpattern.Match(url);
            List<string> preprocess = new List<string>();
            List<string> postprocess = new List<string>();
            ArgDictionary args = new ArgDictionary();
            Regex pre = new Regex("preprocess\\s*=\\s*\\((([\\w\\.\\-/]+)(?:,?))+\\)", RegexOptions.IgnoreCase | RegexOptions.Singleline);
            Regex post = new Regex("postprocess\\s*=\\s*\\((([\\w\\.\\-/]+)(?:,?))+\\)", RegexOptions.IgnoreCase | RegexOptions.Singleline);
            string[] strArgs = strArgMatch.Groups[1].Value.Split(new string[1] { "&" }, StringSplitOptions.RemoveEmptyEntries);
            string[] array = strArgs;
            foreach (string arg in array)
            {
                Match process = pre.Match(arg);
                if (process.Success)
                {
                    foreach (Capture capture in process.Groups[2].Captures)
                    {
                        preprocess.Add(capture.Value);
                    }
                }
                process = post.Match(arg);
                if (process.Success)
                {
                    foreach (Capture capture in process.Groups[2].Captures)
                    {
                        postprocess.Add(capture.Value);
                    }
                }
                string[] argPair = arg.Split(new string[1] { "=" }, StringSplitOptions.RemoveEmptyEntries);
                if (argPair.Length >= 2)
                {
                    ((Hashtable)(object)args).Add((object)argPair[0].Trim(), (object)argPair[1].Trim());
                }
                else
                {
                    ((Hashtable)(object)args).Add((object)argPair[0].Trim(), (object)string.Empty);
                }
            }
            e.ReturnValue = false;
            try
            {
                List<string> invocation = new List<string>();
                invocation.AddRange(preprocess);
                invocation.Add(strCmdMatch.Groups[1].Value);
                invocation.AddRange(postprocess);
                Features.Invoke(args, invocation.ToArray());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void Naviate(string url)
        {
            Web.Navigate(url);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.Web = new System.Windows.Forms.WebBrowser();
            base.SuspendLayout();
            this.Web.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Web.Location = new System.Drawing.Point(0, 0);
            this.Web.MinimumSize = new System.Drawing.Size(20, 20);
            this.Web.Name = "Web";
            this.Web.Size = new System.Drawing.Size(625, 522);
            this.Web.TabIndex = 0;
            base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
            base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            base.Controls.Add(this.Web);
            base.Name = "HelpContentPanel";
            base.Size = new System.Drawing.Size(625, 522);
            base.ResumeLayout(false);
        }

    }

}
