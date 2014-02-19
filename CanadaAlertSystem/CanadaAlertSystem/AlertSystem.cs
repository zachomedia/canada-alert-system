using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

using System.IO;
using System.Net;
using System.Net.Sockets;

using System.Diagnostics;
using System.ComponentModel;

namespace ZacharySeguin.CanadaAlertSystem
{
    public delegate void AlertReceived(object sender, AlertEventArgs e);
    public delegate void AlertUpdated(object sender, AlertUpdatedEventArgs e);
    public delegate void AlertEnded(object sender, AlertEventArgs e);

    public class AlertSystem
    {
        #region Events

        /// <summary>
        /// A new alert was received.
        /// </summary>
        public EventHandler<AlertEventArgs> AlertReceived;

        /// <summary>
        /// An alert was updated.
        /// </summary>
        public EventHandler<AlertUpdatedEventArgs> AlertUpdated;

        /// <summary>
        /// An alert has ended (expired/cancelled/etc).
        /// </summary>
        public EventHandler<AlertEventArgs> AlertEnded;

        #endregion Events
        #region Properties

        /// <summary>
        /// Gets a list of active alerts.
        /// </summary>
        public List<Alert> Alerts { get; protected set; }

        /// <summary>
        /// The TcpClient used for streaming.
        /// </summary>
        public TcpClient StreamingClient { get; set; }

        /// <summary>
        /// Background worker used to monitor a TcpStream.
        /// </summary>
        private BackgroundWorker StreamingListener { get; set; }

        #endregion Properties

        #region Constructors

        /// <summary>
        /// Constructs a new AlertSystem object.
        /// </summary>
        public AlertSystem()
        {
            this.Alerts = new List<Alert>();
            this.StreamingClient = null;
            this.StreamingListener = null;
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
        /// Call the event handler informing that an alert has been updated.
        /// </summary>
        /// <param name="args"></param>
        protected void OnAlertUpdated(AlertUpdatedEventArgs args)
        {
            EventHandler<AlertUpdatedEventArgs> handler = this.AlertUpdated;

            if (handler != null)
                handler(this, args);
        }// End of OnAlertReceived method

        /// <summary>
        /// Call the event handler informing that an alert has ended.
        /// </summary>
        /// <param name="args"></param>
        protected void OnAlertEnded(AlertEventArgs args)
        {
            EventHandler<AlertEventArgs> handler = this.AlertEnded;

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
        public bool LoadFromXDocument(XDocument xDoc)
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
                Debug.WriteLine(e.Message);
                Debug.WriteLine(e.StackTrace);

                return false;
            }// End of catch
        }// End of LoadFromXDocument method

        /// <summary>
        /// Loads from an XmlSerialization file of an Alert object.
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public bool LoadFromXmlSerializationFile(string filename)
        {
            try
            {
                Alert alert = Alert.FromXmlFile(filename);

                if (alert != null)
                {
                    this.AddAlert(alert);
                    return true;
                }// End of if

                return false;
            }// End of try
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                Debug.WriteLine(e.StackTrace);

                return false;
            }// End of catch
        }// End of LoadFromXmlSerializationFile method

        /// <summary>
        /// Connects to a TCP Stream for receiving alerts.
        /// </summary>
        /// <param name="hostname">Hostname of the streaming server.</param>
        /// <param name="port">Port of the streaming server.</param>
        /// <returns></returns>
        public bool ConnectToStream(string hostname, int port)
        {
            if (this.StreamingClient != null)
                this.CloseStream();

            try
            {
                // Connect to Tcp Stream
                this.StreamingClient = new TcpClient(hostname, port);

                this.StreamingListener = new BackgroundWorker();
                this.StreamingListener.WorkerSupportsCancellation = true;
                this.StreamingListener.WorkerReportsProgress = true;

                this.StreamingListener.DoWork += this.StreamingListener_DoWork;
                this.StreamingListener.ProgressChanged += this.StreamingListener_ProgressChanged;

                this.StreamingListener.RunWorkerAsync();

                return true;
            }// End of try
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return false;
            }// End of catch
        }// End of ConnectToStream method

        private void StreamingListener_DoWork(object sender, DoWorkEventArgs args)
        {
            StreamReader reader = null;
            string alert = String.Empty;

            while (this.StreamingClient.Connected && !this.StreamingListener.CancellationPending)
            {
                try
                {
                    if (reader == null)
                        reader = new StreamReader(this.StreamingClient.GetStream());

                    char character = (char)reader.Read();
                    alert += character;

                    // If the alert contains </alert>
                    // then the full alert has been loaded
                    if (alert.Contains("</alert>"))
                    {
                        try
                        {
                            XDocument xDoc = XDocument.Parse(alert);
                            ((BackgroundWorker)sender).ReportProgress(0, xDoc);
                        }// End of try
                        catch (Exception e)
                        {
                            Debug.WriteLine(e.Message);
                        }// End of catch
                        finally
                        {
                            alert = String.Empty;
                        }// End of finally
                    }// End of if
                }// End of try
                catch (Exception e)
                {
                    Debug.WriteLine(e.Message);
                }// End of catch
            }// End of while

            Debug.WriteLine("Reading loop complete - Socket Closed");
        }// End of StreamingListener_DoWork method

        private void StreamingListener_ProgressChanged(object sender, ProgressChangedEventArgs args)
        {
            XDocument xDoc = (XDocument)args.UserState;
            if (!this.LoadFromXDocument(xDoc))
                Debug.WriteLine("Failed to load alert.");
        }// End of StreamingListener_ProgressChangedEventArgs method

        /// <summary>
        /// Closes the stream.
        /// </summary>
        /// <returns></returns>
        public bool CloseStream()
        {
            try
            {
                if (this.StreamingClient != null)
                    this.StreamingClient.Close();

                this.StreamingClient = null;

                if (this.StreamingListener != null && this.StreamingListener.IsBusy)
                    this.StreamingListener.CancelAsync();

                this.StreamingListener = null;

                return true;
            }// End of try
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return false;
            }// End of catch
        }// End of CloseStream methods

        #endregion Methods
    }// End of class
}// End of namespaces
