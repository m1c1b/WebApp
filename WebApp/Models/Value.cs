using System;

namespace WebApp.Models
{
    public class Value
    {
        public int Id { get; set; }      // Id значения
        public double LabVal { get; set; }  // Значение
        public double SensVal { get; set; }  // Значение
        public DateTime Time { get; set; } // Дата создания значения

        public Value(double labVal, double sensVal, DateTime time)
        {
            LabVal = labVal;
            SensVal = sensVal;
            Time = time;
        }

        public Value()
        {
            
        }
    }
}