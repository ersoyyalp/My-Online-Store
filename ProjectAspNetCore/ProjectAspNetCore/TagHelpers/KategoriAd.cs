using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;
using ProjectAspNetCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectAspNetCore.TagHelpers
{   [HtmlTargetElement("getirKategoriAd")]
    public class KategoriAd : TagHelper
    {
        private readonly IUrunRepository _urunRepository;
        public KategoriAd(IUrunRepository urunRepository)
        {
            _urunRepository = urunRepository;

        }
        public int UrunId { get; set; }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
           
            string data = "";
            var gelenKategoriler = _urunRepository.GetirKategoriler(UrunId).Select(I => I.Ad);

            foreach (var item in gelenKategoriler)
            {
                data += item+" ";
            }
            output.Content.SetContent(data);
        }
    }
}
