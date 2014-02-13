using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZacharySeguin.CanadaAlertSystem
{
    public enum AlertStatus
    {
        /// <summary>
        /// Actual Alert
        /// </summary>
        Actual,
        
        /// <summary>
        /// Exercise Alert
        /// </summary>
        Exercise,

        /// <summary>
        /// System Alert
        /// </summary>
        System,

        /// <summary>
        /// Test Alert
        /// </summary>
        Test,

        /// <summary>
        /// Draft Alert
        /// </summary>
        Draft,

        /// <summary>
        /// Unkown Alert
        /// </summary>
        Unknown
    }// End of enum
}// End of namespace
