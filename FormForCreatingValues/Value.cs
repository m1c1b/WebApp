using System;

namespace FormForCreatingValues
{
    public class Value
    {
        public int Id { get; set; }            // Id значения
        public double LabVal { get; set; }     // Калибровочное значение
        public double SensVal { get; set; }    // Значение, полученное с датчика
        public DateTime Time { get; set; }     // Время создания объекта

        public Value(int id, double labVal, double sensVal, DateTime time)    // Конструктор класса
        {
            Id = id;
            LabVal = labVal;
            SensVal = sensVal;
            Time = time;
        }
    }
}