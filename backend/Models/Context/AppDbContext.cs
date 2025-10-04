using Microsoft.EntityFrameworkCore;

namespace CourseMate.Models.Context
{
    public class AppDbContext : DbContext
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure the self-referencing many-to-many relationship for Course ↔ Course
            // DO NOT delete or comment this as this is crucial for EF Core to infer the relationship correctly.
            modelBuilder.Entity<CoursePrerequisite>()
                .HasOne(cp => cp.Course)
                .WithMany(c => c.PrerequisiteCourses)
                .HasForeignKey(cp => cp.CourseId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<CoursePrerequisite>()
                .HasOne(cp => cp.PrerequisiteCourse)
                .WithMany(c => c.IsPrerequisiteFor)
                .HasForeignKey(cp => cp.PrerequisiteId)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(modelBuilder);
        }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //modelBuilder.Entity<Admin>().HasData(
        //    new Admin()
        //    {
        //        Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
        //        FirstName = "Super",
        //        LastName = "Admin",
        //        Email = "admin@yopmail.com",
        //        Password = "123",  //password to be hashed yet.
        //        PhoneNumber = "0333-8478333",
        //        Role = (int)enumRole.Admin,
        //        CreatedAt = DateTime.Now,
        //        UpdatedAt = DateTime.Now,
        //        IsActive = (int)enumStatus.Active,
        //        DateOfBirth = new DateTime(2002, 10, 23),
        //        Department = "Administration",
        //        Address = "123 Admin St",
        //        City = "Lahore",
        //        State = "Admin State",
        //        ZipCode = "54000",
        //        Country = "Pakistan",
        //        ProfilePictureUrl = null,
        //        IsDeleted = false,
        //        Position = (int)enumPosition.Admin
        //    });

        public DbSet<Admin> Admins { get; set; }
        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<CourseOffering> CourseOfferings { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Prerequisite> Prerequisites { get; set; }
        public DbSet<StudentSemester> StudentSemesters { get; set; }
        public DbSet<Semester> Semesters { get; set; }
        public DbSet<GradeScale> Grades { get; set; }

    }
}
