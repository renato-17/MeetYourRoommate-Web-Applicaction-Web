using Microsoft.EntityFrameworkCore;
using Roommates.API.Domain.Models;


namespace Roommates.API.Domain.Persistence.Contexts
{
    public class AppDbContext : DbContext
    {

        public DbSet<Person> People { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Lessor> Lessors { get; set; }
        public DbSet<Property> Properties { get; set; }
        public DbSet<PropertyDetail> PropertyDetails { get; set; }
        public DbSet<PropertyResource> PropertyResources { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Ad> Ads { get; set; }

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

            builder.Entity<Person>()
                .HasMany(p => p.Comments)
                .WithOne(c => c.Person)
                .HasForeignKey(c => c.PersonId);


            // Student entity
            builder.Entity<Student>().Property(s => s.Description).HasMaxLength(150);
            builder.Entity<Student>().Property(s => s.Hobbies).HasMaxLength(150);
            builder.Entity<Student>().Property(s => s.Smoker);

           

            // Lessor entity
            builder.Entity<Lessor>().Property(l => l.Premium);

            // Property Entity
            builder.Entity<Property>().ToTable("Properties");
            builder.Entity<Property>().HasKey(t => t.Id);
            builder.Entity<Property>().Property(t => t.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Property>().Property(t => t.Address).IsRequired().HasMaxLength(100);
            builder.Entity<Property>().Property(t => t.Description).IsRequired().HasMaxLength(300);

            builder.Entity<Property>()
                .HasOne(p => p.PropertyDetail)
                .WithOne(pd => pd.Property)
                .HasForeignKey<PropertyDetail>(pd => pd.PropertyId);

            // Property Detail Entity
            builder.Entity<PropertyDetail>().ToTable("Property_details");
            builder.Entity<PropertyDetail>().HasKey(pd => pd.Id);
            builder.Entity<PropertyDetail>().Property(pd => pd.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<PropertyDetail>().Property(pd => pd.Rooms).IsRequired();
            builder.Entity<PropertyDetail>().Property(pd => pd.Bathrooms).IsRequired();
            builder.Entity<PropertyDetail>().Property(pd => pd.SquareMeters).IsRequired();
            builder.Entity<PropertyDetail>().Property(pd => pd.Kitchen).IsRequired();
            builder.Entity<PropertyDetail>().Property(pd => pd.Livingroom).IsRequired();
            builder.Entity<PropertyDetail>().Property(pd => pd.Price).IsRequired();

            builder.Entity<PropertyDetail>()
                .HasMany(pd => pd.PropertyResources)
                .WithOne(pr => pr.PropertyDetail)
                .HasForeignKey(pr => pr.PropertyDetailId);

            // Property Resource Entity
            builder.Entity<PropertyResource>().ToTable("Property_resources");
            builder.Entity<PropertyResource>().HasKey(pr => pr.Id);
            builder.Entity<PropertyResource>().Property(pr => pr.Type).IsRequired().HasMaxLength(50);
            builder.Entity<PropertyResource>().Property(pr => pr.DateUpload).ValueGeneratedOnAdd();

            // Comment entity
            builder.Entity<Comment>().ToTable("Comments");
            builder.Entity<Comment>().HasKey(c => c.Id);
            builder.Entity<Comment>().Property(c => c.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Comment>().Property(c => c.Description).IsRequired().HasMaxLength(150);
            builder.Entity<Comment>().Property(c => c.DateCreated).ValueGeneratedOnAdd();
            builder.Entity<Comment>().Property(c => c.DateUpdated).ValueGeneratedOnUpdate();

            // Ad Entity
            builder.Entity<Ad>().ToTable("Ads");
            builder.Entity<Ad>().HasKey(a => a.Id);
            builder.Entity<Ad>().Property(a => a.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Ad>().Property(a => a.Title).IsRequired().HasMaxLength(20);
            builder.Entity<Ad>().Property(a => a.DateStart).ValueGeneratedOnAdd();
            builder.Entity<Ad>().Property(a => a.DateUpdate).ValueGeneratedOnUpdate();

            builder.Entity<Ad>()
                .HasMany(a => a.Comments)
                .WithOne(c => c.Ad)
                .HasForeignKey(c => c.AdId);
        }
    }
}
