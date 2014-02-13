using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZacharySeguin.CanadaAlertSystem
{
    public class AlertEventArgs : EventArgs
    {
        public Alert Alert { get; set; }

        public AlertEventArgs(Alert alert)
        {
            this.Alert = alert;
        }// End of constructor
    }// End of class
}// End of namespace
