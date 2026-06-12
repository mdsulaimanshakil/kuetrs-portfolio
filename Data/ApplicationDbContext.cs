using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using KUETResearchSociety.Models;

namespace KUETResearchSociety.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<TeamMember> TeamMembers { get; set; }
        public DbSet<Activity> Activities { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<GalleryItem> GalleryItems { get; set; }
        public DbSet<ContactMessage> ContactMessages { get; set; }
        public DbSet<SiteSettings> SiteSettings { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Seed default site settings
            builder.Entity<SiteSettings>().HasData(new SiteSettings
            {
                Id = 1,
                SiteTitle = "KUET Research Society",
                HeroTitle = "KUET Research Society",
                HeroSubtitle = "Innovating Through Research and Technology",
                HeroButtonPrimary = "Join Us",
                HeroButtonSecondary = "Learn More",
                AboutText = "The KUET Research Society is a student-driven organization dedicated to fostering a strong culture of research, innovation, and critical thinking among students of Khulna University of Engineering & Technology (KUET). It serves as a platform where curious minds come together to explore ideas, work on meaningful projects, and develop solutions to real-world problems.",
                VisionText = "To inspire future researchers and innovators, building a community that values knowledge, creativity, and continuous learning.",
                MissionText = "To encourage students to engage in research activities by organizing workshops, seminars, and collaborative projects.",
                ContactEmail = "info@kuetrs.org",
                Phone = "Will be updated soon",
                Address = "KUET Campus, Khulna, Bangladesh"
            });

            // Seed sample team members
            builder.Entity<TeamMember>().HasData(
                new TeamMember
                {
                    Id = 1,
                    Name = "Shah Md Khalil Ullah",
                    Designation = "President",
                    Department = "Computer Science & Engineering",
                    Batch = "20",
                    DisplayOrder = 1,
                    CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                },
                new TeamMember
                {
                    Id = 2,
                    Name = "Ahmed Safa",
                    Designation = "Member Secretary",
                    Department = "Computer Science & Engineering",
                    Batch = "20",
                    DisplayOrder = 2,
                    CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                }
            );

            // Seed sample activities
            builder.Entity<Activity>().HasData(
                new Activity
                {
                    Id = 1,
                    Title = "AI Research Lab",
                    Description = "Developing advanced machine learning models for healthcare diagnostics and automated systems.",
                    Details = "Our AI Research Lab focuses on cutting-edge machine learning and deep learning technologies to solve real-world problems.",
                    CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                },
                new Activity
                {
                    Id = 2,
                    Title = "IoT Automation",
                    Description = "Smart campus initiatives focusing on energy efficiency, sensing, and sustainable environments.",
                    Details = "We build IoT solutions for smart campus management including automated lighting, environmental monitoring, and energy optimization.",
                    CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                },
                new Activity
                {
                    Id = 3,
                    Title = "Robotics Workshop",
                    Description = "Annual hands-on workshops exploring autonomous navigation and drone technologies.",
                    Details = "Our annual Robotics Workshop brings together students from all departments to build and program autonomous robots.",
                    CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                }
            );
        }
    }
}
