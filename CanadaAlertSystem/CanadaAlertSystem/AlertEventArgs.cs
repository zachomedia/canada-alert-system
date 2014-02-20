using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZacharySeguin.CanadaAlertSystem
{
    /// <summary>
    /// Alert Event arguments.
    /// </summary>
    public class AlertEventArgs : EventArgs
    {
        /// <summary>
        /// Gets the alert object.
        /// </summary>
        public Alert Alert { get; protected set; }

        /// <summary>
        /// Constructs a new Alert event args object.
        /// </summary>
        /// <param name="alert">The alert object.</param>
        public AlertEventArgs(Alert alert)
        {
            this.Alert = alert;
        }// End of constructor
    }// End of class
}// End of namespace
