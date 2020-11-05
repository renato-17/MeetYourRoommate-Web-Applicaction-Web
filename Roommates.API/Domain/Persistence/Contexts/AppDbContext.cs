using Microsoft.EntityFrameworkCore;
using Roommates.API.Domain.Models;


namespace Roommates.API.Domain.Persistence.Contexts
{
    public class AppDbContext : DbContext
    {

        public DbSet<Person> People { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Lessor> Lessors { get; set; }



        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //Person entity
            builder.Entity<Person>().ToTable("People");
            builder.Entity<Person>()
                .HasDiscriminator<string>("Person Type")
                .HasValue<Student>("Student")
                .HasValue<Lessor>("Lessor");

            builder.Entity<Person>().HasKey(p => p.Id);
            builder.Entity<Person>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Person>().Property(p => p.FirstName).IsRequired().HasMaxLength(50);
            builder.Entity<Person>().Property(p => p.LastName).IsRequired().HasMaxLength(50);
            builder.Entity<Person>().Property(p => p.Dni).IsRequired().HasMaxLength(8);
            builder.Entity<Person>().Property(p => p.Phone).IsRequired().HasMaxLength(13);
            builder.Entity<Person>().Property(p => p.Gender).IsRequired();
            builder.Entity<Person>().Property(p => p.Address).IsRequired().HasMaxLength(100);
            builder.Entity<Person>().Property(p => p.Birthdate).IsRequired();
            builder.Entity<Person>().Property(p => p.Mail).IsRequired().HasMaxLength(60);
            builder.Entity<Person>().Property(p => p.Password).IsRequired();

      

            // Student entity
            builder.Entity<Student>().Property(s => s.Description).HasMaxLength(150);
            builder.Entity<Student>().Property(s => s.Hobbies).HasMaxLength(150);
            builder.Entity<Student>().Property(s => s.Smoker);

           

            // Lessor entity
            builder.Entity<Lessor>().Property(l => l.Premium);

           

      
        }
    }
}
