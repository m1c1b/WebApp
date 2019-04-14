using System.Data.Entity;    
namespace WebApp.Models
{
    public class LabValuesContext:DbContext
    {
        public LabValuesContext():base("LabValues")
        {}
        public DbSet<Value> Values { get; set; }
    }
}