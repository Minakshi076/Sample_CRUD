using Sample_CRUD.Models;
using System.Data.Entity;

namespace Sample_CRUD.DAL
{
    public class SampleCrudContext :DbContext
    {
        public SampleCrudContext()
            : base("name=SampleCrudEntities")
        {           
        }

        public DbSet<State> State { get; set; }
        public DbSet<Country> Country { get; set; }
    }
}