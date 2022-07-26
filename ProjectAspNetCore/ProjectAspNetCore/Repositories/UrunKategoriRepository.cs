using ProjectAspNetCore.Contexts;
using ProjectAspNetCore.Entities;
using ProjectAspNetCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ProjectAspNetCore.Repositories
{
    public class UrunKategoriRepository : GenericRepository<UrunKategori>, IUrunKategoriRepository
    {
        public UrunKategori GetirFiltreile(Expression<Func<UrunKategori, bool>> filter)
        {
            using var context = new ProjectContext();

            return context.UrunKategoriler.FirstOrDefault(filter);
        }

        
    }
}
