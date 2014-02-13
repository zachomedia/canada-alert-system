using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using System.Xml.Linq;

using ZacharySeguin.CanadaAlertSystem;

namespace ZacharySeguin.CanadaAlertSystemTesting
{
    class Program
    {
        static void Main(string[] cargs)
        {
            AlertSystem system = new AlertSystem();

            system.AlertReceived += delegate(object sender, AlertEventArgs args)
            {
                Console.WriteLine("*** ALERT ****\n{0}: {1} at {2}", args.Alert.Sender, args.Alert.Identifier, args.Alert.Sent.ToString());

                foreach (AlertInfo info in args.Alert.Information)
                {
                    Console.WriteLine("\t{0} - {1} at {2}", info.SenderName, info.Event, info.Effective.ToString());
                }// End of foreach
            };
            
            XDocument xDoc = XDocument.Load(@"C:\Users\zachary\Documents\Visual Studio 2013\Projects\CanadaAlertingSystem\CanadaAlertingSystem\ExampleXML\AlertExample1.xml");
            Console.WriteLine("Success: " + system.LoadFromXmlDocument(xDoc));

            Console.Read();
        }
    }
}
