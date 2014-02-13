using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZacharySeguin.CanadaAlertSystem
{
    public enum AlertCategory
    {
        /// <summary>
        /// Geological
        /// </summary>
        Geo,

        /// <summary>
        /// Meteorology
        /// </summary>
        Met,

        /// <summary>
        /// Safety
        /// </summary>
        Safety,

        /// <summary>
        /// Security
        /// </summary>
        Security,

        /// <summary>
        /// Rescue
        /// </summary>
        Rescue,

        /// <summary>
        /// Fire
        /// </summary>
        Fire,

        /// <summary>
        /// Health
        /// </summary>
        Health,

        /// <summary>
        /// Environmental
        /// </summary>
        Env,

        /// <summary>
        /// Transport
        /// </summary>
        Transport,

        /// <summary>
        /// Infrastructure
        /// </summary>
        Infra,

        /// <summary>
        /// Chemical, Biological, Radiological, Nuclear, Explosive
        /// </summary>
        CBRNE,

        /// <summary>
        /// Other
        /// </summary>
        Other,

        /// <summary>
        /// Unknown
        /// </summary>
        Unknown
    }// End of enum
}// End of namespace
