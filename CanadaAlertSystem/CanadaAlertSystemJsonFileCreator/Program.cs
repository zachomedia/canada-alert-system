using System;
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
            using (StreamWriter file = new StreamWriter(filename))
            {
                file.WriteLine(JsonConvert.SerializeObject(AlertSystem.GetAlerts()));
            }// End of using
        }// End of CreateJsonFile method

        static void Main(string[] args)
        {
            string filename = "alerts.json";
            if (args.Length != 0)
                filename = args[0];

            AlertSystem = new AlertSystem();

            AlertSystem.AlertReceived += delegate(object sender, AlertEventArgs e)
            {
                CreateJsonFile(filename);
            };

            AlertSystem.AlertUpdated += delegate(object sender, AlertUpdatedEventArgs e)
            {
                CreateJsonFile(filename);
            };

            AlertSystem.AlertExpired += delegate(object sender, AlertEventArgs e)
            {
                CreateJsonFile(filename);
            };

            // FOR TESTING ONLY
            foreach (string file in System.IO.Directory.GetFiles(@"C:\TEMP\Alerts\"))
                AlertSystem.LoadFromXmlSerializationFile(file);

            AlertSystem.ConnectToStream("streaming1.naad-adna.pelmorex.com", 8080);

            while (true)
                System.Threading.Thread.Sleep(5000);
        }// End of main method
    }// End of class
}// End of namespace
