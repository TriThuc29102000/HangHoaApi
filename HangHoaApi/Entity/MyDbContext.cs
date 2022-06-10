using HangHoaApi.IdentityAuth;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HangHoaApi.Entity
{
    public class MyDbContext : IdentityDbContext<ApplicationUser>
    {
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options) { }
        //Tao map sang db
        #region DbSet
        public DbSet<LoaiEntity> loaiEntities { get; set; }
        public DbSet<HangHoaEntity> hangHoaEntities { get; set; }
        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);

            ///////////////////////////////
            modelBuilder.Entity<LoaiEntity>(entity =>
            {
                entity.ToTable("Loai");
                entity.HasKey(loai => loai.MaLoai);
                entity.Property(loai => loai.TenLoai).IsRequired();
            });
            modelBuilder.Entity<HangHoaEntity>(entity =>
            {
                entity.ToTable("HangHoa");
                entity.HasKey(hanghoa => hanghoa.MaHangHoa);
                entity.Property(hanghoa => hanghoa.TenHangHoa).IsRequired();

                //Tao lien ket giua cac bang
                entity.HasOne(e => e.LoaiEntity).WithMany(e => e.hangHoaEntities)
                .HasForeignKey(e => e.MaLoai).HasConstraintName("FK_Loai_HangHoa");
            });

            /////////////////////////////////
            
        }
    }
}
