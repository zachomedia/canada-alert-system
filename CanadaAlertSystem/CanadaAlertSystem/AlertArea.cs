using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZacharySeguin.CanadaAlertSystem
{
    /// <summary>
    /// NAAD Alert Area
    /// </summary>
    public class AlertArea
    {
        /// <summary>
        /// Gets the Description.
        /// </summary>
        public string Description { protected set; get; }

        /// <summary>
        /// Gets the Polygons.
        /// </summary>
        public List<string> Polygons { protected set; get; }

        /// <summary>
        /// Gets the Circles.
        /// </summary>
        public List<string> Circles { protected set; get; }

        /// <summary>
        /// Gets the Geocodes.
        /// </summary>
        public List<string> Geocodes { protected set; get; }

        /// <summary>
        /// Gets the altitude.
        /// </summary>
        public double Altitude { protected set; get; }

        /// <summary>
        /// Gets the ceiling.
        /// </summary>
        public double Ceiling { protected set; get; }

        /// <summary>
        /// Constructs a default Alert Area.
        /// </summary>
        public AlertArea()
        {
            this.Init();
        }// End of constructor method

        /// <summary>
        /// Initializes the Alert Area.
        /// </summary>
        private void Init()
        {
            this.Description = String.Empty;
            this.Polygons = new List<string>();
            this.Circles = new List<string>();
            this.Geocodes = new List<string>();
            this.Altitude = 0.0;
            this.Ceiling = 0.0;
        }// End of Init method
    }// End of class
}// End of namespace
