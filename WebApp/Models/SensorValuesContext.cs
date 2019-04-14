using System.Data.Entity;
namespace WebApp.Models
{
    public class SensorValuesContext:DbContext
    {
        public SensorValuesContext():base("SensorValues")
        {}
        public DbSet<Value> Values { get; set; }
    }
}