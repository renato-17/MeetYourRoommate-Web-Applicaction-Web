using Microsoft.EntityFrameworkCore;
using Roommates.API.Domain.Models;
using System.IO;

namespace Roommates.API.Domain.Persistence.Contexts
{
    public class AppDbContext : DbContext
    {

        public DbSet<Person> People { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Models.Task> Tasks { get; set; }
        public DbSet<StudyCenter> StudyCenters { get; set; }
        public DbSet<Campus> Campuses { get; set; }
        public DbSet<Lessor> Lessors { get; set; }
        public DbSet<Ad> Ads { get; set; }
        public DbSet<Property> Properties { get; set; }
        public DbSet<PropertyDetail> PropertyDetails { get; set; }
        public DbSet<PropertyResource> PropertyResources { get; set; }
        public DbSet<FriendshipRequest> FriendshipRequests { get; set; }
        public DbSet<ReservationDetail> ReservationDetails { get; set; }
        public DbSet<Reservation> Reservations { get; set; }

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
            builder.Entity<Student>().Property(s => s.Available).HasDefaultValue(1);
            
            builder.Entity<Student>()
                .HasMany(s => s.FriendshipRequestsSent)
                .WithOne(fs => fs.StudentOne)
                .HasForeignKey(fs => fs.StudentOneId);

            builder.Entity<Student>()
                .HasMany(s => s.FriendshipRequestsReceived)
                .WithOne(fs => fs.StudentTwo)
                .HasForeignKey(fs => fs.StudentTwoId);

            builder.Entity<Student>()
                .HasMany(s => s.ReservationDetails)
                .WithOne(rd => rd.Student)
                .HasForeignKey(rd => rd.StudentId);

            //Friendship Request Entity
            builder.Entity<FriendshipRequest>().ToTable("Friendship_Requests");
            builder.Entity<FriendshipRequest>().HasKey(fs => new { fs.StudentOneId, fs.StudentTwoId });
            builder.Entity<FriendshipRequest>().Property(fs => fs.Status).HasDefaultValue(0);

            // Team entity
            builder.Entity<Team>().ToTable("Teams");
            builder.Entity<Team>().HasKey(t => t.Id);
            builder.Entity<Team>().Property(t => t.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Team>().Property(t => t.Name).IsRequired().HasMaxLength(50);

            builder.Entity<Team>()
                .HasMany(t => t.Students)
                .WithOne(s => s.Team)
                .HasForeignKey(s => s.TeamId);

            builder.Entity<Team>()
                .HasMany(t => t.Tasks)
                .WithOne(t => t.Team)
                .HasForeignKey(t => t.TeamId).IsRequired();

            // Task Entity
            builder.Entity<Models.Task>().ToTable("Tasks");
            builder.Entity<Models.Task>().HasKey(t => t.Id);
            builder.Entity<Models.Task>().Property(t => t.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Models.Task>().Property(t => t.Description).IsRequired().HasMaxLength(50);
            builder.Entity<Models.Task>().Property(t => t.CreatedDate).ValueGeneratedOnAdd();

            // Study Center Entity
            builder.Entity<StudyCenter>().ToTable("Study_Centers");
            builder.Entity<StudyCenter>().HasKey(s => s.Id);
            builder.Entity<StudyCenter>().Property(s => s.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<StudyCenter>().Property(s => s.Name).IsRequired().HasMaxLength(25);

            builder.Entity<StudyCenter>()
                .HasMany(s => s.Campus)
                .WithOne(s => s.StudyCenter)
                .HasForeignKey(s => s.StudyCenterId);

            //Campus Entity
            builder.Entity<Campus>().ToTable("Campuses");
            builder.Entity<Campus>().HasKey(c => c.Id);
            builder.Entity<Campus>().Property(c => c.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Campus>().Property(c => c.Name).IsRequired().HasMaxLength(30);
            builder.Entity<Campus>().Property(c => c.Address).IsRequired().HasMaxLength(100);

            builder.Entity<Campus>()
                .HasMany(sc => sc.Students)
                .WithOne(s => s.Campus)
                .HasForeignKey(s => s.CampusId);

            // Lessor entity
            builder.Entity<Lessor>().Property(l => l.Premium);

            builder.Entity<Lessor>()
                .HasMany(p => p.Properties)
                .WithOne(p => p.Lessor)
                .HasForeignKey(p => p.LessorId);
            builder.Entity<Lessor>()
                .HasMany(l => l.Ads)
                .WithOne(a => a.Lessor)
                .HasForeignKey(a => a.LessorId);
            builder.Entity<Lessor>()
                .HasMany(l => l.ReservationDetails)
                .WithOne(rd => rd.Lessor)
                .HasForeignKey(rd => rd.LessorId);

            // Ad Entity
            builder.Entity<Ad>().ToTable("Ads");
            builder.Entity<Ad>().HasKey(a => a.Id);
            builder.Entity<Ad>().Property(a => a.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Ad>().Property(a => a.DateStart).ValueGeneratedOnAdd();
            builder.Entity<Ad>().Property(a => a.DateUpdate).ValueGeneratedOnUpdate();

            builder.Entity<Ad>()
                .HasMany(a => a.Comments)
                .WithOne(c => c.Ad)
                .HasForeignKey(c => c.AdId);

            // Property Entity
            builder.Entity<Property>().ToTable("Properties");
            builder.Entity<Property>().HasKey(t => t.Id);
            builder.Entity<Property>().Property(t => t.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Property>().Property(t => t.Address).IsRequired().HasMaxLength(100);
            builder.Entity<Property>().Property(t => t.Description).IsRequired().HasMaxLength(300);

            builder.Entity<Property>()
                .HasMany(p => p.Ads)
                .WithOne(a => a.Property)
                .HasForeignKey(a => a.PropertyId);
            builder.Entity<Property>()
                .HasOne(p => p.PropertyDetail)
                .WithOne(pd => pd.Property)
                .HasForeignKey<PropertyDetail>(pd => pd.PropertyId);
            builder.Entity<Property>()
                .HasMany(p => p.ReservationDetails)
                .WithOne(rd => rd.Property)
                .HasForeignKey(rd => rd.PropertyId);

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


            // Reservation Detail Entity
            builder.Entity<ReservationDetail>().ToTable("Reservation_details");
            builder.Entity<ReservationDetail>().HasKey(rd => rd.Id);
            builder.Entity<ReservationDetail>().Property(rd => rd.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<ReservationDetail>().Property(rd => rd.Amount).IsRequired();
            builder.Entity<ReservationDetail>().Property(rd => rd.Downpayment).IsRequired();

            // Reservation Entity
            builder.Entity<Reservation>().ToTable("Reservations");
            builder.Entity<Reservation>().HasKey(r => r.Id);
            builder.Entity<Reservation>().Property(r => r.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Reservation>().Property(r => r.DateStart).IsRequired();
            builder.Entity<Reservation>().Property(r => r.DateEnd).IsRequired();

            builder.Entity<Reservation>()
                .HasMany(r => r.ReservationDetails)
                .WithOne(rd => rd.Reservation)
                .HasForeignKey(rd => rd.ReservationId);

        }
    }
}
