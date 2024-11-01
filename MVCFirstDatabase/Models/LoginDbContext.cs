using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProyectoControlDeParqueos.Models;

namespace MVCFirstDatabase.Models
{


    
        public class LoginDbContext : IdentityDbContext<ApplicationUsers>

        {

            public LoginDbContext(DbContextOptions<LoginDbContext> options) : base(options)

            {

        }


        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Parqueo> Parqueos { get; set; }
        public DbSet<Tarifa> Tarifas { get; set; }
        public DbSet<Carro> Carros { get; set; }
        public DbSet<RegistroIngreso> RegistroIngresos { get; set; }
        public DbSet<RegistroSalida> RegistroSalidas { get; set; }
        public DbSet<Factura> Facturas { get; set; }


    }
    }


