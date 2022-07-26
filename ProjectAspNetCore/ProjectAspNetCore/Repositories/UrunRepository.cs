using Microsoft.EntityFrameworkCore.Internal;
using ProjectAspNetCore.Contexts;
using ProjectAspNetCore.Entities;
using ProjectAspNetCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectAspNetCore.Repositories
{
    public class UrunRepository : GenericRepository<Urun>, IUrunRepository
    {
        private readonly IUrunKategoriRepository _urunKategoriRepository;
        public UrunRepository(IUrunKategoriRepository urunKategoriRepository)
        {
            _urunKategoriRepository = urunKategoriRepository;
        }

        public List<Kategori> GetirKategoriler(int urunId)
        {
            using var context = new ProjectContext();
            return context.Urunler.Join(context.UrunKategoriler, urun => urun.Id, urunKategori => urunKategori.UrunId,
                (u, uc) => new
                {
                    urun = u,
                    urunKategori = uc

                }).Join(context.Kategoriler, iki => iki.urunKategori.KategoriId, kategori => kategori.Id, (uc, k) => new
                {
                    urun = uc.urun,
                    kategori = k,
                    UrunKategori = uc.urunKategori

                }).Where(I => I.urun.Id == urunId).Select(I => new Kategori
                {
                    Ad = I.kategori.Ad,
                    Id = I.kategori.Id
                }).ToList();
        }

        public void SilKategori(UrunKategori urunKategori)
        {
            var kontrolKayit = _urunKategoriRepository.GetirFiltreile(I => I.KategoriId == urunKategori.KategoriId && I.UrunId == urunKategori.UrunId);
            if (kontrolKayit != null)
            {
                _urunKategoriRepository.Sil(kontrolKayit);
            }

        }
        public void EkleKategori(UrunKategori urunKategori)
        {
            var kontrolKayit = _urunKategoriRepository.GetirFiltreile(I => I.KategoriId == urunKategori.KategoriId && I.UrunId == urunKategori.UrunId);
            if (kontrolKayit == null)
            {
                _urunKategoriRepository.Ekle(urunKategori);
            }
        }

        public List<Urun> GetirKategoriIdile(int kategoriId)
        {
            using var context = new ProjectContext();

            return context.Urunler.Join(context.UrunKategoriler, u => u.Id, uc => uc.UrunId,
                (urun, urunKategori) => new
                {
                    Urun = urun,
                    UrunKategori = urunKategori


                }).Where(I => I.UrunKategori.KategoriId == kategoriId).Select(I => new Urun
                {
                    Id = I.Urun.Id,
                    Ad = I.Urun.Ad,
                    Fiyat = I.Urun.Fiyat,
                    Resim = I.Urun.Resim
                }).ToList();
        }
    }
}