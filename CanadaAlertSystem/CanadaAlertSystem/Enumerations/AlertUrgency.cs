using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZacharySeguin.CanadaAlertSystem
{
    public enum AlertUrgency
    {
        /// <summary>
        /// Immediate
        /// </summary>
        Immediate,

        /// <summary>
        /// Expected
        /// </summary>
        Expected,

        /// <summary>
        /// Future
        /// </summary>
        Future,

        /// <summary>
        /// Past
        /// </summary>
        Past,

        /// <summary>
        /// Unkonwn
        /// </summary>
        Unknown
    }// End of enum
}// End of namespace
