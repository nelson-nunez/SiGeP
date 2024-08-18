using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Logging;
using SiGeP.Model.Model;
using SiGeP.Model.Base;

namespace SiGeP.Model
{
    public class DbModelContext : DbContext
    {
        public DbSetEntities DbSets { get; } = new DbSetEntities();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                              .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                              .AddJsonFile("appsettings.json")
                              .Build();
                var str = configuration.GetConnectionString("DefaultConnection");
                if (string.IsNullOrEmpty(str))
                    throw new Exception("No hay string de conexión...");

                optionsBuilder
                    .UseSqlServer(str)
                    .EnableSensitiveDataLogging()
                    .ConfigureWarnings(c => c.Log((RelationalEventId.CommandExecuting, LogLevel.Debug)));

                optionsBuilder.UseLazyLoadingProxies();
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Customer>().HasKey(x => new { x.Id });
            modelBuilder.Entity<Gender>().HasKey(x => new { x.Id });

            //Elimina ciclos de eliminacion en cascada
            var cascadeFKs = modelBuilder.Model.GetEntityTypes().SelectMany(t => t.GetForeignKeys()).Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade);
            foreach (var fk in cascadeFKs)
                fk.DeleteBehavior = DeleteBehavior.Restrict;

            AddMyFilters(ref modelBuilder);

            modelBuilder.Seed();
        }

        private void AddMyFilters(ref ModelBuilder modelBuilder)
        {
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                //other automated configurations left out   
                if (entityType.ClrType != null && typeof(ISoftDelete).IsAssignableFrom(entityType.ClrType) && entityType.BaseType == null)
                {
                    entityType.AddSoftDeleteQueryFilter();
                }
            }
        }
    }

    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            if (modelBuilder != null)
            {
                SeedCustomer(modelBuilder);
                SeedGender(modelBuilder);
                SeedAppUser(modelBuilder);
            }
        }
        private static void SeedAppUser(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AppUser>().HasData(
            new AppUser
            {
                Id = 1,
                Name = "admin",
                Password = "admin",
                CreatedBy = "System",   // Asignar valor a CreatedBy
                Created = DateTime.Now,
            });
        }

        private static void SeedCustomer(ModelBuilder modelBuilder)
        {
            var customers = new List<Customer>
            {
                new Customer
                {
                    Id = 1,
                    Name = "Juan Pérez",
                    BirthDate = new DateTime(1985, 5, 15),
                    CUIL = "20123456789",
                    GenderId = 1, // Masculino
                    Phone = "1234567890",
                    CreatedBy = "System",
                    Created = DateTime.Now,
                },
                new Customer
                {
                    Id = 2,
                    Name = "María López",
                    BirthDate = new DateTime(1990, 3, 22),
                    CUIL = "20234567890",
                    GenderId = 2, // Femenino
                    Phone = "0987654321",
                    CreatedBy = "System",
                    Created = DateTime.Now,
                },
                new Customer
                {
                    Id = 3,
                    Name = "Carlos García",
                    BirthDate = new DateTime(1982, 7, 30),
                    CUIL = "20345678901",
                    GenderId = 1, // Masculino
                    Phone = "1122334455",
                    CreatedBy = "System",
                    Created = DateTime.Now,
                },
                new Customer
                {
                    Id = 4,
                    Name = "Ana Martínez",
                    BirthDate = new DateTime(1995, 1, 17),
                    CUIL = "20456789012",
                    GenderId = 2, // Femenino
                    Phone = "6677889900",
                    CreatedBy = "System",
                    Created = DateTime.Now,
                },
                new Customer
                {
                    Id = 5,
                    Name = "Miguel Fernández",
                    BirthDate = new DateTime(1978, 12, 5),
                    CUIL = "20567890123",
                    GenderId = 1, // Masculino
                    Phone = "5566778899",
                    CreatedBy = "System",
                    Created = DateTime.Now,
                }
            };

            foreach (var customer in customers)
            {
                modelBuilder.Entity<Customer>().HasData(customer);
            }
        }


        private static void SeedGender(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Gender>().HasData(
            new Gender
            {
                Id = 1,
                Name = "Femenino",
                CreatedBy = "System",   // Asignar valor a CreatedBy
                Created = DateTime.Now,
            },
            new Gender
            {
                Id = 2,
                Name = "Masculino",
                CreatedBy = "System",   // Asignar valor a CreatedBy
                Created = DateTime.Now,
            },
            new Gender
            {
                Id = 3,
                Name = "Otro",
                CreatedBy = "System",   // Asignar valor a CreatedBy
                Created = DateTime.Now,
            });
        }

    }
}
