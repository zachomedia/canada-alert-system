using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }// End of class
}// End of namespace
