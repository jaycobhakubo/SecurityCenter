using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace GTI.Modules.SecurityCenter
{
    public class program
    {
        [MTAThread]
        static void Main(string[] args)
        {
            SecurityCenterModule sc = new SecurityCenterModule();

            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                sc.StartModule(1);
                //Application.Run(new Position ());

                //Application.Run(new dlgPDI_VRM138());
                //Application.Run(new CBBMainEntry ());
                //Application.Run(new HandPickCards ());
                //ErrorReport reportForm = new ErrorReport();
                //reportForm.ErrorInfo = "test info";
                //reportForm.ShowDialog();
                //TestingCBB();
            }
            catch (Exception ex)
            {
                throw new Exception("Program run failed:" + ex.Message );
                //Utilities.LogInfo(ex.Message, ErrorLevel.Serious, (new StackFrame(true)).GetFileName(), (new StackFrame(true)).GetFileLineNumber());
            }

        }
    }
}
