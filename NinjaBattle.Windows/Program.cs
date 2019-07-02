using Microsoft.AspNet.SignalR.Client;
using System;

namespace NinjaBattle.Windows
{
    //#if WINDOWS || LINUX
    /// <summary>
    /// The main class.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //Set connection
            var connection = new HubConnection("http://localhost:59242");
            //Make proxy to hub based on hub name on server
            var myHub = connection.CreateHubProxy("Chat");
            //Start connection

            connection.Start().ContinueWith(task => {
                if (task.IsFaulted)
                {
                    //Console.WriteLine("There was an error opening the connection:{0}",
                    //                  task.Exception.GetBaseException());
                }
                else
                {
                    //Console.WriteLine("Connected");
                }

            }).Wait();

            myHub.Invoke<string>("Send", "HELLO World ").ContinueWith(task => {
                if (task.IsFaulted)
                {
                    Console.WriteLine("There was an error calling send: {0}",
                                      task.Exception.GetBaseException());
                }
                else
                {
                    Console.WriteLine(task.Result);
                }
            });

            myHub.On<string>("addMessage", param => {
                Console.WriteLine(param);
            });

            myHub.Invoke<string>("DoSomething", "I'm doing something!!!").Wait();


            using (var game = new Combate())
                game.Run();
        }
    }
    //#endif
}
