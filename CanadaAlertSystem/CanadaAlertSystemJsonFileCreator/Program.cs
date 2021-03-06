﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ZacharySeguin.CanadaAlertSystem;

namespace ZacharySeguin.CanadaAlertSystemJsonFileCreator
{
    public class Program
    {
        private const bool DEBUG = false;

        /// <summary>
        /// Alert System object.
        /// </summary>
        public static AlertSystem AlertSystem { get; protected set; }

        /// <summary>
        /// Creates a JSON file with the currently active alerts.
        /// </summary>
        /// <param name="filename"></param>
        public static void CreateJsonFile(string filename)
        {
            for (int x = 0; x < 10; ++x)
            {
                try
                {
                    using (StreamWriter file = new StreamWriter(filename))
                    {
                        file.WriteLine(JsonConvert.SerializeObject(AlertSystem.GetAlerts()));
                    }// End of using

                    break;
                }// End of try
                catch (Exception)
                {
                    System.Threading.Thread.Sleep(500);
                }// End of catch
            }// End of for
        }// End of CreateJsonFile method

        static void PrintStatistics(Alert alert)
        {
            if (DEBUG)
            {
                Console.WriteLine("\tTotal Information: {0}", alert.Information.Count);
                Console.WriteLine("\tExpired Information: {0}", alert.Information.FindAll(i => i.Expires <= DateTime.Now).Count);
            }   
        }

        static void Main(string[] args)
        {
            // Filename
            string filename = "alerts.json";
            if (args.Length != 0)
                filename = args[0];

            // Create Alert System
            // and register for events.
            AlertSystem = new AlertSystem();

            AlertSystem.AlertReceived += delegate(object sender, AlertEventArgs e)
            {
                if (DEBUG)
                {
                    Console.WriteLine("[Alert Received] {0}", e.Alert.Identifier);
                    PrintStatistics(e.Alert);
                }
                CreateJsonFile(filename);
            };

            AlertSystem.AlertUpdated += delegate(object sender, AlertUpdatedEventArgs e)
            {
                if (DEBUG)
                {
                    Console.WriteLine("[Alert Updated] {0} - {1}", e.OriginalAlert.Identifier, e.UpdatedAlert.Identifier);
                    PrintStatistics(e.UpdatedAlert);
                }
                CreateJsonFile(filename);
            };

            AlertSystem.AlertExpired += delegate(object sender, AlertEventArgs e)
            {
                if (DEBUG)
                {
                    Console.WriteLine("[Alert Expired] {0}", e.Alert.Identifier);
                    PrintStatistics(e.Alert);
                }
                CreateJsonFile(filename);
            };

            // If the file exists, load existing alerts
            //      If they have expired, they will automatically be removed.
            try
            {
                if (File.Exists(filename))
                {
                    List<Alert> alerts = new List<Alert>();

                    using (StreamReader reader = new StreamReader(filename))
                    {
                        alerts = (List<Alert>)JsonConvert.DeserializeObject(reader.ReadToEnd(), typeof(List<Alert>));
                    }// End of using

                    alerts.ForEach(a => AlertSystem.AddAlert(a));
                }// End of if
            }// End of try
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
            }// End of catch

            if (DEBUG)
            {
                Console.WriteLine(" --- LOADING OF EXISTING ALERTS COMPLETE --- ");
            }

            // Connect to the NAAD stream
            AlertSystem.ConnectToStream("streaming1.naad-adna.pelmorex.com", 8080);

            while (true)
            {
                System.Threading.Thread.Sleep(5000);
            }// End of while
        }// End of main method
    }// End of class
}// End of namespace
