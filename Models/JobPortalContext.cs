using System;
using System.Collections.Generic;
using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace job_portal.Models;

public partial class JobPortalContext : DbContext
{
    private readonly string dbConn;
    public JobPortalContext(string conn)
    {
        this.dbConn = conn;
    }

    public JobPortalContext(DbContextOptions<JobPortalContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Application> Applications { get; set; }

    public virtual DbSet<ApplicationRole> ApplicationRoles { get; set; }

    public virtual DbSet<Location> Locations { get; set; }

    public virtual DbSet<PreferedRole> PreferedRoles { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Skill> Skills { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<WalkinJob> WalkinJobs { get; set; }

    public virtual DbSet<WalkinJobRole> WalkinJobRoles { get; set; }

    public virtual DbSet<WalkinJobTimeslot> WalkinJobTimeslots { get; set; }

 

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Application>(entity =>
        {
            entity.HasKey(e => e.ApplicationId).HasName("PRIMARY");

            entity.ToTable("applications");

            entity.HasIndex(e => e.ApplicationId, "application_id_UNIQUE").IsUnique();

            entity.HasIndex(e => e.UserId, "user_id_idx");

            entity.HasIndex(e => e.WalkinJobId, "walkin_job_id_idx");

            entity.Property(e => e.ApplicationId).HasColumnName("application_id");
            entity.Property(e => e.DtCreated)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("dt_created");
            entity.Property(e => e.DtModified)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("dt_modified");
            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.WalkinJobId).HasColumnName("walkin_job_id");

            entity.HasOne(d => d.User).WithMany(p => p.Applications)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("userid");

            entity.HasOne(d => d.WalkinJob).WithMany(p => p.Applications)
                .HasForeignKey(d => d.WalkinJobId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("walkin_job_id");
        });

        modelBuilder.Entity<ApplicationRole>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("application_role");

            entity.HasIndex(e => e.AppId, "app_id_idx");

            entity.HasIndex(e => e.Id, "id_UNIQUE").IsUnique();

            entity.HasIndex(e => e.Role, "walkin_job_role_id_idx");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AppId).HasColumnName("app_id");
            entity.Property(e => e.DtCreated)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("dt_created");
            entity.Property(e => e.DtModified)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("dt_modified");
            entity.Property(e => e.Role).HasColumnName("role");

            entity.HasOne(d => d.App).WithMany(p => p.ApplicationRoles)
                .HasForeignKey(d => d.AppId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("app_id");

            entity.HasOne(d => d.RoleNavigation).WithMany(p => p.ApplicationRoles)
                .HasForeignKey(d => d.Role)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("walkin_job_role_id");
        });

        modelBuilder.Entity<Location>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("location");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Address)
                .HasColumnType("text")
                .HasColumnName("address");
            entity.Property(e => e.City)
                .HasMaxLength(45)
                .HasColumnName("city");
            entity.Property(e => e.DtCreated)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("dt_created");
            entity.Property(e => e.DtModified)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("dt_modified");
            entity.Property(e => e.PhoneNo)
                .HasMaxLength(45)
                .HasColumnName("phone_no");
            entity.Property(e => e.PinCode)
                .HasMaxLength(45)
                .HasColumnName("pin_code");
            entity.Property(e => e.VenueName)
                .HasMaxLength(45)
                .HasColumnName("venue_name");
        });

        modelBuilder.Entity<PreferedRole>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("prefered_roles");

            entity.HasIndex(e => e.Id, "id_UNIQUE").IsUnique();

            entity.HasIndex(e => e.Role, "role_id_idx");

            entity.HasIndex(e => e.UserId, "user_id_idx");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.DtCreated)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("dt_created");
            entity.Property(e => e.DtUpdated)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("dt_updated");
            entity.Property(e => e.Role).HasColumnName("role");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.RoleNavigation).WithMany(p => p.PreferedRoles)
                .HasForeignKey(d => d.Role)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("roleid");

            entity.HasOne(d => d.User).WithMany(p => p.PreferedRoles)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("user_id");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("roles");

            entity.HasIndex(e => e.Id, "id").IsUnique();

            entity.HasIndex(e => e.Id, "id_UNIQUE").IsUnique();

            entity.HasIndex(e => e.Title, "title_UNIQUE").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Descr).HasColumnName("descr");
            entity.Property(e => e.DtCreated)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("dt_created");
            entity.Property(e => e.DtUpdated)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("dt_updated");
            entity.Property(e => e.ImageUrl)
                .HasColumnType("mediumtext")
                .HasColumnName("image_url");
            entity.Property(e => e.Package)
                .HasMaxLength(45)
                .HasColumnName("package");
            entity.Property(e => e.Req).HasColumnName("req");
            entity.Property(e => e.Title)
                .HasMaxLength(45)
                .HasColumnName("title");
            entity.Property(e => e.Type).HasMaxLength(45);
        });

        modelBuilder.Entity<Skill>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("skills");

            entity.HasIndex(e => e.Id, "id_UNIQUE").IsUnique();

            entity.HasIndex(e => e.UserId, "user_id_idx");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.DtCreated)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("dt_created");
            entity.Property(e => e.DtUpdtaed)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("dt_updtaed");
            entity.Property(e => e.SkillName)
                .HasMaxLength(45)
                .HasColumnName("skill_name");
            entity.Property(e => e.SkillType)
                .HasColumnType("enum('expert','familiar')")
                .HasColumnName("skill_type");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.Skills)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("user");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("user");

            entity.HasIndex(e => e.Email, "email_UNIQUE").IsUnique();

            entity.HasIndex(e => e.Id, "id_UNIQUE").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CollegeLocation)
                .HasMaxLength(45)
                .HasColumnName("college_location");
            entity.Property(e => e.CollegeName)
                .HasMaxLength(45)
                .HasColumnName("college_name");
            entity.Property(e => e.CurrentCtc)
                .HasMaxLength(50)
                .HasColumnName("currentCTC");
            entity.Property(e => e.DtCreated)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("dt_created");
            entity.Property(e => e.DtModified)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("dt_modified");
            entity.Property(e => e.Email)
                .HasMaxLength(45)
                .HasColumnName("email");
            entity.Property(e => e.EndOfNotice)
                .HasColumnType("date")
                .HasColumnName("endOfNotice");
            entity.Property(e => e.ExpectedCtc)
                .HasMaxLength(50)
                .HasColumnName("expectedCTC");
            entity.Property(e => e.FirstName)
                .HasMaxLength(45)
                .HasColumnName("firstName");
            entity.Property(e => e.HaveAppearedBefore)
                .HasDefaultValueSql("'0'")
                .HasColumnName("haveAppearedBefore");
            entity.Property(e => e.IsExperienced).HasColumnName("isExperienced");
            entity.Property(e => e.IsOnNotice)
                .HasDefaultValueSql("'0'")
                .HasColumnName("isOnNotice");
            entity.Property(e => e.LastName)
                .HasMaxLength(45)
                .HasColumnName("lastName");
            entity.Property(e => e.Password)
                .HasMaxLength(45)
                .HasColumnName("password");
            entity.Property(e => e.Percentage).HasColumnName("percentage");
            entity.Property(e => e.PeriodOfNotice).HasColumnName("periodOfNotice");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(45)
                .HasColumnName("phoneNumber");
            entity.Property(e => e.PortfolioUrl)
                .HasColumnType("mediumtext")
                .HasColumnName("portfolioUrl");
            entity.Property(e => e.ProfilePhoto)
                .HasColumnType("mediumtext")
                .HasColumnName("profilePhoto");
            entity.Property(e => e.Qualification)
                .HasMaxLength(45)
                .HasColumnName("qualification");
            entity.Property(e => e.Referrers)
                .HasMaxLength(45)
                .HasColumnName("referrers");
            entity.Property(e => e.Resume)
                .HasColumnType("mediumtext")
                .HasColumnName("resume");
            entity.Property(e => e.RoleAppearedBefore)
                .HasMaxLength(45)
                .HasColumnName("roleAppearedBefore");
            entity.Property(e => e.SendUpdates)
                .HasDefaultValueSql("'0'")
                .HasColumnName("sendUpdates");
            entity.Property(e => e.Stream)
                .HasMaxLength(45)
                .HasColumnName("stream");
            entity.Property(e => e.YearOfPassing).HasColumnName("year_of_passing");
            entity.Property(e => e.Yoe).HasColumnName("yoe");
        });

        modelBuilder.Entity<WalkinJob>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("walkin_jobs");

            entity.HasIndex(e => e.Id, "id_UNIQUE").IsUnique();

            entity.HasIndex(e => e.Location, "venue_id_idx");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.DtCreated)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("dt_created");
            entity.Property(e => e.DtModified)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("dt_modified");
            entity.Property(e => e.EndDate)
                .HasColumnType("date")
                .HasColumnName("end_date");
            entity.Property(e => e.ExamIns).HasColumnName("examIns");
            entity.Property(e => e.ExtraInfo)
                .HasMaxLength(100)
                .HasColumnName("extra_info");
            entity.Property(e => e.GenIns).HasColumnName("genIns");
            entity.Property(e => e.Location).HasColumnName("location");
            entity.Property(e => e.Process).HasColumnName("process");
            entity.Property(e => e.StartDate)
                .HasColumnType("date")
                .HasColumnName("start_date");
            entity.Property(e => e.SysReq).HasColumnName("sysReq");

            entity.HasOne(d => d.LocationNavigation).WithMany(p => p.WalkinJobs)
                .HasForeignKey(d => d.Location)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("venue_id");
        });

        modelBuilder.Entity<WalkinJobRole>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("walkin_job_roles");

            entity.HasIndex(e => e.Id, "id_UNIQUE").IsUnique();

            entity.HasIndex(e => e.Role, "role_id_walkinjob_idx");

            entity.HasIndex(e => e.WalkinJob, "walkin_job_idx");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.DtCreated)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("dt_created");
            entity.Property(e => e.DtModified)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("dt_modified");
            entity.Property(e => e.Role).HasColumnName("role");
            entity.Property(e => e.WalkinJob).HasColumnName("walkin_job");

            entity.HasOne(d => d.RoleNavigation).WithMany(p => p.WalkinJobRoles)
                .HasForeignKey(d => d.Role)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("role_id_walkinjob");

            entity.HasOne(d => d.WalkinJobNavigation).WithMany(p => p.WalkinJobRoles)
                .HasForeignKey(d => d.WalkinJob)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("walkin_job");
        });

        modelBuilder.Entity<WalkinJobTimeslot>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("walkin_job_timeslots");

            entity.HasIndex(e => e.Id, "id_UNIQUE").IsUnique();

            entity.HasIndex(e => e.WalkinJobId, "walkin_jobs_idx");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.DtCreated)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("dt_created");
            entity.Property(e => e.DtModified)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("dt_modified");
            entity.Property(e => e.EndTime)
                .HasColumnType("datetime")
                .HasColumnName("end_time");
            entity.Property(e => e.StartTime)
                .HasColumnType("datetime")
                .HasColumnName("start_time");
            entity.Property(e => e.WalkinJobId).HasColumnName("walkin_job_id");

            entity.HasOne(d => d.WalkinJob).WithMany(p => p.WalkinJobTimeslots)
                .HasForeignKey(d => d.WalkinJobId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("walkin_jobs");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
