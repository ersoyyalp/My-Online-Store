﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProjectAspNetCore.Entities;
using ProjectAspNetCore.Interfaces;
using ProjectAspNetCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectAspNetCore.Controllers
{
    public class HomeController : Controller
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IUrunRepository _urunRepository;
        private readonly ISepetRepository _sepetRepository;
        public HomeController(IUrunRepository urunRepository, SignInManager<AppUser> 
            signInManager, ISepetRepository sepetRepository)
        {
            _signInManager = signInManager;
            _urunRepository = urunRepository;
            _sepetRepository = sepetRepository;
        }
        public IActionResult Index(int? kategoriId)
        {
            ViewBag.KategoriId = kategoriId;
            return View();
        }
        public IActionResult UrunDetay(int id)
        {
            return View(_urunRepository.GetirIdile(id));
        }

        public IActionResult GirisYap()
        {
            return View(new KullanıcıGirisModel());
        }
        [HttpPost]
        public IActionResult GirisYap(KullanıcıGirisModel model)
        {
            if (ModelState.IsValid)
            {
                var signInResult = _signInManager.PasswordSignInAsync(model.KullaniciAd, model.Sifre,
                    model.BeniHatirla, false).Result;

                if (signInResult.Succeeded)
                {
                    return RedirectToAction("Index", "Home", new { area = "Admin" });
                }
                ModelState.AddModelError("", "Kullanıcı Adı veya Şifre hatalı");
            }
            return View(model);
        }
        public IActionResult Sepet()
        {
            return View(_sepetRepository.GetirSepettekiUrunler());
        }
        public IActionResult SepettenCikar(int id)
        {
           var cikarilacakUrun = _urunRepository.GetirIdile(id);
            _sepetRepository.SepettenCikar(cikarilacakUrun);
            return RedirectToAction("Sepet");
        }
        public IActionResult SepetiBosalt(decimal fiyat)
        {
            _sepetRepository.SepetiBosalt();
            return RedirectToAction("Tesekkur", new {fiyat=fiyat });
        }
        public IActionResult Tesekkur(decimal fiyat)
        {
            ViewBag.Fiyat = fiyat;
            return View();
        }

        public IActionResult EkleSepet(int id)
        {
            var urun = _urunRepository.GetirIdile(id);
            _sepetRepository.SepetEkle(urun);
            TempData["bildirim"] = "Ürün sepete eklendi";
            return RedirectToAction("Index");
        }

        public IActionResult NotFound(int code)
        {
            ViewBag.Code = code;
            return View();
        }

    }
   
}
