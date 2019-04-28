using System.Data.Entity;    
namespace Test
{
    public class LabValuesContext:DbContext
    {
        public LabValuesContext():base("LabValues")
        {}
        public DbSet<Value> Values { get; set; }
    }
}