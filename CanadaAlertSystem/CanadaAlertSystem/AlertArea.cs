using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

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

        /// <summary>
        /// Load from XML document.
        /// </summary>
        /// <param name="xDoc"></param>
        /// <returns>Whether or not loading from XML was successful.</returns>
        public static bool FromXmlElement(XElement xElement, out AlertArea outArea)
        {
            try
            {
                // Declare variable
                XNamespace ns = xElement.Name.Namespace;

                IEnumerable<XElement> elsTemp;
                XElement elTemp;

                // Initialize info object
                AlertArea area = new AlertArea();

                elTemp = xElement.Element(ns + "areaDesc");
                if (elTemp != null)
                    area.Description = elTemp.Value;

                elsTemp = xElement.Elements(ns + "polgygon");
                foreach (XElement el in elsTemp)
                    area.Polygons.Add(el.Value);

                elsTemp = xElement.Elements(ns + "geocde");
                foreach (XElement el in elsTemp)
                {
                    elTemp = el.Element(el.Name.Namespace + "value");

                    if (elTemp != null)
                        area.Geocodes.Add(elTemp.Value);
                }// End of foreach

                elTemp = xElement.Element(ns + "altitude");
                if (elTemp != null)
                {
                    double value = 0.0;
                    if (double.TryParse(elTemp.Value, out value))
                        area.Altitude = value;
                }// End of if

                elTemp = xElement.Element(ns + "ceiling");
                if (elTemp != null)
                {
                    double value = 0.0;
                    if (double.TryParse(elTemp.Value, out value))
                        area.Ceiling = value;
                }// End of if

                outArea = area;
                return true;
            }// End of try
            catch (Exception)
            {
                outArea = null;
                return false;
            }// End of catch
        }// End of FromXmlElement method
    }// End of class
}// End of namespace
