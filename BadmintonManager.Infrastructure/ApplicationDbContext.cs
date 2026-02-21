using BadmintonManager.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BadmintonManager.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Member> Members { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<SessionParticipant> SessionParticipants { get; set; }
        public DbSet<Cost> Costs { get; set; }
        public DbSet<Skill> Skills { get; set; } 

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Session>().Property(s => s.TotalActualCost).HasColumnType("decimal(18,2)");
            modelBuilder.Entity<Session>().Property(s => s.AppliedCostPerPerson).HasColumnType("decimal(18,2)");
            modelBuilder.Entity<Session>().Property(s => s.ClubFundContribution).HasColumnType("decimal(18,2)");
            modelBuilder.Entity<Cost>().Property(c => c.Amount).HasColumnType("decimal(18,2)");
            modelBuilder.Entity<SessionParticipant>().Property(sp => sp.AmountDue).HasColumnType("decimal(18,2)");

            // --- Cấu hình quan hệ Member - Skill ---
            modelBuilder.Entity<Member>()
                .HasOne(m => m.SkillLevel)
                .WithMany()
                .HasForeignKey(m => m.SkillLevelId)
                .OnDelete(DeleteBehavior.SetNull);
            
            // --- Seed Data (Dữ liệu mẫu cho trình độ) ---
            modelBuilder.Entity<Skill>().HasData(
                new Skill { Id = Guid.Parse("11111111-1111-1111-1111-111111111111"), Name = "TBY", Description = "Biết luật, cầu chưa ổn định", ScoreWeight = 100 },
                new Skill { Id = Guid.Parse("22222222-2222-2222-2222-222222222222"), Name = "TB-", Description = "Phong trào, đánh đều tay",    ScoreWeight = 200 },
                new Skill { Id = Guid.Parse("33333333-3333-3333-3333-333333333333"), Name = "TB",  Description = "Tốt, bộ pháp tốt, cầu cắm",   ScoreWeight = 300 },
                new Skill { Id = Guid.Parse("44444444-4444-4444-4444-444444444444"), Name = "TB+", Description = "chiến thuật tốt đánh hay ",   ScoreWeight = 400 },
                new Skill { Id = Guid.Parse("55555555-5555-5555-5555-555555555555"), Name = "TBK", Description = "Bán chuyên, kỹ thuật cao",    ScoreWeight = 500 }
            );
        }
    }
}
