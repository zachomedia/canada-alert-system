using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZacharySeguin.CanadaAlertSystem
{
    public enum AlertType
    {
        /// <summary>
        /// Alert (New Alert)
        /// </summary>
        Alert,

        /// <summary>
        /// Update to an existing alert
        /// </summary>
        Update,

        /// <summary>
        /// Cancel an existing alert
        /// </summary>
        Cancel,

        /// <summary>
        /// Ack alert type.
        /// </summary>
        Ack,

        /// <summary>
        /// Error alert.
        /// </summary>
        Error,

        /// <summary>
        /// Unknown alert type.
        /// </summary>
        Unknown
    }// End of enum
}// End of enum
