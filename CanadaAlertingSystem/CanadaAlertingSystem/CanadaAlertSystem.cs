using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

using System.Diagnostics;

namespace ZacharySeguin.CanadaAlertSystem
{
    public delegate void AlertReceived(object sender, AlertEventArgs a);

    public class AlertSystem
    {
        #region Events

        /// <summary>
        /// A new alert was received.
        /// </summary>
        public EventHandler<AlertEventArgs> AlertReceived;

        #endregion Events
        #region Properties

        public List<Alert> Alerts { get; protected set; }

        #endregion Properties

        #region Constructors
        public AlertSystem()
        {
            this.Alerts = new List<Alert>();
        }// End of constructor method

        #endregion Constructors
        #region Methods

        /// <summary>
        /// Call the event handler informing that an alert was received.
        /// </summary>
        /// <param name="args"></param>
        protected void OnAlertReceived(AlertEventArgs args)
        {
            EventHandler<AlertEventArgs> handler = this.AlertReceived;

            if (handler != null)
                handler(this, args);
        }// End of OnAlertReceived method

        /// <summary>
        /// Adds an alert.
        /// </summary>
        /// <param name="alert"></param>
        public void AddAlert(Alert alert)
        {
            this.Alerts.Add(alert);
            this.OnAlertReceived(new AlertEventArgs(alert));
        }// End of AddAlert method

        /// <summary>
        /// Loads alerts from an XmlDocument.
        /// </summary>
        /// <param name="xDoc">Document to load</param>
        /// <returns>true if succesfully read, false otherwise.</returns>
        public bool LoadFromXmlDocument(XDocument xDoc)
        {
            try
            {
                Alert alert = null;
                bool success = Alert.FromXmlElement(xDoc.Root, out alert);

                if (success)
                    this.AddAlert(alert);

                return success;
            }// End of try
            catch (Exception e)
            {
                Debug.WriteLine(e);
                return false;
            }// End of catch
        }// End of LoadFromXml method

        #endregion Methods
    }// End of class
}// End of namespaces
