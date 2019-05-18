using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using FireSharp;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;

namespace FormForCreatingValues
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox2.Text != "" && textBox3.Text != "")
            {
                string[] valuesfromform = {textBox1.Text.Replace('.', ','), textBox2.Text.Replace('.', ','), textBox3.Text.Replace('.', ',')};
                string path = @"C:\Users\viazn\RiderProjects\WebApp\FormForCreatingValues\Values from the lab\";
                string subpath = $@"{DateTime.Now.Year}\{DateTime.Now.Month}\";
                FirebaseValue[] firebaseValues = new FirebaseValue[3]; // Массив объектов для записи в БДРВ
                Value[] valuestotext = new Value[3]; // Объекты для записи в файл
                FirebaseResponse response; // Переменная для получения ответов из БДРВ
                
                DirectoryInfo dirInfo = new DirectoryInfo(path);
                if (!dirInfo.Exists)
                {
                    dirInfo.Create();
                }
                dirInfo.CreateSubdirectory(subpath);
                
                IFirebaseConfig config = new FirebaseConfig
                                {
                                    AuthSecret = "r0Dx9Gi0gj7g3kZ4EJQodtFgOIq80IuBfBuko8HS",
                                    BasePath = "https://fir-2d407.firebaseio.com/"
                                };
                IFirebaseClient client = new FirebaseClient(config);
                
                
                response = client.Get("Last/Laboratory values/Id");
                int displacement = Convert.ToInt16(response.Body); // Смещение Id значений из БДРВ
                
                int i;
                for (i = 1 + displacement; i <= 3 + displacement; i++)
                {
                    FirebaseValue labvaluetofirebase = new FirebaseValue(i, Convert.ToDouble(valuesfromform[i - displacement - 1]), DateTime.Now);
                    
                    client.Set("Laboratory values/" + labvaluetofirebase.Id, labvaluetofirebase);
                    Thread.Sleep(100);
                    
                    response = client.Get("Last/Sensor values/Value");
                    valuestotext[i - displacement - 1] = new Value(i,labvaluetofirebase.Value,Convert.ToDouble(response.Body.Replace('.',',')), labvaluetofirebase.Time);
                    
                    if (i == 3 + displacement)
                    {
                        client.Set("Last/Laboratory values", labvaluetofirebase);
                    }
                }
                
                using (FileStream fstream = new FileStream(path + subpath+ $@"{DateTime.Now.Day}.txt", FileMode.OpenOrCreate))
                {
                    for (i = 0; i < 3; i++)
                    {
                        // преобразуем строку в байты
                        byte[] array = Encoding.Default.GetBytes
                            ($@"L_{valuestotext[i].LabVal} S_{valuestotext[i].SensVal} T_{valuestotext[i].Time}.{valuestotext[i].Time.Millisecond} L-S:{valuestotext[i].LabVal-valuestotext[i].SensVal}" + "\n");
                        // запись массива байтов в файл
                        fstream.Write(array, 0, array.Length);
                    }
                }
            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }
    }
}
