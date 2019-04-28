using System;
using System.IO;

namespace Test
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            string path = $@"C:\Users\viazn\RiderProjects\WebApp\FormForCreatingValues\Values from the lab\{DateTime.Now.Year}\{DateTime.Now.Month}";

            if (FillDb.WritingCheck(path))
            {
                FillDb.FillLabVals(path);
            }
            FillDb.FillSensorVals();
        }
    }
}