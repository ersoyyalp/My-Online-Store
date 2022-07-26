using ProjectAspNetCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectAspNetCore.Interfaces
{
    public interface IUrunRepository : IGenericRepository<Urun>
    {
        List<Kategori> GetirKategoriler(int urunId);
        void EkleKategori(UrunKategori urunKategori);
        void SilKategori(UrunKategori urunKategori);
        List<Urun> GetirKategoriIdile(int kategoriId);
    }
}
