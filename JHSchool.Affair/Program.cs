using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FISCA;
using FISCA.Presentation;


namespace JHSchool.Affair
{
    public class Program
    {
        [MainMethod("JHSchool.Affair")]
        public static void Main()
        {
            RegisterTab();
        }

        public static void RegisterTab()
        {
            MotherForm.AddPanel((IBlankPanel)(object)EduAdmin.Instance);
            MotherForm.AddPanel((IBlankPanel)(object)StuAdmin.Instance);
        }
    }
}
