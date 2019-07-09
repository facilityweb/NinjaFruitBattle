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
            using (var game = new Combate())
                game.Run();
        }
    }
    //#endif
}
