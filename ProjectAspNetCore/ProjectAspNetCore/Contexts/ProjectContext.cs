using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProjectAspNetCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectAspNetCore.Contexts
{
    public class ProjectContext : IdentityDbContext<AppUser>
    {
      
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("server=LAPTOP-UI4AV1F3;database=ProjectAspNetCore; integrated security=true;");
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Urun>().HasMany(I => I.UrunKategoriler).WithOne
                (I => I.Urun).HasForeignKey(I => I.UrunId);

            modelBuilder.Entity<Kategori>().HasMany(I => I.UrunKategoriler).WithOne
                (I => I.Kategori).HasForeignKey(I => I.KategoriId);

            modelBuilder.Entity<UrunKategori>().HasIndex(I => new
            {
                I.KategoriId,
                I.UrunId
            }).IsUnique();
            base.OnModelCreating(modelBuilder);
        }


        public DbSet<UrunKategori> UrunKategoriler { get; set; }
        public DbSet<Urun> Urunler { get; set; }
        public DbSet<Kategori> Kategoriler { get; set; }
    }
}
