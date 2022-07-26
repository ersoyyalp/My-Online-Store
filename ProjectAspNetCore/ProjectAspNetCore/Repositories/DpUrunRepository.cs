using Dapper.Contrib.Extensions;
using Microsoft.Data.SqlClient;
using ProjectAspNetCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectAspNetCore.Repositories
{
    public class DpUrunRepository
    {
        public List<Urun> GetirHepsi()
        {
            using var connection = new SqlConnection("server=LAPTOP-UI4AV1F3;database=ProjectAspNetCore; integrated security=true;");

            return connection.GetAll<Urun>().ToList();

        }
    }
}
