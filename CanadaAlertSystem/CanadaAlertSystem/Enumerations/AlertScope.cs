using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZacharySeguin.CanadaAlertSystem
{
    public enum AlertScope
    {
        /// <summary>
        /// Alert is public.
        /// </summary>
        Public,

        /// <summary>
        /// Alert is restricted.
        /// </summary>
        Restricted,

        /// <summary>
        /// Alert is private.
        /// </summary>
        Private,

        /// <summary>
        /// Alert scope is unkown.
        /// </summary>
        Unknown
    }// End of enum
}// End of namespace
