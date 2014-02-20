using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Runtime.Serialization;

namespace ZacharySeguin.CanadaAlertSystem
{
    /// <summary>
    /// NAAD Alert Area
    /// </summary>
    [Serializable()]
    [DataContract]
    public class AlertArea
    {
        /// <summary>
        /// Gets the Description.
        /// </summary>
        [DataMember]
        public string Description { set; get; }

        /// <summary>
        /// Gets the Polygons.
        /// </summary>
        [DataMember]
        public List<string> Polygons { set; get; }

        /// <summary>
        /// Gets the Circles.
        /// </summary>
        [DataMember]
        public List<string> Circles { set; get; }

        /// <summary>
        /// Gets the Geocodes.
        /// </summary>
        [DataMember]
        public List<string> Geocodes { set; get; }

        /// <summary>
        /// Gets the altitude.
        /// </summary>
        [DataMember]
        public double Altitude { set; get; }

        /// <summary>
        /// Gets the ceiling.
        /// </summary>
        [DataMember]
        public double Ceiling { set; get; }

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

                elsTemp = xElement.Elements(ns + "polygon");
                foreach (XElement el in elsTemp)
                    area.Polygons.Add(el.Value);

                elsTemp = xElement.Elements(ns + "geocode");
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
