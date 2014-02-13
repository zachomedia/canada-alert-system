using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZacharySeguin.CanadaAlertSystem
{
    public enum AlertResponseType
    {
        /// <summary>
        /// Shelter
        /// </summary>
        Shelter,

        /// <summary>
        /// Evacuate
        /// </summary>
        Evacuate,

        /// <summary>
        /// Prepare
        /// </summary>
        Prepare,

        /// <summary>
        /// Execute
        /// </summary>
        Execute,

        /// <summary>
        /// Avoid
        /// </summary>
        Avoid,

        /// <summary>
        /// Monitor
        /// </summary>
        Monitor,

        /// <summary>
        /// Assess
        /// </summary>
        Assess,

        /// <summary>
        /// All Clear
        /// </summary>
        AllClear,

        /// <summary>
        /// None
        /// </summary>
        None,

        /// <summary>
        /// Unknown
        /// </summary>
        Unknown
    }// End of enum
}// End of namespace
