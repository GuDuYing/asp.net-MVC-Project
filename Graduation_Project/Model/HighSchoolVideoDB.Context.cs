﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace Model
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class Entities : DbContext
    {
        public Entities()
            : base("name=Entities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<T_Course> T_Course { get; set; }
        public virtual DbSet<T_Discuss> T_Discuss { get; set; }
        public virtual DbSet<T_Exercises> T_Exercises { get; set; }
        public virtual DbSet<T_LOG> T_LOG { get; set; }
        public virtual DbSet<T_Manage> T_Manage { get; set; }
        public virtual DbSet<T_Option> T_Option { get; set; }
        public virtual DbSet<T_SchoolInfo> T_SchoolInfo { get; set; }
        public virtual DbSet<T_SelectCourse> T_SelectCourse { get; set; }
        public virtual DbSet<T_Student> T_Student { get; set; }
        public virtual DbSet<T_Teacher> T_Teacher { get; set; }
        public virtual DbSet<T_User> T_User { get; set; }
        public virtual DbSet<T_Video> T_Video { get; set; }
    }
}
