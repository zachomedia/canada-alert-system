using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace ZacharySeguin.CanadaAlertSystem
{
    /// <summary>
    /// NAAD Alert Info
    /// </summary>
    [Serializable()]
    public class AlertInfo
    {
        /// <summary>
        /// Gets the language.
        /// </summary>
        public string Language { set; get; }

        /// <summary>
        /// Gets the Alert Categories.
        /// </summary>
        public List<AlertCategory> Categories { set; get; }

        /// <summary>
        /// Gets the event.
        /// </summary>
        public string Event { set; get; }

        /// <summary>
        /// Gets the Response Types.
        /// </summary>
        public List<AlertResponseType> ResponseTypes { set; get; }

        /// <summary>
        /// Gets the AlertUrgency.
        /// </summary>
        public AlertUrgency Urgency { set; get; }

        /// <summary>
        /// Gets the AlertSeverity.
        /// </summary>
        public AlertSeverity Severity { set; get; }

        /// <summary>
        /// Gets the AlertCertainty.
        /// </summary>
        public AlertCertainty Certainty { set; get; }

        /// <summary>
        /// Gets the audience.
        /// </summary>
        public string Audience { set; get; }

        /// <summary>
        /// Gets the event codes.
        /// </summary>
        public List<string> EventCodes { set; get; }

        /// <summary>
        /// Gets the effective date/time.
        /// </summary>
        public DateTime Effective { set; get; }

        /// <summary>
        /// Gets the onset date/time.
        /// </summary>
        public DateTime OnSet { set; get; }

        /// <summary>
        /// Gets the expiry date/time.
        /// </summary>
        public DateTime Expires { set; get; }

        /// <summary>
        /// Gets the sender name.
        /// </summary>
        public string SenderName { set; get; }

        /// <summary>
        /// Gets the headline.
        /// </summary>
        public string Headline { set; get; }

        /// <summary>
        /// Gets the description.
        /// </summary>
        public string Description { set; get; }

        /// <summary>
        /// Gets the instruction.
        /// </summary>
        public string Instruction { set; get; }

        /// <summary>
        /// Gets the web URI.
        /// </summary>
        [XmlIgnore]
        public Uri Web { set; get; }

        [XmlElement("Web")]
        public string _Web
        {
            set
            {
                this.Web = new Uri(value);
            }// End of set

            get
            {
                return this.Web.ToString();
            }// End of get
        }// End of _Web method

        /// <summary>
        /// Gets the contact.
        /// </summary>
        public string Contact { set; get; }

        /// <summary>
        /// Gets the parameters.
        /// </summary>
        public List<string> Parameters { set; get; }

        /// <summary>
        /// Gets the resources.
        /// </summary>
        public List<AlertResource> Resources { set; get; }

        /// <summary>
        /// Gets the areas.
        /// </summary>
        public List<AlertArea> Areas { set; get; }

        /// <summary>
        /// Constructs a default AlertInfo object.
        /// </summary
        public AlertInfo()
        {
            this.Init();
        }// End of constructor method

        /// <summary>
        /// Intializes the AlertInfo object.
        /// </summary>
        private void Init()
        {
            this.Language = String.Empty;
            this.Categories = new List<AlertCategory>();
            this.Event = String.Empty;
            this.ResponseTypes = new List<AlertResponseType>();
            this.Urgency = AlertUrgency.Unknown;
            this.Severity = AlertSeverity.Unknown;
            this.Certainty = AlertCertainty.Unknown;
            this.Audience = String.Empty;
            this.EventCodes = new List<string>();
            this.Effective = DateTime.Now;
            this.OnSet = DateTime.Now;
            this.Expires = DateTime.Now;
            this.SenderName = String.Empty;
            this.Headline = String.Empty;
            this.Description = String.Empty;
            this.Instruction = String.Empty;
            this.Web = null;
            this.Contact = String.Empty;
            this.Parameters = new List<string>();
            this.Resources = new List<AlertResource>();
            this.Areas = new List<AlertArea>();
        }// End of Init method

        /// <summary>
        /// Load from XML document.
        /// </summary>
        /// <param name="xDoc"></param>
        /// <returns>Whether or not loading from XML was successful.</returns>
        public static bool FromXmlElement(XElement xElement, out AlertInfo outInfo)
        {
            try
            {
                // Declare variable
                XNamespace ns = xElement.Name.Namespace;

                IEnumerable<XElement> elsTemp;
                XElement elTemp;

                // Initialize info object
                AlertInfo info = new AlertInfo();

                elTemp = xElement.Element(ns + "language");
                if (elTemp != null)
                    info.Language = elTemp.Value;

                elsTemp = xElement.Elements(ns + "category");
                foreach (XElement el in elsTemp)
                {
                    AlertCategory category = AlertCategory.Unknown;
                    if (Enum.TryParse<AlertCategory>(elTemp.Value, out category))
                        info.Categories.Add(category);
                }// End of foreach

                elTemp = xElement.Element(ns + "event");
                if (elTemp != null)
                    info.Event = elTemp.Value;

                elsTemp = xElement.Elements(ns + "responseType");
                foreach (XElement el in elsTemp)
                {
                    AlertResponseType responseType = AlertResponseType.Unknown;
                    if (Enum.TryParse<AlertResponseType>(elTemp.Value, out responseType))
                        info.ResponseTypes.Add(responseType);
                }// End of foreach

                elTemp = xElement.Element(ns + "urgency");
                if (elTemp != null)
                {
                    AlertUrgency urgency = AlertUrgency.Unknown;
                    if (Enum.TryParse<AlertUrgency>(elTemp.Value, out urgency))
                        info.Urgency = urgency;
                }// End of if

                elTemp = xElement.Element(ns + "severity");
                if (elTemp != null)
                {
                    AlertSeverity severity = AlertSeverity.Unknown;
                    if (Enum.TryParse<AlertSeverity>(elTemp.Value, out severity))
                        info.Severity = severity;
                }// End of if

                elTemp = xElement.Element(ns + "certainty");
                if (elTemp != null)
                {
                    AlertCertainty certainty = AlertCertainty.Unknown;
                    if (Enum.TryParse<AlertCertainty>(elTemp.Value, out certainty))
                        info.Certainty = certainty;
                }// End of if

                elTemp = xElement.Element(ns + "audience");
                if (elTemp != null)
                    info.Audience = elTemp.Value;

                elsTemp = xElement.Elements(ns + "eventCode");
                foreach (XElement el in elsTemp)
                    info.EventCodes.Add(el.Value);

                elTemp = xElement.Element(ns + "effective");
                if (elTemp != null)
                {
                    DateTime dt;
                    if (DateTime.TryParse(elTemp.Value, out dt))
                        info.Effective = dt;
                }// End of if

                elTemp = xElement.Element(ns + "onset");
                if (elTemp != null)
                {
                    DateTime dt;
                    if (DateTime.TryParse(elTemp.Value, out dt))
                        info.OnSet = dt;
                }// End of if

                elTemp = xElement.Element(ns + "expires");
                if (elTemp != null)
                {
                    DateTime dt;
                    if (DateTime.TryParse(elTemp.Value, out dt))
                        info.Expires = dt;
                }// End of if

                elTemp = xElement.Element(ns + "senderName");
                if (elTemp != null)
                    info.SenderName = elTemp.Value;

                elTemp = xElement.Element(ns + "headline");
                if (elTemp != null)
                    info.Headline = elTemp.Value;

                elTemp = xElement.Element(ns + "description");
                if (elTemp != null)
                    info.Description = elTemp.Value;

                elTemp = xElement.Element(ns + "instruction");
                if (elTemp != null)
                    info.Instruction = elTemp.Value;

                elTemp = xElement.Element(ns + "web");
                if (elTemp != null)
                {
                    Uri uri;
                    if (Uri.TryCreate(elTemp.Value, UriKind.RelativeOrAbsolute, out uri))
                        info.Web = uri;
                }// End of if

                elTemp = xElement.Element(ns + "contact");
                if (elTemp != null)
                    info.Contact = elTemp.Value;

                elsTemp = xElement.Elements(ns + "parameter");
                foreach (XElement el in elsTemp)
                    info.Parameters.Add(el.Value);

                elsTemp = xElement.Elements(ns + "resource");
                foreach (XElement el in elsTemp)
                {
                    AlertResource resource;
                    if (AlertResource.FromXmlElement(el, out resource))
                        info.Resources.Add(resource);
                }// End of foreach

                elsTemp = xElement.Elements(ns + "area");
                foreach (XElement el in elsTemp)
                {
                    AlertArea area;
                    if (AlertArea.FromXmlElement(el, out area))
                        info.Areas.Add(area);
                }// End of foreach

                outInfo = info;
                return true;
            }// End of try
            catch (Exception)
            {
                outInfo = null;
                return false;
            }// End of catch
        }// End of FromXmlElement method
    }// End of class
}// End of namespace
