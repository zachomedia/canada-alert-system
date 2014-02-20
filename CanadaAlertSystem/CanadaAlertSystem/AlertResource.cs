using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.Runtime.Serialization;

namespace ZacharySeguin.CanadaAlertSystem
{
    /// <summary>
    /// NAAD Alert Resource
    /// </summary>
    [Serializable()]
    [DataContract]
    public class AlertResource
    {
        /// <summary>
        /// Gets the description.
        /// </summary>
        [DataMember]
        public string Description { set; get; }

        /// <summary>
        /// Gets the Mime Type.
        /// </summary>
        [DataMember]
        public string MimeType { set; get; }

        /// <summary>
        /// Gets the size.
        /// </summary>
        [DataMember]
        public int Size { set; get; }

        /// <summary>
        /// Gets the Uniform Resource Locator.
        /// </summary>
        [XmlIgnore]
        [IgnoreDataMember]
        public Uri Uri { set; get; }

        [XmlElement("Uri")]
        [DataMember(Name = "Uri")]
        public string _Uri
        {
            set
            {
                if (!String.IsNullOrEmpty(value))
                    this.Uri = new Uri(value);
            }// End of set

            get
            {
                if (this.Uri == null)
                    return String.Empty;

                return this.Uri.ToString();
            }// End of get
        }// End of _Uri method

        /// <summary>
        /// Gets the deref Uniform Resource Locator.
        /// </summary>
        [XmlIgnore]
        [IgnoreDataMember]
        public Uri DerefUri { set; get; }

        [XmlElement("DerefUri")]
        [DataMember(Name = "DerefUri")]
        public string _DerefUri
        {
            set
            {
                if (!String.IsNullOrEmpty(value))
                    this.DerefUri = new Uri(value);
            }// End of set

            get
            {
                if (this.DerefUri == null)
                    return String.Empty;

                return this.DerefUri.ToString();
            }// End of get
        }// End of _DerefUri property

        /// <summary>
        /// Gets the digest.
        /// </summary>
        [DataMember]
        public string Digest { set; get; }

        /// <summary>
        /// Constructs a default AlertResource object.
        /// </summary>
        public AlertResource()
        {
            this.Init();
        }// End of constructor method

        /// <summary>
        /// Initializes the AlertResource object.
        /// </summary>
        private void Init()
        {
            this.Description = String.Empty;
            this.MimeType = String.Empty;
            this.Size = 0;
            this.Uri = null;
            this.DerefUri = null;
            this.Digest = String.Empty;
        }// End of Init method

        /// <summary>
        /// Load from XML document.
        /// </summary>
        /// <param name="xDoc"></param>
        /// <returns>Whether or not loading from XML was successful.</returns>
        public static bool FromXmlElement(XElement xElement, out AlertResource outResource)
        {
            try
            {
                // Declare variable
                XNamespace ns = xElement.Name.Namespace;

                XElement elTemp;

                // Initialize info object
                AlertResource resource = new AlertResource();

                elTemp = xElement.Element(ns + "resourceDesc");
                if (elTemp != null)
                    resource.Description = elTemp.Value;

                elTemp = xElement.Element(ns + "mimeType");
                if (elTemp != null)
                    resource.MimeType = elTemp.Value;

                elTemp = xElement.Element(ns + "uri");
                if (elTemp != null)
                {
                    Uri uri;
                    if (Uri.TryCreate(elTemp.Value, UriKind.RelativeOrAbsolute, out uri))
                        resource.Uri = uri;
                }// End of if

                elTemp = xElement.Element(ns + "derefUri");
                if (elTemp != null)
                {
                    Uri uri;
                    if (Uri.TryCreate(elTemp.Value, UriKind.RelativeOrAbsolute, out uri))
                        resource.DerefUri = uri;
                }// End of if

                elTemp = xElement.Element(ns + "digest");
                if (elTemp != null)
                    resource.Digest = elTemp.Value;
                
                outResource = resource;
                return true;
            }// End of try
            catch (Exception)
            {
                outResource = null;
                return false;
            }// End of catch
        }// End of FromXmlElement method
    }// End of class
}// End of namespace
