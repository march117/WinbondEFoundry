﻿//------------------------------------------------------------------------------
// <auto-generated>
//    這個程式碼是由範本產生。
//
//    對這個檔案進行手動變更可能導致您的應用程式產生未預期的行為。
//    如果重新產生程式碼，將會覆寫對這個檔案的手動變更。
// </auto-generated>
//------------------------------------------------------------------------------

namespace DbModel
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class DbEntities : DbContext
    {
        public DbEntities()
            : base("name=DbEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<CompanyIPList> CompanyIPList { get; set; }
        public DbSet<CompanyProfile> CompanyProfile { get; set; }
        public DbSet<DocumentAttr> DocumentAttr { get; set; }
        public DbSet<DocumentMainCategory> DocumentMainCategory { get; set; }
        public DbSet<Documents> Documents { get; set; }
        public DbSet<DocumentSubCategory> DocumentSubCategory { get; set; }
        public DbSet<DocumentsWithSubCategory> DocumentsWithSubCategory { get; set; }
        public DbSet<DocumentWithAttr> DocumentWithAttr { get; set; }
        public DbSet<DownloadHistory> DownloadHistory { get; set; }
        public DbSet<Groups> Groups { get; set; }
        public DbSet<MainFunction> MainFunction { get; set; }
        public DbSet<NDAs> NDAs { get; set; }
        public DbSet<NDAUpdateLog> NDAUpdateLog { get; set; }
        public DbSet<Project> Project { get; set; }
        public DbSet<ProjectAndUsers> ProjectAndUsers { get; set; }
        public DbSet<SubFunction> SubFunction { get; set; }
        public DbSet<UserAndFunction> UserAndFunction { get; set; }
        public DbSet<WinbondGroupAndFuntion> WinbondGroupAndFuntion { get; set; }
        public DbSet<WinbondUserAndGroup> WinbondUserAndGroup { get; set; }
        public DbSet<WinbondUserAndProject> WinbondUserAndProject { get; set; }
        public DbSet<WinbondUsers> WinbondUsers { get; set; }
        public DbSet<UserProfile> UserProfile { get; set; }
    }
}