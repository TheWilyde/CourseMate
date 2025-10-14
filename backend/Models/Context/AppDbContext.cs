using Microsoft.EntityFrameworkCore;

namespace CourseMate.Models.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Admin> Admins => Set<Admin>();
        public DbSet<Instructor> Instructors => Set<Instructor>();
        public DbSet<Student> Students => Set<Student>();
        public DbSet<Course> Courses => Set<Course>();
        public DbSet<CourseOffering> CourseOfferings => Set<CourseOffering>();
        public DbSet<CoursePrerequisite> CoursePrerequisites => Set<CoursePrerequisite>();
        public DbSet<Enrollment> Enrollments => Set<Enrollment>();
        public DbSet<StudentSemester> StudentSemesters => Set<StudentSemester>();
        public DbSet<StudentCourseGrade> StudentCourseGrades => Set<StudentCourseGrade>();
        public DbSet<Semester> Semesters => Set<Semester>();
        public DbSet<Degree> Degrees => Set<Degree>();
        public DbSet<Department> Departments => Set<Department>();
        public DbSet<Lecture> Lectures => Set<Lecture>();
        public DbSet<GradeScale> GradeScales => Set<GradeScale>(); 
        public DbSet<UserRole> UserRoles => Set<UserRole>();


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // === CoursePrerequisite: self-referencing many-to-many (Course ↔ Course) ===
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

            //  Lecture: Course ↔ Instructor ===
            modelBuilder.Entity<Lecture>()
                .HasOne(l => l.Course)
                .WithMany(c => c.Lectures)
                .HasForeignKey(l => l.CourseId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Lecture>()
                .HasOne(l => l.Instructor)
                .WithMany(i => i.Lectures)
                .HasForeignKey(l => l.InstructorId)
                .OnDelete(DeleteBehavior.Restrict); // safer than Cascade

            // CourseOffering ↔ Course & Semester & Instructor ===
            modelBuilder.Entity<CourseOffering>()
                .HasOne(o => o.Course)
                .WithMany(c => c.Offerings)
                .HasForeignKey(o => o.CourseId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<CourseOffering>()
                .HasOne(o => o.Semester)
                .WithMany(s => s.Offerings)
                .HasForeignKey(o => o.SemesterId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<CourseOffering>()
                .HasOne(o => o.Instructor)
                .WithMany(i => i.CourseOfferings)
                .HasForeignKey(o => o.InstructorId)
                .OnDelete(DeleteBehavior.Restrict);

            // Enrollment ↔ Student & CourseOffering ===
            modelBuilder.Entity<Enrollment>()
                .HasOne(e => e.Student)
                .WithMany(s => s.Enrollments)
                .HasForeignKey(e => e.StudentId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Enrollment>()
                .HasOne(e => e.CourseOffering)
                .WithMany(o => o.Enrollments)
                .HasForeignKey(e => e.OfferingId)
                .OnDelete(DeleteBehavior.Cascade);

            // StudentSemester ↔ Student & Semester ===
            modelBuilder.Entity<StudentSemester>()
                .HasOne(ss => ss.Student)
                .WithMany(s => s.StudentSemesters)
                .HasForeignKey(ss => ss.StudentId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<StudentSemester>()
                .HasOne(ss => ss.Semester)
                .WithMany(s => s.StudentSemesters)
                .HasForeignKey(ss => ss.SemesterId)
                .OnDelete(DeleteBehavior.Cascade);

            // StudentCourseGrade ↔ StudentSemester, CourseOffering, Instructor ===
            modelBuilder.Entity<StudentCourseGrade>()
                .HasOne(scg => scg.StudentSemester)
                .WithMany(ss => ss.StudentCourseGrades)
                .HasForeignKey(scg => scg.StudentSemesterId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<StudentCourseGrade>()
                .HasOne(scg => scg.CourseOffering)
                .WithMany()
                .HasForeignKey(scg => scg.CourseOfferingId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<StudentCourseGrade>()
                .HasOne(scg => scg.Instructor)
                .WithMany()
                .HasForeignKey(scg => scg.InstructorId)
                .OnDelete(DeleteBehavior.SetNull);

            // Seed UserRoles data
            modelBuilder.Entity<UserRole>().HasData(

                // DateTime set to static becasue of EF Constraints
                new UserRole 
                { 
                    Id = 1, 
                    RoleName = "Student", 
                    EmailDomain = "@student.coursemate.com",
                    Description = "Student role with access to courses and grades",
                    CanSelfRegister = true,
                    CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                    UpdatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                    IsActive = true
                },
                new UserRole
                { 
                    Id = 2, 
                    RoleName = "Instructor", 
                    EmailDomain = "@instructor.coursemate.com",
                    Description = "Instructor role with access to teaching tools",
                    CanSelfRegister = true,
                    CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                    UpdatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                    IsActive = true
                },
                new UserRole 
                { 
                    Id = 3, 
                    RoleName = "Admin", 
                    EmailDomain = "@coursemate.com",
                    Description = "Administrator with full system access",
                    CanSelfRegister = false,
                    CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                    UpdatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                    IsActive = true
                }
            );

            // Add unique constraint on EmailDomain
            modelBuilder.Entity<UserRole>()
                .HasIndex(ur => ur.EmailDomain)
                .IsUnique();

            base.OnModelCreating(modelBuilder);
        }
    }
}
