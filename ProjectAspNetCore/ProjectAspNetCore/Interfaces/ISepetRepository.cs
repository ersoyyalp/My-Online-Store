using ProjectAspNetCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectAspNetCore.Interfaces
{
    public interface ISepetRepository
    {
        void SepetEkle(Urun urun);
        void SepettenCikar(Urun urun);
        List<Urun> GetirSepettekiUrunler();
        void SepetiBosalt();
    }
}
