﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using System.IO;

namespace ZacharySeguin.CanadaAlertSystem
{
    /// <summary>
    /// An alert issued by the National Alert Aggregation and Dissemation System.
    /// </summary>
    [Serializable()]
    [DataContract]
    public class Alert
    {
        /// <summary>
        /// Gets the identifier.
        /// </summary>
        [DataMember]
        public string Identifier { set; get; }

        /// <summary>
        /// Gets the sender.
        /// </summary>
        [DataMember]
        public string Sender { set; get; }

        /// <summary>
        /// Gets the DateTime the alert was sent.
        /// </summary>
        [DataMember]
        public DateTime Sent { set; get; }

        /// <summary>
        /// Gets the AlertStatus.
        /// </summary>
        [IgnoreDataMember]
        public AlertStatus Status { set; get; }

        /// <summary>
        /// Gets the AlertStatus as a string (for serialization).
        /// </summary>
        [DataMember(Name = "Status")]
        private string _Status
        {
            get
            {
                return this.Status.ToString();
            }// End of get
            set
            {
                AlertStatus status = AlertStatus.Unknown;
                Enum.TryParse<AlertStatus>(value, out status);

                this.Status = status;
            }// End of set
        }// End of _Status property

        /// <summary>
        /// Gets the AlertType.
        /// </summary>
        [IgnoreDataMember]
        public AlertType Type { set; get; }

        /// <summary>
        /// Gets the AlertType as a string (for serialization).
        /// </summary>
        [DataMember(Name = "Type")]
        private string _Type
        {
            get
            {
                return this.Type.ToString();
            }// End of get
            set
            {
                AlertType type = AlertType.Unknown;
                Enum.TryParse<AlertType>(value, out type);

                this.Type = type;
            }// End of set
        }// End of _Type property

        /// <summary>
        /// Gets the source.
        /// </summary>
        [DataMember]
        public string Source { set; get; }

        /// <summary>
        /// Gets the AlertScope.
        /// </summary>
        [IgnoreDataMember]
        public AlertScope Scope { set; get; }

        /// <summary>
        /// Gets the AlertScope as a string (for serialization).
        /// </summary>
        [DataMember(Name = "Scope")]
        private string _Scope
        {
            get
            {
                return this.Scope.ToString();
            }// End of get
            set
            {
                AlertScope scope = AlertScope.Unknown;
                Enum.TryParse<AlertScope>(value, out scope);

                this.Scope = scope;
            }// End of set
        }// End of _Scope property

        /// <summary>
        /// Gets the restrictions.
        /// </summary>
        [DataMember]
        public string Restriction { set; get; }

        /// <summary>
        /// Gets the addressess.
        /// </summary>
        [DataMember]
        public string Addresses { set; get; }

        /// <summary>
        /// Gets the codes.
        /// </summary>
        [DataMember]
        public List<string> Codes { set; get; }

        /// <summary>
        /// Gets the note.
        /// </summary>
        [DataMember]
        public string Note { set; get; }

        /// <summary>
        /// Gets the references.
        /// </summary>
        [DataMember]
        public string References { set; get; }

        /// <summary>
        /// Gets the incidents.
        /// </summary>
        [DataMember]
        public string Incidents { set; get; }

        /// <summary>
        /// Gets the Alert Informations.
        /// </summary>
        [DataMember]
        public List<AlertInfo> Information { set; get; }

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

        /// <summary>
        /// Writes the alert to xml.
        /// </summary>
        /// <param name="file"></param>
        public void ToXmlFile(string file)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(Alert));

            using (StreamWriter fileWriter = new StreamWriter(file))
            {
                xmlSerializer.Serialize(fileWriter, this);
            }// End of using
        }// End of ToXmlFile method

        /// <summary>
        /// Reconstructs alert object from XML.
        /// </summary>
        /// <param name="file"></param>
        public static Alert FromXmlFile(string file)
        {
            Alert alert = null;

            XmlSerializer xmlSerializer = new XmlSerializer(typeof(Alert));

            using (StreamReader fileReader = new StreamReader(file))
            {
                alert = (Alert)xmlSerializer.Deserialize(fileReader);
            }// End of using

            return alert;
        }// End of ToXmlFile method
    }// End of class
}// End of namespace
