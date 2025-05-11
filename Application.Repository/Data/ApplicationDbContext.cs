
using Microsoft.EntityFrameworkCore;

namespace Chat.Application.Entities.Data 
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<UserEntity> tblUsers { get; set; }
        public DbSet<ApiTokenEntity> tblApiTokens { get; set; } 
        public DbSet<UserDetailEntity> tblUserDetails { get; set; }
        public DbSet<GroupEntity> tblGroups { get; set; }
        public DbSet<UserGroupEntity> tblUserGroups { get; set; }
        public DbSet<MediaTypeEntity> tblMediaTypes { get; set; }
        public DbSet<MessageEntity> tblMessages { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
             
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Table Schema 

            modelBuilder.Entity<ApiTokenEntity>(entity => 
            {
                entity.ToTable("tblApiTokens").HasKey(e => e.ApiId).HasName("PK_tblApiTokens");
            });

            modelBuilder.Entity<UserEntity>(entity =>
            {
                entity.ToTable("tblUsers").HasKey(e => e.UserId).HasName("PK_tblUsers");
            });

            modelBuilder.Entity<UserDetailEntity>(entity =>
            {
                entity.ToTable("tblUserDetails").HasKey(e => e.UserId).HasName("PK_tblUserDetails");
                entity.HasOne(d => d.User).WithOne(p => p.UserDetail).HasConstraintName("FK_tblUserDetails_tblUsers");
            });

            modelBuilder.Entity<GroupEntity>(entity =>
            {
                entity.ToTable("tblGroups").HasKey(e => e.GroupId).HasName("PK_tblGroups"); 
            });

            modelBuilder.Entity<UserGroupEntity>(entity =>
            {
                entity.ToTable("tblUserGroups").HasKey(e => e.UserId).HasName("PK_tblUserGroups");
                entity.HasOne(d => d.Group).WithMany(p => p.UserGroup).HasConstraintName("FK_tblUserGroups_tblGroups");
                entity.HasOne(d => d.User).WithMany(p => p.UserGroups).HasConstraintName("FK_tblUserGroups_tblUsers");

            });

            modelBuilder.Entity<MediaTypeEntity>(entity =>
            {
                entity.ToTable("tblMediaTypes").HasKey(e => e.MediaTypeId).HasName("PK_tblMediaTypes"); 
            });

            modelBuilder.Entity<MessageEntity>(entity =>
            {
                entity.ToTable("tblMessages").HasKey(e => e.MediaTypeId).HasName("PK_tblMessages");

                entity.HasOne(d => d.UserGroup).WithMany(p => p.Messages)
                      .HasConstraintName("FK_tblMessages_tblUserGroups").OnDelete(DeleteBehavior.NoAction);

                entity.HasOne(d => d.ReceiverUserGroup).WithMany(p => p.ReceiverMessages)
                      .HasConstraintName("FK_tblMessages_ReceiverUserGroup").OnDelete(DeleteBehavior.NoAction);

                entity.HasOne(d => d.MediaType).WithMany(p => p.Messages).HasConstraintName("FK_tblMessages_tblMediaTypes");
                
            });

            base.OnModelCreating(modelBuilder);

            #endregion
        }
    }
    
} 