using System.Data.Entity;
namespace Test
{
    public class SensorValuesContext:DbContext
    {
        public SensorValuesContext():base("SensorValues")
        {}
        public DbSet<Value> Values { get; set; }
    }
}