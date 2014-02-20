using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZacharySeguin.CanadaAlertSystem
{
    /// <summary>
    /// Event arguments when an event is updated.
    /// </summary>
    public class AlertUpdatedEventArgs : EventArgs
    {
        /// <summary>
        /// Gets t original (old) alert.
        /// </summary>
        public Alert OriginalAlert { get; private set; }

        /// <summary>
        /// Gets the updated alert.
        /// </summary>
        public Alert UpdatedAlert { get; private set; }

        /// <summary>
        /// Constructs a new Alert Updated event args object.
        /// </summary>
        /// <param name="originalAlert">The original (old) alert.</param>
        /// <param name="updatedAlert">The new alert.</param>
        public AlertUpdatedEventArgs(Alert originalAlert, Alert updatedAlert)
        {
            this.OriginalAlert = originalAlert;
            this.UpdatedAlert = updatedAlert;
        }// End of constructor
    }// End of class
}// End of namespace
