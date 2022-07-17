using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace CMS.Data.Models
{
    public partial class DB_CMSContext : DbContext
    {
        public DB_CMSContext()
        {
        }

        public DB_CMSContext(DbContextOptions<DB_CMSContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TblLookupMaster> TblLookupMasters { get; set; }
        public virtual DbSet<TblLookupTypeMaster> TblLookupTypeMasters { get; set; }
        public virtual DbSet<TblProductImage> TblProductImages { get; set; }
        public virtual DbSet<TblProductMaster> TblProductMasters { get; set; }
        public virtual DbSet<TblProductReview> TblProductReviews { get; set; }
        public virtual DbSet<TblRoleType> TblRoleTypes { get; set; }
        public virtual DbSet<TblSubLookupMaster> TblSubLookupMasters { get; set; }
        public virtual DbSet<TblUserMaster> TblUserMasters { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=LAPTOP-VC0IBCS1;Database=DB_CMS;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Latin1_General_CI_AI");

            modelBuilder.Entity<TblLookupMaster>(entity =>
            {
                entity.ToTable("tblLookupMaster");

                entity.Property(e => e.CreatedOn)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ImagePath).HasMaxLength(1000);

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.LookUpType).HasColumnName("LookUp_Type");

                entity.Property(e => e.ModifiedOn)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Name).HasMaxLength(500);

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.TblLookupMasterCreatedByNavigations)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblLookupMaster_CreatedBy");

                entity.HasOne(d => d.LookUpTypeNavigation)
                    .WithMany(p => p.TblLookupMasters)
                    .HasForeignKey(d => d.LookUpType)
                    .HasConstraintName("FK_tblLookupMaster_LookUp_Type");

                entity.HasOne(d => d.ModifiedByNavigation)
                    .WithMany(p => p.TblLookupMasterModifiedByNavigations)
                    .HasForeignKey(d => d.ModifiedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblLookupMaster_ModifiedBy");
            });

            modelBuilder.Entity<TblLookupTypeMaster>(entity =>
            {
                entity.ToTable("tblLookupTypeMaster");

                entity.Property(e => e.CreatedOn)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.EnumValue)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.ModifiedOn)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Name).HasMaxLength(250);

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.TblLookupTypeMasterCreatedByNavigations)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblLookupTypeMaster_CreatedBy");

                entity.HasOne(d => d.ModifiedByNavigation)
                    .WithMany(p => p.TblLookupTypeMasterModifiedByNavigations)
                    .HasForeignKey(d => d.ModifiedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblLookupTypeMaster_ModifiedBy");
            });

            modelBuilder.Entity<TblProductImage>(entity =>
            {
                entity.ToTable("tblProductImage");

                entity.Property(e => e.CreatedOn)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.ModifiedOn)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ProductId).HasColumnName("Product_Id");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.TblProductImageCreatedByNavigations)
                    .HasForeignKey(d => d.CreatedBy)
                    .HasConstraintName("FK_tblProductImage_CreatedBy");

                entity.HasOne(d => d.ModifiedByNavigation)
                    .WithMany(p => p.TblProductImageModifiedByNavigations)
                    .HasForeignKey(d => d.ModifiedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblProductImage_ModifiedBy");
            });

            modelBuilder.Entity<TblProductMaster>(entity =>
            {
                entity.ToTable("tblProductMaster");

                entity.Property(e => e.CreatedOn)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Desc).HasColumnType("ntext");

                entity.Property(e => e.ImagePath).HasMaxLength(1000);

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.ModifiedOn)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(2000);

                entity.Property(e => e.Price).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.Summary).HasColumnType("ntext");

                entity.HasOne(d => d.CaptionTag)
                    .WithMany(p => p.TblProductMasterCaptionTags)
                    .HasForeignKey(d => d.CaptionTagId)
                    .HasConstraintName("FK_tblProductMaster_CaptionTagId");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.TblProductMasterCategories)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblProductMaster_CategoryId");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.TblProductMasterCreatedByNavigations)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblProductMaster_CreatedBy");

                entity.HasOne(d => d.ModifiedByNavigation)
                    .WithMany(p => p.TblProductMasterModifiedByNavigations)
                    .HasForeignKey(d => d.ModifiedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblProductMaster_ModifiedBy");

                entity.HasOne(d => d.SubCategory)
                    .WithMany(p => p.TblProductMasters)
                    .HasForeignKey(d => d.SubCategoryId)
                    .HasConstraintName("FK_tblProductMaster_SubCategoryId");
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

                entity.Property(e => e.ModifiedOn)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ShortDescription).HasMaxLength(2000);

                entity.Property(e => e.Title).HasMaxLength(500);

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.TblProductReviews)
                    .HasForeignKey(d => d.CreatedBy)
                    .HasConstraintName("tblProductReview_CreatedBy");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.TblProductReviews)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("tblProductReview_ProductId");
            });

            modelBuilder.Entity<TblRoleType>(entity =>
            {
                entity.HasKey(e => e.RoleId)
                    .HasName("PK__tblRoleT__8AFACE1A750C2A0B");

                entity.ToTable("tblRoleType");

                entity.Property(e => e.CreatedOn)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.ModifiedOn)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.RoleName)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.TblRoleTypeCreatedByNavigations)
                    .HasForeignKey(d => d.CreatedBy)
                    .HasConstraintName("FK_tblRoleType_CreatedBy");

                entity.HasOne(d => d.ModifiedByNavigation)
                    .WithMany(p => p.TblRoleTypeModifiedByNavigations)
                    .HasForeignKey(d => d.ModifiedBy)
                    .HasConstraintName("FK_tblRoleType_ModifiedBy");
            });

            modelBuilder.Entity<TblSubLookupMaster>(entity =>
            {
                entity.ToTable("tblSubLookupMaster");

                entity.Property(e => e.CreatedOn)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ImagePath).HasMaxLength(1000);

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.ModifiedOn)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Name).HasMaxLength(500);

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.TblSubLookupMasterCreatedByNavigations)
                    .HasForeignKey(d => d.CreatedBy)
                    .HasConstraintName("tblSubLookupMaster_CreatedBy");

                entity.HasOne(d => d.LookUp)
                    .WithMany(p => p.TblSubLookupMasters)
                    .HasForeignKey(d => d.LookUpId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("tblSubLookupMaster_LookUpId");

                entity.HasOne(d => d.ModifiedByNavigation)
                    .WithMany(p => p.TblSubLookupMasterModifiedByNavigations)
                    .HasForeignKey(d => d.ModifiedBy)
                    .HasConstraintName("tblSubLookupMaster_ModifiedBy");
            });

            modelBuilder.Entity<TblUserMaster>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("PK__tblUserM__1788CC4C1AEC9452");

                entity.ToTable("tblUserMaster");

                entity.Property(e => e.CreatedOn)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Dob)
                    .HasColumnType("datetime")
                    .HasColumnName("DOB");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.FirstName).HasMaxLength(256);

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.LastName).HasMaxLength(256);

                entity.Property(e => e.Mobile).HasMaxLength(15);

                entity.Property(e => e.ModifiedOn)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Password).HasMaxLength(250);

                entity.Property(e => e.ProfilePhoto).HasMaxLength(1000);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.TblUserMasters)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__tblUserMa__RoleI__6477ECF3");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
