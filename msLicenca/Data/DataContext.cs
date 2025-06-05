using Microsoft.EntityFrameworkCore;
using msLicenca.Entity;
using System.Collections.Generic;

namespace msLicenca.Data
{
    public class DataContext : DbContext
    {
        //install entityFramework
        public DataContext(DbContextOptions options) : base(options)
        {
        }

        //falta migration nao adicionei no console nem criei nada
        public DbSet<Licenca> Licenca { get; set; }

    }
}
