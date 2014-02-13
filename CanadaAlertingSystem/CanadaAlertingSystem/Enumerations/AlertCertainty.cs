using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZacharySeguin.CanadaAlertSystem
{
    public enum AlertCertainty
    {
        /// <summary>
        /// Observed
        /// </summary>
        Observed,

        /// <summary>
        /// Likely
        /// </summary>
        Likely,

        /// <summary>
        /// Possible
        /// </summary>
        Possible,

        /// <summary>
        /// Unlikely
        /// </summary>
        Unlikely,

        /// <summary>
        /// Unknown
        /// </summary>
        Unknown
    }// End of AlertCertainty
}// End of namespace
