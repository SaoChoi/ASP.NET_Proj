using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace OntapASP8.Models
{
    public partial class QLNV : DbContext
    {
        public QLNV()
            : base("name=QLNV")
        {
        }

        public virtual DbSet<NhanVien> NhanViens { get; set; }
        public virtual DbSet<Phong> Phongs { get; set; }
        public virtual DbSet<tblUser> tblUsers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<tblUser>()
                .Property(e => e.username)
                .IsUnicode(false);
        }
    }
}
