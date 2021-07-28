using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TestArch_API.Data;

namespace TestArch_API.Test
{
    public class BasePruebas
    {
        protected TestArch_APIContext ConstruirContext(string nombreDB)
        {
            var opciones = new DbContextOptionsBuilder<TestArch_APIContext>()
                .UseInMemoryDatabase(nombreDB).Options;

            var dbContext = new TestArch_APIContext(opciones);
            return dbContext;
        }
    }
}
