using Entity;
using Microsoft.EntityFrameworkCore;

namespace Repositories
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        { }

        public DbSet<UserEntity> User { get; set; }
        public DbSet<UserRefreshTokenEntity> UserRefreshToken { get; set; }

        #region WorkSpace
        public DbSet<WorkSpaceEntity> WorkSpace { get; set; }
        public DbSet<WorkSpaceUserEntity> WorkSpaceUser { get; set; }
        public DbSet<WorkSpaceDeskEntity> WorkSpaceDesk { get; set; }

        #endregion

        #region Desk 

        public DbSet<DeskEntity> Desk { get; set; }
        public DbSet<DeskVisibilityTypeEntity> DeskVisibilityType { get; set; }

        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserRefreshTokenEntity>()
                .HasOne(urt => urt.User as UserEntity);

            modelBuilder.Entity<WorkSpaceUserEntity>()
                .HasOne(wsu => wsu.WorkSpace as WorkSpaceEntity);

            modelBuilder.Entity<WorkSpaceUserEntity>()
                .HasOne(wsu => wsu.User as UserEntity);

            modelBuilder.Entity<WorkSpaceDeskEntity>()
                .HasOne(wsd => wsd.WorkSpace as WorkSpaceEntity);

            modelBuilder.Entity<WorkSpaceDeskEntity>()
                .HasOne(wsd => wsd.Desk as DeskEntity);

            modelBuilder.Entity<DeskEntity>()
                .HasOne(d => d.DeskVisibilityType as DeskVisibilityTypeEntity);

            modelBuilder.Entity<DeskEntity>()
                .HasOne(d => d.User as UserEntity);
        }
    }
}
