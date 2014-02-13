using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ZacharySeguin.CanadaAlertSystem
{
    /// <summary>
    /// An alert issued by the National Alert Aggregation and Dissemation System.
    /// </summary>
    public class Alert
    {
        /// <summary>
        /// Gets the identifier.
        /// </summary>
        public string Identifier { protected set; get; }

        /// <summary>
        /// Gets the sender.
        /// </summary>
        public string Sender { protected set; get; }

        /// <summary>
        /// Gets the DateTime the alert was sent.
        /// </summary>
        public DateTime Sent { protected set; get; }

        /// <summary>
        /// Gets the AlertStatus.
        /// </summary>
        public AlertStatus Status { protected set; get; }

        /// <summary>
        /// Gets the AlertType.
        /// </summary>
        public AlertType Type { protected set; get; }

        /// <summary>
        /// Gets the source.
        /// </summary>
        public string Source { protected set; get; }

        /// <summary>
        /// Gets the AlertScope.
        /// </summary>
        public AlertScope Scope { protected set; get; }

        /// <summary>
        /// Gets the restrictions.
        /// </summary>
        public string Restriction { protected set; get; }

        /// <summary>
        /// Gets the addressess.
        /// </summary>
        public string Addresses { protected set; get; }

        /// <summary>
        /// Gets the codes.
        /// </summary>
        public List<string> Codes { protected set; get; }

        /// <summary>
        /// Gets the note.
        /// </summary>
        public string Note { protected set; get; }

        /// <summary>
        /// Gets the references.
        /// </summary>
        public string References { protected set; get; }

        /// <summary>
        /// Gets the incidents.
        /// </summary>
        public string Incidents { protected set; get; }

        /// <summary>
        /// Gets the Alert Informations.
        /// </summary>
        public List<AlertInfo> Information { protected set; get; }

        /// <summary>
        /// Constructs a new Alert object.
        /// </summary>
        public Alert()
        {
            this.Init();
        }// End of constructor method

        /// <summary>
        /// Initializes the Alert object, giving properties a default value.
        /// </summary>
        private void Init()
        {
            this.Identifier = String.Empty;
            this.Sender = String.Empty;
            this.Sent = DateTime.Now;

            this.Status = AlertStatus.Unknown;
            this.Type = AlertType.Unknown;
            this.Source = String.Empty;
            this.Scope = AlertScope.Unknown;
            
            this.Restriction = String.Empty;
            this.Addresses = String.Empty;
            this.Codes = new List<string>();
            this.Note = String.Empty;
            this.References = String.Empty;
            this.Incidents = String.Empty;

            this.Information = new List<AlertInfo>();
        }// End of Init method

        /// <summary>
        /// Load from XML document.
        /// </summary>
        /// <param name="xDoc"></param>
        /// <returns>Whether or not loading from XML was successful.</returns>
        public static bool FromXmlElement(XElement xElement, out Alert outAlert)
        {
            try
            {
                // Declare variable
                XNamespace ns = xElement.Name.Namespace;

                IEnumerable<XElement> elsTemp;
                XElement elTemp;

                // Initialize alert object
                Alert alert = new Alert();

                elTemp = xElement.Element(ns + "identifier");
                if (elTemp != null)
                    alert.Identifier = elTemp.Value;

                elTemp = xElement.Element(ns + "sender");
                if (elTemp != null)
                    alert.Sender = elTemp.Value;

                elTemp = xElement.Element(ns + "sent");
                if (elTemp != null)
                {
                    DateTime dtSent;
                    if (DateTime.TryParse(elTemp.Value, out dtSent))
                        alert.Sent = dtSent;
                }// End of if

                elTemp = xElement.Element(ns + "status");
                if (elTemp != null)
                {
                    AlertStatus status = AlertStatus.Unknown;
                    if (Enum.TryParse<AlertStatus>(elTemp.Value, out status))
                        alert.Status = status;
                }// End of if

                elTemp = xElement.Element(ns + "msgType");
                if (elTemp != null)
                {
                    AlertType type = AlertType.Unknown;
                    if (Enum.TryParse<AlertType>(elTemp.Value, out type))
                        alert.Type = type;
                }// End of if

                elTemp = xElement.Element(ns + "source");
                if (elTemp != null)
                    alert.Source = elTemp.Value;

                elTemp = xElement.Element(ns + "scope");
                if (elTemp != null)
                {
                    AlertScope scope = AlertScope.Unknown;
                    if (Enum.TryParse<AlertScope>(elTemp.Value, out scope))
                        alert.Scope = scope;
                }// End of if

                elTemp = xElement.Element(ns + "restriction");
                if (elTemp != null)
                    alert.Restriction = elTemp.Value;

                elTemp = xElement.Element(ns + "addresses");
                if (elTemp != null)
                    alert.Addresses = elTemp.Value;

                elsTemp = xElement.Elements(ns + "code");
                foreach (XElement el in elsTemp)
                    alert.Codes.Add(el.Value);

                elTemp = xElement.Element(ns + "note");
                if (elTemp != null)
                    alert.Note = elTemp.Value;

                elTemp = xElement.Element(ns + "references");
                if (elTemp != null)
                    alert.References = elTemp.Value;

                elTemp = xElement.Element(ns + "incidents");
                if (elTemp != null)
                    alert.Incidents = elTemp.Value;

                elsTemp = xElement.Elements(ns + "info");
                foreach (XElement el in elsTemp)
                {
                    AlertInfo info;
                    if (AlertInfo.FromXmlElement(el, out info))
                        alert.Information.Add(info);
                }// End of foreach

                // Set the output
                outAlert = alert;
                return true;
            }// End of try
            catch (Exception)
            {
                outAlert = null;
                return false;
            }// End of catch
        }// End of FromXmlDocument method
    }// End of class
}// End of namespace
