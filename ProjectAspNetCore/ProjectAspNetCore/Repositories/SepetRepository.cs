using Microsoft.AspNetCore.Http;
using ProjectAspNetCore.CustomExtensions;
using ProjectAspNetCore.Entities;
using ProjectAspNetCore.Interfaces;
using System.Collections.Generic;

namespace ProjectAspNetCore.Repositories
{
    public class SepetRepository : ISepetRepository
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public SepetRepository(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public void SepetEkle(Urun urun)
        {
            var gelenListe = _httpContextAccessor.HttpContext.Session.GetObject<List<Urun>>("sepet");

            if (gelenListe == null)
            {
                gelenListe = new List<Urun>();
                gelenListe.Add(urun);
            }
            else
            {
                gelenListe.Add(urun);
            }
            _httpContextAccessor.HttpContext.Session.SetObject("sepet", gelenListe);
        }
        public void SepettenCikar(Urun urun)
        {
            var gelenListe = 
                _httpContextAccessor.HttpContext.Session.GetObject<List<Urun>>("sepet");
            gelenListe.Remove(urun);
            _httpContextAccessor.HttpContext.Session.SetObject("sepet", gelenListe);
        }
        public List<Urun> GetirSepettekiUrunler()
        {
            return _httpContextAccessor.HttpContext.Session.GetObject<List<Urun>>("sepet");
          
        }
        public void SepetiBosalt()
        {
            _httpContextAccessor.HttpContext.Session.Remove("sepet");
        }
    
    }
}
