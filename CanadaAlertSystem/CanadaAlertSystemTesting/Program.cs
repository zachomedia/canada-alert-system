using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using System.Net.Sockets;
using System.Net;
using System.Xml.Linq;

using ZacharySeguin.CanadaAlertSystem;
using System.Diagnostics;

namespace ZacharySeguin.CanadaAlertSystemTesting
{
    class Program
    {
        static void Main(string[] cargs)
        {
            AlertSystem system = new AlertSystem();

            system.AlertReceived += delegate(object sender, AlertEventArgs args)
            {
                args.Alert.ToXmlFile(@"C:\TEMP\ALERTS\alert-" + args.Alert.Identifier + ".xml");

                if (args.Alert.Status != AlertStatus.Actual && args.Alert.Status != AlertStatus.Exercise)
                {
                    Debug.WriteLine("Non actual/exercise alert received.", DateTime.Now.ToString());
                    return;
                }

                Console.WriteLine("*** ALERT ****\n{0}: {1} at {2}", args.Alert.Sender, args.Alert.Identifier, args.Alert.Sent.ToString());

                foreach (AlertInfo info in args.Alert.Information)
                {
                    Console.WriteLine("\t{0} - {1} at {2}", info.SenderName, info.Event, info.Effective.ToString());
                    Console.WriteLine("\t{0}", info.Headline.ToUpper());
                    Console.WriteLine("\tDecription: {0}", info.Description);
                    Console.WriteLine("\tAction: {0}", info.Instruction);

                    foreach (AlertArea area in info.Areas)
                    {
                        Console.WriteLine("\t\tEffective For: " + area.Description);
                        //Console.WriteLine(area.Polygons[0]);
                    }// End of foreach
                }// End of foreach

                Console.WriteLine();
            };

            system.ConnectToStream("streaming1.naad-adna.pelmorex.com", 8080);

            Console.Read();
        }
    }
}
