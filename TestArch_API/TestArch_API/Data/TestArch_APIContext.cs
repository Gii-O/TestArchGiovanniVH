using Microsoft.EntityFrameworkCore;

namespace TestArch_API.Data
{
    /* ------------------------------------------------------------------------------------------------------------------*/
    /* ----- ORM: DbContext -----*/
    /* ------------------------------------------------------------------------------------------------------------------*/
    public class TestArch_APIContext : DbContext
    {
        public TestArch_APIContext (DbContextOptions<TestArch_APIContext> options): base(options)
        {
        }

        /* ------------------------------------------------------------------------------------------------------------------*/
        /* ----- TABLES MAP LINQ -----*/
        /* ------------------------------------------------------------------------------------------------------------------*/
        public DbSet<TestArch_API.Models.Usuario> usuarios { get; set; }
        public DbSet<TestArch_API.Models.Tarea> tareas { get; set; }
    }
}
