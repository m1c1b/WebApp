using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using FireSharp;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;

namespace FormForCreatingValues
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Thread myThread = new Thread(Generate);
            myThread.Start(); // запускаем второй поток
            
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }

        static void Generate()
        {
            IFirebaseConfig config = new FirebaseConfig
            {
                AuthSecret = "r0Dx9Gi0gj7g3kZ4EJQodtFgOIq80IuBfBuko8HS",
                BasePath = "https://fir-2d407.firebaseio.com/"
            };
            IFirebaseClient client = new FirebaseClient(config);
                
            FirebaseResponse response = client.Get("Last/Sensor values/Id");
            int displacement = Convert.ToInt16(response.Body);
            int i = displacement;
            Random random = new Random();
            
            while (true)
            {
                i++;
                FirebaseValue sensval = new FirebaseValue(i, 92 + Math.Round(random.NextDouble(),2), DateTime.Now); 
                client.Set("Sensor values/" + sensval.Id, sensval);
                client.Set("Last/Sensor values", sensval);
            }
        }
    }
}
