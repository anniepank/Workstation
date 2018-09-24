using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace WorkstationService
{
    public partial class Service1 : ServiceBase
    {
        Timer timer = new Timer(); // name space(using System.Timers;) 
        public Service1()
        {
            CanPauseAndContinue = true;
            CanHandleSessionChangeEvent = true;
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            EventLog.WriteEntry($"Service is started at {DateTime.Now}");
            timer.Elapsed += new ElapsedEventHandler(OnElapsedTime);
            timer.Interval = 5000; //number in milisecinds  
            timer.Enabled = true;
        }

        protected override void OnStop()
        {
            EventLog.WriteEntry($"Service is stopped at {DateTime.Now}");
        }


        private void OnElapsedTime(object source, ElapsedEventArgs e)
        {
            // EventLog.WriteEntry($"Service is recall at {DateTime.Now}");
        }

        protected override void OnSessionChange(SessionChangeDescription changeDescription)
        {
            switch (changeDescription.Reason)
            {
                case SessionChangeReason.SessionLogon:
                    EventLog.WriteEntry("SimpleService.OnSessionChange: Logon");
                    EventLog.WriteEntry("NAME" +  System.Security.Principal.WindowsIdentity.GetCurrent().Name);
                    break;

                case SessionChangeReason.SessionLogoff:
                    EventLog.WriteEntry("SimpleService.OnSessionChange Logoff");
                    break;
            }
        }
    }
}
