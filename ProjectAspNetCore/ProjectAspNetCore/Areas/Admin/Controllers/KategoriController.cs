﻿using Microsoft.AspNetCore.Mvc;
using ProjectAspNetCore.Entities;
using ProjectAspNetCore.Interfaces;
using ProjectAspNetCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectAspNetCore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class KategoriController : Controller
    {
        
        private readonly IKategoriRepository _kategoriRepository;
        public KategoriController(IKategoriRepository kategoriRepository)
        {
            _kategoriRepository = kategoriRepository;
        }
        public IActionResult Index()
        {
            return View(_kategoriRepository.GetirHepsi());
        }
        public IActionResult Ekle()
        {
            return View(new KategoriEkleModel());
        }
        [HttpPost]
        public IActionResult Ekle(KategoriEkleModel model)
        {
            if (ModelState.IsValid)
            {
                _kategoriRepository.Ekle(new Kategori{ 
                    Ad = model.Ad
                });
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public IActionResult Guncelle(int id)
        {
            var guncellenecekKategori = _kategoriRepository.GetirIdile(id);
            KategoriGuncelleModel model = new KategoriGuncelleModel {
                Id = guncellenecekKategori.Id,
                Ad = guncellenecekKategori.Ad
               
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult Guncelle(KategoriGuncelleModel model)
        {
            if (ModelState.IsValid)
            {
                var guncellenecekKategori = _kategoriRepository.GetirIdile(model.Id);
                guncellenecekKategori.Ad = model.Ad;

                _kategoriRepository.Guncelle(guncellenecekKategori);

                return RedirectToAction("Index");
            }

            return View(model);
        }
        public IActionResult Sil(int id)
        {
            _kategoriRepository.Sil(new Kategori { Id = id });
            return RedirectToAction("Index");
        }
            
    }
}
