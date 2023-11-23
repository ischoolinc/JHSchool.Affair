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
    public class StuAdmin : BlankPanel
    {
        private static StuAdmin _stu_admin;

        private HelpContentPanel helpContentPanel1;

        public static StuAdmin Instance
        {
            get
            {
                if (_stu_admin == null)
                {
                    _stu_admin = new StuAdmin();
                }
                return _stu_admin;
            }
        }

        private StuAdmin()
        {
            InitializeComponent();
            ((BlankPanel)this).Group = "學務作業";
            if (!helpContentPanel1.NavigateBySetting(((BlankPanel)this).Group))
            {
                helpContentPanel1.Naviate("https://support.ischool.com.tw/hc/zh-tw");
            }
        }

        private void InitializeComponent()
        {
            this.helpContentPanel1 = new JHSchool.Affair.HelpContentPanel();
            this.ContentPanePanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // ContentPanePanel
            // 
            this.ContentPanePanel.Controls.Add(this.helpContentPanel1);
            this.ContentPanePanel.Location = new System.Drawing.Point(0, 163);
            this.ContentPanePanel.Size = new System.Drawing.Size(870, 421);
            // 
            // helpContentPanel1
            // 
            this.helpContentPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.helpContentPanel1.Location = new System.Drawing.Point(0, 0);
            this.helpContentPanel1.Name = "helpContentPanel1";
            this.helpContentPanel1.Size = new System.Drawing.Size(870, 421);
            this.helpContentPanel1.TabIndex = 0;
            // 
            // StuAdmin
            // 
            this.Name = "StuAdmin";
            this.ContentPanePanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }
    }

}
