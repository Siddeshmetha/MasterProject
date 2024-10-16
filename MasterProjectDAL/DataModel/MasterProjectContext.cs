using System;
using System.Collections.Generic;
using MasterProjectDAL.ViewModel;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace MasterProjectDAL.DataModel;

public partial class MasterProjectContext : DbContext, IMasterProjectContext
{
    //public MasterProjectContext()
    //{
    //}

    public MasterProjectContext(DbContextOptions<MasterProjectContext> options)
        : base(options)
    {
    }

    public virtual DbSet<User> User { get; set; }

    public virtual DbSet<UserMember> UserMember { get; set; }

    
    public DbSet<Product> Product { get ; set ; }
    public DbSet<Lookup> Lookups { get ; set ; }
    public DbSet<Relation> Relationships { get ; set ; }
    public DbSet<School> School { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    { }
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseMySql("server=127.0.0.1;port=3306;user=root;password=root;database=masterproject", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.32-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Relation>(entity =>
        {
            entity.HasKey(e => e.Idrelations).HasName("PRIMARY");

            entity.ToTable("relation");

            entity.Property(e => e.Idrelations).HasColumnName("idrelations");
            entity.Property(e => e.Relation1)
                .HasMaxLength(45)
                .HasColumnName("Relation");
        });

        modelBuilder.Entity<School>(entity =>
        {
            entity.HasKey(e => e.SchoolId).HasName("PRIMARY");

            entity.ToTable("school");

            entity.HasIndex(e => e.SuserId, "fk_School_SUserId_idx");

            entity.Property(e => e.Location).HasMaxLength(45);
            entity.Property(e => e.SchoolName).HasMaxLength(45);
            entity.Property(e => e.Schoolcol).HasMaxLength(45);
            entity.Property(e => e.SuserId).HasColumnName("SUserId");

            entity.HasOne(d => d.Suser).WithMany(p => p.School)
                .HasForeignKey(d => d.SuserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_School_SUserId");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.IdUser).HasName("PRIMARY");

            entity.ToTable("user");

            entity.Property(e => e.Password).HasMaxLength(45);
            entity.Property(e => e.UserName).HasMaxLength(45);
        });

        modelBuilder.Entity<UserMember>(entity =>
        {
            entity.HasKey(e => e.IduserMember).HasName("PRIMARY");

            entity.ToTable("user_member");

            entity.HasIndex(e => e.RelationId, "fk_user_member_relation_id_idx");

            entity.HasIndex(e => e.UserId, "fk_user_member_user_id_idx");

            entity.Property(e => e.IduserMember).HasColumnName("iduser_member");
            entity.Property(e => e.Age).HasColumnName("age");
            entity.Property(e => e.IsActive)
                .HasDefaultValueSql("'1'")
                .HasColumnName("Is_Active");
            entity.Property(e => e.MemberName)
                .HasMaxLength(45)
                .HasColumnName("Member_Name");
            entity.Property(e => e.MemerRelation)
                .HasMaxLength(45)
                .HasColumnName("Memer_Relation");
            entity.Property(e => e.RelationId).HasColumnName("relation_id");
            entity.Property(e => e.UserId).HasColumnName("User_Id");

            entity.HasOne(d => d.Relation).WithMany(p => p.UserMember)
                .HasForeignKey(d => d.RelationId)
                .HasConstraintName("fk_user_member_relation_id");

            entity.HasOne(d => d.User).WithMany(p => p.UserMember)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_user_member_user_id");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
