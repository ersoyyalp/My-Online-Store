using Microsoft.AspNetCore.Mvc;
using ProjectAspNetCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectAspNetCore.ViewComponents
{
    public class UrunList :ViewComponent
    {
       private readonly IUrunRepository _urunRepository;
       public UrunList(IUrunRepository urunRepository)
        {
            _urunRepository = urunRepository;
        }
        public IViewComponentResult Invoke(int? kategoriId)
        {
            if (kategoriId.HasValue)
            {
                return View(_urunRepository.GetirKategoriIdile((int)kategoriId));
            }
            return View(_urunRepository.GetirHepsi());
        }
    }
    
}
