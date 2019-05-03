using System;

namespace FormForCreatingValues
{
    public class Value
    {
        public int Id { get; set; }      // Id значения
        public double LabVal { get; set; }  // Значение
        public double SensVal { get; set; }  // Значение
        public DateTime Time { get; set; } // Дата создания значения

        public Value(int id, double labVal, double sensVal, DateTime time)
        {
            Id = id;
            LabVal = labVal;
            SensVal = sensVal;
            Time = time;
        }
    }
}