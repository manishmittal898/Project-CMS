using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

#nullable disable

namespace CMS.Data.Models
{
    public partial class PracticesContext : DbContext
    {
        public IConfiguration Configuration { get; }
        public PracticesContext()
        {
      
        }

        public PracticesContext(IConfiguration configuration,DbContextOptions<PracticesContext> options)
            : base(options)
        {
            Configuration = configuration;
        }

        public virtual DbSet<TblLookupMaster> TblLookupMasters { get; set; }
        public virtual DbSet<TblLookupTypeMaster> TblLookupTypeMasters { get; set; }
        public virtual DbSet<TblProductImage> TblProductImages { get; set; }
        public virtual DbSet<TblProductMaster> TblProductMasters { get; set; }
        public virtual DbSet<TblProductReview> TblProductReviews { get; set; }
        public virtual DbSet<IRoleType> TblRoleTypes { get; set; }
        public virtual DbSet<TblSubLookupMaster> TblSubLookupMasters { get; set; }
        public virtual DbSet<TblUserMaster> TblUserMasters { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
              optionsBuilder.UseSqlServer("Server=KULDEEP\\SQLEXPRESS;Initial Catalog=Practices;MultipleActiveResultSets=true;User ID=sa;Password=sql@2012;");
           //     optionsBuilder.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));

            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Latin1_General_CI_AI");

            modelBuilder.Entity<TblLookupMaster>(entity =>
            {
                entity.ToTable("tblLookupMaster");

                entity.Property(e => e.CreatedBy).HasMaxLength(250);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.LookUpType).HasColumnName("LookUp_Type");

                entity.Property(e => e.ModifiedBy).HasMaxLength(250);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(250);

                entity.HasOne(d => d.LookUpTypeNavigation)
                    .WithMany(p => p.TblLookupMasters)
                    .HasForeignKey(d => d.LookUpType)
                    .HasConstraintName("FK__tblLookup__LookU__164452B1");
            });

            modelBuilder.Entity<TblLookupTypeMaster>(entity =>
            {
                entity.ToTable("tblLookupTypeMaster");

                entity.Property(e => e.CreatedBy).HasMaxLength(250);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.ModifiedBy).HasMaxLength(250);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(250);
            });

            modelBuilder.Entity<TblProductImage>(entity =>
            {
                entity.ToTable("tblProductImage");

                entity.Property(e => e.CreatedOn)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.FileName).HasMaxLength(250);

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.ProductId).HasColumnName("Product_Id");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.TblProductImages)
                    .HasForeignKey(d => d.CreatedBy)
                    .HasConstraintName("FK__tblProduc__Creat__36B12243");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.TblProductImages)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK__tblProduc__Produ__3A81B327");
            });

            modelBuilder.Entity<TblProductMaster>(entity =>
            {
                entity.ToTable("tblProductMaster");

                entity.Property(e => e.Caption).HasMaxLength(250);

                entity.Property(e => e.CreatedBy).HasMaxLength(250);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.Desc).HasMaxLength(500);

                entity.Property(e => e.ModifiedBy).HasMaxLength(250);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(250);

                entity.Property(e => e.Price).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.Summary).HasMaxLength(500);
            });

            modelBuilder.Entity<TblProductReview>(entity =>
            {
                entity.ToTable("tblProductReview");

                entity.Property(e => e.CreatedOn)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Description).HasMaxLength(4000);

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.ShortDescription).HasMaxLength(2000);

                entity.Property(e => e.Title).HasMaxLength(500);

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.TblProductReviews)
                    .HasForeignKey(d => d.CreatedBy)
                    .HasConstraintName("FK__tblProduc__Creat__3F466844");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.TblProductReviews)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__tblProduc__Produ__3D5E1FD2");
            });

            modelBuilder.Entity<IRoleType>(entity =>
            {
                entity.HasKey(e => e.RoleId)
                    .HasName("PK__tblRoleT__8AFACE1A5DDDCA31");

                entity.ToTable("tblRoleType");

                entity.Property(e => e.CreatedOn)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.RoleName)
                    .IsRequired()
                    .HasMaxLength(250);
            });

            modelBuilder.Entity<TblSubLookupMaster>(entity =>
            {
                entity.ToTable("tblSubLookupMaster");

                entity.Property(e => e.CreatedOn)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(250);

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.TblSubLookupMasters)
                    .HasForeignKey(d => d.CreatedBy)
                    .HasConstraintName("FK__tblSubLoo__Creat__45F365D3");

                entity.HasOne(d => d.LookUp)
                    .WithMany(p => p.TblSubLookupMasters)
                    .HasForeignKey(d => d.LookUpId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__tblSubLoo__LookU__440B1D61");
            });

            modelBuilder.Entity<TblUserMaster>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("PK__tblUserM__1788CC4C655526C9");

                entity.ToTable("tblUserMaster");

                entity.Property(e => e.CreatedOn)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Dob)
                    .HasColumnType("datetime")
                    .HasColumnName("DOB");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(128);

                entity.Property(e => e.FirstName).HasMaxLength(256);

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.LastName).HasMaxLength(256);

                entity.Property(e => e.Mobile).HasMaxLength(15);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.Password).HasMaxLength(250);

                entity.Property(e => e.ProfilePhoto).HasMaxLength(250);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.TblUserMasters)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__tblUserMa__RoleI__300424B4");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
