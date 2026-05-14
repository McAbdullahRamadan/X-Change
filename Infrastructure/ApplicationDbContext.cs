using Domain.Entities.Business;
using Domain.Entities.System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure
{

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string,
      ApplicationUserClaim, ApplicationUserRole, ApplicationUserLogin,
      ApplicationRoleClaim, ApplicationUserToken>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Course> Courses => Set<Course>();
        public DbSet<Section> Sections => Set<Section>();
        public DbSet<Lesson> Lessons => Set<Lesson>();
        public DbSet<Enrollment> Enrollments => Set<Enrollment>();
        public DbSet<ApplicationUserPasswordHistory> PasswordHistories => Set<ApplicationUserPasswordHistory>();


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Decimal Price
            builder.Entity<Course>()
                   .Property(x => x.Price)
                   .HasColumnType("decimal(18,2)")
                   .IsRequired();

            // Relationships Business Entities
            builder.Entity<Course>()
                   .HasOne(c => c.Instructor)
                   .WithMany(u => u.Courses)
                   .HasForeignKey(c => c.InstructorId);

            builder.Entity<Section>()
                   .HasOne(s => s.Course)
                   .WithMany(c => c.Sections)
                   .HasForeignKey(s => s.CourseId);

            builder.Entity<Lesson>()
                   .HasOne(l => l.Section)
                   .WithMany(s => s.Lessons)
                   .HasForeignKey(l => l.SectionId);

            builder.Entity<Enrollment>()
                 .HasOne(e => e.User)
                 .WithMany(u => u.Enrollments)
                 .HasForeignKey(e => e.UserId)
                 .OnDelete(DeleteBehavior.Cascade); // احتفظ بهذا إذا كان ضرورياً

            builder.Entity<Enrollment>()
                .HasOne(e => e.Course)
                .WithMany(c => c.Enrollments)
                .HasForeignKey(e => e.CourseId)
                .OnDelete(DeleteBehavior.Restrict); // غير هذا إلى Restrict أو ClientCascade

            // Identity Relationships
            builder.Entity<ApplicationUserClaim>()
                   .HasOne(uc => uc.User)
                   .WithMany(u => u.Claims)
                   .HasForeignKey(uc => uc.UserId);

            builder.Entity<ApplicationUserLogin>()
                   .HasOne(l => l.User)
                   .WithMany(u => u.Logins)
                   .HasForeignKey(l => l.UserId);

            builder.Entity<ApplicationUserRole>()
                   .HasOne(ur => ur.User)
                   .WithMany(u => u.UserRoles)
                   .HasForeignKey(ur => ur.UserId);

            builder.Entity<ApplicationUserRole>()
                   .HasOne(ur => ur.Role)
                   .WithMany(r => r.UserRoles)
                   .HasForeignKey(ur => ur.RoleId);

            builder.Entity<ApplicationRoleClaim>()
                   .HasOne(rc => rc.Role)
                   .WithMany(r => r.RoleClaims)
                   .HasForeignKey(rc => rc.RoleId);

            builder.Entity<ApplicationUserToken>()
                   .HasOne(t => t.User)
                   .WithMany(u => u.Tokens)
                   .HasForeignKey(t => t.UserId);
            builder.Entity<ApplicationUserPasswordHistory>()
                .HasOne(ph => ph.CreatedByUser)
                .WithMany()
                .HasForeignKey(ph => ph.CreatedByUserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }

}
