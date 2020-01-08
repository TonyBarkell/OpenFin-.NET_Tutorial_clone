using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Openfin.Desktop;

namespace OpenFinHelloDotNet
{
 
  class Program
  {
        static void Main(string[] args)
        {

            Console.WriteLine("Inter Application Bus Example");
           

            var runtimeOptions = new RuntimeOptions
            {
                Version = "stable"
            };

            var runtime = Runtime.GetRuntimeInstance(runtimeOptions);

            runtime.Connect(() =>
            {
                /*
                InterApplicationBus.Subscription<string>(runtime, "OpenFinTopic").MessageReceived += (s, e) =>
                {
                    Console.WriteLine(e.Message);
                };
                */

                Console.WriteLine("The runtime has now connected.");

                var appOptions = new ApplicationOptions("application-name", "application-uuid", "about:blank");

                appOptions.MainWindowOptions.AutoShow = false;
                appOptions.MainWindowOptions.Frame = true;
                appOptions.MainWindowOptions.Resizable = true;
                appOptions.MainWindowOptions.Name = "Test Window";

                var application = runtime.CreateApplication(appOptions);
                application.run((a) =>
                {

                  Console.WriteLine("app started.");
            
                },
                (n) =>
                {
                });
            });

            string topic;
            string message;
            do
            {
                Console.WriteLine("Enter Topic:");
                topic = Console.ReadLine();
                Console.WriteLine("Enter Message:");
                message = Console.ReadLine();
                InterApplicationBus.Publish(runtime, topic, message);
            } while (!topic.Equals("quit"));

        }

  }
}
