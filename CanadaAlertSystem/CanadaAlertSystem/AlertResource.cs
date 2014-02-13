using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ZacharySeguin.CanadaAlertSystem
{
    /// <summary>
    /// NAAD Alert Resource
    /// </summary>
    public class AlertResource
    {
        /// <summary>
        /// Gets the description.
        /// </summary>
        public string Description { protected set; get; }

        /// <summary>
        /// Gets the Mime Type.
        /// </summary>
        public string MimeType { protected set; get; }

        /// <summary>
        /// Gets the size.
        /// </summary>
        public int Size { protected set; get; }

        /// <summary>
        /// Gets the Uniform Resource Locator.
        /// </summary>
        public Uri Uri { protected set; get; }

        /// <summary>
        /// Gets the deref Uniform Resource Locator.
        /// </summary>
        public Uri DerefUri { protected set; get; }

        /// <summary>
        /// Gets the digest.
        /// </summary>
        public string Digest { protected set; get; }

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
