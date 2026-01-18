using Microsoft.EntityFrameworkCore;

namespace WaterQualityMonitoring.Models
{
    public class ApplicationDbContext : DbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<WaterReport> WaterReports { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            
            // إضافة بيانات ابتدائية للمستخدمين
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    UserID = 1,
                    Username = "admin",
                    Email = "admin@waterquality.ps",
                    Password = HashPassword("admin123"),
                    PhoneNumber = "0599000000",
                    Governorate = "Gaza",
                    RegistrationDate = new DateTime(2025, 3, 3)
                },
                new User
                {
                    UserID = 2,
                    Username = "ahmad",
                    Email = "ahmad@gmail.com",
                    Password = HashPassword("ahmad123"),
                    PhoneNumber = "0599111111",
                    Governorate = "Rafah",
                    RegistrationDate = new DateTime(2025, 1, 1)
                },
                new User
                {
                    UserID = 3,
                    Username = "sara",
                    Email = "sara@hotmail.com",
                    Password = HashPassword("sara123"),
                    PhoneNumber = "0599222222",
                    Governorate = "KhanYounis",
                    RegistrationDate = new DateTime(2025, 2, 5)
                }
            );

            // إضافة بيانات ابتدائية لتقارير المياه
            modelBuilder.Entity<WaterReport>().HasData(
                new WaterReport
                {
                    ReportID = 1,
                    UserID = 2, // ahmad
                    Location = "Shuja'iyya neighborhood - Gaza",
                    ReportDate = new DateTime(2025, 1, 10),
                    PollutionType = "chemical pollution",
                    Description = "Unpleasant odor and strange color in drinking water",
                    Status = "Confirmed",
                    SourceType = "sewage from settlements"
                },
                new WaterReport
                {
                    ReportID = 2,
                    UserID = 3, // sara
                    Location = "Nuseirat camp",
                    ReportDate = new DateTime(2025, 2, 25),
                    PollutionType = "bacteria",
                    Description = "Bacterial contamination led to intestinal diseases",
                    Status = "Pending",
                    SourceType = "sewage leak"
                },
                new WaterReport
                {
                    ReportID = 3,
                    UserID = 1, // admin
                    Location = "Rafah City - Brazil Neighborhood",
                    ReportDate = new DateTime(2025, 3, 10),
                    PollutionType = "high salinity",
                    Description = "Salty water unfit for drinking",
                    Status = "Confirmed",
                    SourceType = "Seawater intrusion"
                },
                new WaterReport
                {
                    ReportID = 4,
                    UserID = 2, // ahmad
                    Location = "Al-Tuffah neighborhood - Gaza",
                    ReportDate = new DateTime(2025, 2, 10),
                    PollutionType = "heavy metals",
                    Description = "Complaints of stomach pain after drinking water",
                    Status = "Pending",
                    SourceType = "industrial waste"
                },
                new WaterReport
                {
                    ReportID = 5,
                    UserID = 3, // sara
                    Location = "Jabalia camp",
                    ReportDate = new DateTime(2025, 1, 15),
                    PollutionType = "High turbidity",
                    Description = "Turbid water with sediment present",
                    Status = "Confirmed",
                    SourceType = "Construction and excavation works"
                }
            );
            
        }

        // دالة مساعدة لتجزئة كلمات المرور

        // دالة مساعدة لتجزئة كلمات المرور
        private static string HashPassword(string password)
        {
            using var sha256 = System.Security.Cryptography.SHA256.Create();
            var hashedBytes = sha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
        }


    }
}


