using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json;

using ZacharySeguin.CanadaAlertSystem;
using System.Diagnostics;

namespace CanadaAlertSystemApi.Controllers
{
    public class AlertsController : ApiController
    {
        /// <summary>
        /// Alert system object.
        /// </summary>
        private static AlertSystem AlertSystem = new AlertSystem();

        /// <summary>
        /// Holds if the alert system has been configured.
        /// </summary>
        private static bool AlertSystemConfigured = false;

        /// <summary>
        /// Setups the Alerts Controller.
        /// </summary>
        public AlertsController()
        {
            if (!AlertSystemConfigured)
            {
                AlertSystem.ConnectToStream("streaming1.naad-adna.pelmorex.com", 8080);
                AlertSystemConfigured = true;
            }// End of if
        }// End of constructor method

        /// <summary>
        /// Returns all alerts.
        /// </summary>
        /// <returns>All alerts.</returns>
        public IEnumerable<Alert> GetAlerts()
        {
            return AlertSystem.GetAlerts();
        }// End of GetAlerts method
    }// End of class
}// End of namespace
