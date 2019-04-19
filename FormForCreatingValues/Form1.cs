using System;
using System.IO;
using System.Text;
using System.Windows.Forms;

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
                string[] values = {textBox1.Text, textBox2.Text, textBox3.Text};
                string path = @"C:\Users\viazn\RiderProjects\WebApp\FormForCreatingValues\Values from the lab";
                string subpath = $@"{DateTime.Now.Year}\{DateTime.Now.Month}";

                DirectoryInfo dirInfo = new DirectoryInfo(path);
                if (!dirInfo.Exists)
                {
                    dirInfo.Create();
                }

                dirInfo.CreateSubdirectory(subpath);

                using (FileStream fstream = new FileStream($@"C:\Users\viazn\RiderProjects\WebApp\FormForCreatingValues\Values from the lab\{DateTime.Now.Year}\{DateTime.Now.Month}\{DateTime.Now.Day}.txt",
                    FileMode.OpenOrCreate))
                {
                    for (int i = 0; i < values.Length; i++)
                    {
                        // преобразуем строку в байты
                        byte[] array = Encoding.Default.GetBytes(values[i] + " " + $@"{DateTime.Now}" + "\n");
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
