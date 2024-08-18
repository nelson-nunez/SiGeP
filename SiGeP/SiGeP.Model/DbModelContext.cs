using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Logging;
using SiGeP.Model.Model;
using SiGeP.Model.Base;
using SiGeP.Model.ModelUser;
using SiGeP.Model.Model.Address;
using SiGeP.Model.Model.Person;

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
                SeedProvince(modelBuilder);
                SeedCity(modelBuilder);
                SeedNeighborhood(modelBuilder);
                SeedAddress(modelBuilder);
                SeedPerson(modelBuilder);
                SeedDoctor(modelBuilder);
                SeedAppointment(modelBuilder);
                SeedMedicalRecord(modelBuilder);
                SeedPayment(modelBuilder);
                SeedReminder(modelBuilder);
                SeedRole(modelBuilder);
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
                RoleId = 1,
                CreatedBy = "System",
                Created = DateTime.Now,
            });
        }
        
        private static void SeedAddress(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Address>().HasData(
            new Address
            {
                Id = 1,
                ProvinceId = 1,
                CityId = 1,
                NeighborhoodId = 1,
                StreetNumber = "1234",
                NeighborhoodName = "Centro",
                CreatedBy = "System",
                Created = DateTime.Now,
            });

            modelBuilder.Entity<Address>().HasData(
            new Address
            {
                Id = 2,
                ProvinceId = 1,
                CityId = 1,
                NeighborhoodId = 1,
                StreetNumber = "1234",
                NeighborhoodName = "Centro 222",
                CreatedBy = "System",
                Created = DateTime.Now,
            });
        }

        private static void SeedPerson(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>().HasData(
            new Person
            {
                Id = 1,
                Name = "Juan",
                LastName = "Pérez",
                DNI = "20123456789",
                BirthDate = new DateTime(1985, 5, 15),
                Phone = "1234567890",
                Email = "juan.perez@example.com",
                GenderId = 1,
                AddressId = 1,
                CreatedBy = "System",
                Created = DateTime.Now,
            },
            new Person
            {
                Id = 2,
                Name = "Dr. María",
                LastName = "González",
                DNI = "20987654321",
                BirthDate = new DateTime(1978, 11, 30),
                Phone = "0987654321",
                Email = "maria.gonzalez@example.com",
                GenderId = 2,
                AddressId = 2,
                CreatedBy = "System",
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
                    DoctorId = 1,  // Asumimos que el doctor ya existe
                    PersonId = 1,  // Referencia a la persona "Juan Pérez"
                    CreatedBy = "System",
                    Created = DateTime.Now,
                }
            };

            foreach (var customer in customers)
            {
                modelBuilder.Entity<Customer>().HasData(customer);
            }
        }

        private static void SeedDoctor(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Doctor>().HasData(
            new Doctor
            {
                Id = 1,
                PersonId = 2,  // Referencia a la persona "Dr. María González"
                Specialty = "Cardiología",
                CreatedBy = "System",
                Created = DateTime.Now,
            });
        }

        private static void SeedGender(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Gender>().HasData(
            new Gender
            {
                Id = 1,
                Name = "Masculino",
                CreatedBy = "System",
                Created = DateTime.Now,
            },
            new Gender
            {
                Id = 2,
                Name = "Femenino",
                CreatedBy = "System",
                Created = DateTime.Now,
            },
            new Gender
            {
                Id = 3,
                Name = "Otro",
                CreatedBy = "System",
                Created = DateTime.Now,
            });
        }

        private static void SeedProvince(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Province>().HasData(
            new Province
            {
                Id = 1,
                Name = "Buenos Aires",
                CreatedBy = "System",
                Created = DateTime.Now,
            },
            new Province
            {
                Id = 2,
                Name = "Córdoba",
                CreatedBy = "System",
                Created = DateTime.Now,
            });
        }

        private static void SeedCity(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<City>().HasData(
            new City
            {
                Id = 1,
                Name = "La Plata",
                ProvinceId = 1,
                CreatedBy = "System",
                Created = DateTime.Now,
            },
            new City
            {
                Id = 2,
                Name = "Córdoba Capital",
                ProvinceId = 2,
                CreatedBy = "System",
                Created = DateTime.Now,
            });
        }

        private static void SeedNeighborhood(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Neighborhood>().HasData(
            new Neighborhood
            {
                Id = 1,
                Name = "Centro",
                CityId = 1,
                CreatedBy = "System",
                Created = DateTime.Now,
            },
            new Neighborhood
            {
                Id = 2,
                Name = "Nueva Córdoba",
                CityId = 2,
                CreatedBy = "System",
                Created = DateTime.Now,
            });
        }

        private static void SeedAppointment(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Appointment>().HasData(
            new Appointment
            {
                Id = 1,
                Date = DateTime.Now.AddDays(1),
                CustomerId = 1,
                Address = "1234 Centro, La Plata",
                CreatedBy = "System",
                Created = DateTime.Now,
            });
        }

        private static void SeedMedicalRecord(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MedicalRecord>().HasData(
            new MedicalRecord
            {
                Id = 1,
                Date = DateTime.Now,
                Diagnosis = "Hipertensión",
                Treatment = "Dieta baja en sodio",
                CustomerId = 1,
                CreatedBy = "System",
                Created = DateTime.Now,
            });
        }

        private static void SeedPayment(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Payment>().HasData(
            new Payment
            {
                Id = 1,
                Date = DateTime.Now,
                Amount = 200.00m,
                AppointmentId = 1,
                CreatedBy = "System",
                Created = DateTime.Now,
            });
        }

        private static void SeedReminder(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Reminder>().HasData(
            new Reminder
            {
                Id = 1,
                Date = DateTime.Now,
                SendMode = "Email",
                Sent = false,
                AppointmentId = 1,
                CreatedBy = "System",
                Created = DateTime.Now,
            });
        }

        private static void SeedRole(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>().HasData(
            new Role
            {
                Id = 1,
                Name = "Admin",
                Permissions = "FullAccess",
                CreatedBy = "System",
                Created = DateTime.Now,
            });
        }
    }

}
