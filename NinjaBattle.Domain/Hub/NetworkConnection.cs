

namespace NinjaBattle.Domain.Hub
{
    public static class NetworkConnection
    {
        //private static IHubProxy hubProxy;
        //private static HubConnection connection;
        public static void ConnectToHub()
        {
            //connection = new HubConnection("https://localhost:44305");

            ////Make proxy to hub based on hub name on server
            //hubProxy = connection.CreateHubProxy("battleHub");

            //hubProxy.On<string>("Send", param =>
            //{
            //    //Console.WriteLine(param);
            //});
            //connection.Start().ContinueWith(task =>
            //{
            //    if (task.IsFaulted)
            //    {
            //        var message = "Thee was an error opening the connection:{0}" + task.Exception.GetBaseException();
            //        //Console.WriteLine("There was an error opening the connection:{0}",
            //        //                  task.Exception.GetBaseException());
            //    }
            //    else
            //    {
            //        //Console.WriteLine("Connected");
            //    }

            //}).Wait();
        }

        public static void DisconnectToHub()
        {
            //connection.Stop();
        }
    }
}
