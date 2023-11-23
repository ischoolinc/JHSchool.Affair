using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FISCA.Presentation;

namespace JHSchool.Affair
{
    public class EduAdmin : BlankPanel
    {
        private static EduAdmin _edu_admin;

        private HelpContentPanel helpContentPanel1;

        public static EduAdmin Instance
        {
            get
            {
                if (_edu_admin == null)
                {
                    _edu_admin = new EduAdmin();
                }
                return _edu_admin;
            }
        }

        private EduAdmin()
        {
            InitializeComponent();
            ((BlankPanel)this).Group = "教務作業";
            if (!helpContentPanel1.NavigateBySetting(((BlankPanel)this).Group))
            {
                helpContentPanel1.Naviate("https://support.ischool.com.tw/hc/zh-tw");
            }
        }

        private void InitializeComponent()
        {
            helpContentPanel1 = new HelpContentPanel();
            base.ContentPanePanel.SuspendLayout();
            ((Control)this).SuspendLayout();
            base.ContentPanePanel.Controls.Add(helpContentPanel1);
            base.ContentPanePanel.Location = new Point(0, 163);
            base.ContentPanePanel.Size = new Size(870, 421);
            helpContentPanel1.Dock = DockStyle.Fill;
            helpContentPanel1.Location = new Point(0, 0);
            helpContentPanel1.Name = "helpContentPanel1";
            helpContentPanel1.Size = new Size(870, 421);
            helpContentPanel1.TabIndex = 0;
            ((Control)this).Name = "EduAdmin";
            base.ContentPanePanel.ResumeLayout(performLayout: false);
            ((Control)this).ResumeLayout(performLayout: false);
        }
    }
}
